using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace модель_Белла_Лападула
{
    /// <summary>
    /// Логика взаимодействия для Add_Del_Subj.xaml
    /// </summary>
    public partial class Add_Del_Subj : Window
    {
        public Add_Del_Subj()
        {
            InitializeComponent();
            for (int i = 0; i < 3; i++)
            {
                CB_lvl.Items.Add(i);
            }
        }
        static List<List<int>> writeStreamMas(string nameOfFile, List<List<int>> numberList)
        {
            StreamWriter sw = new StreamWriter(nameOfFile);
            foreach (List<int> s in numberList)
            {
                for (int i = 0; i < s.Count; i++)
                {
                    sw.Write(s[i] + " ");
                }
                sw.WriteLine('\t');
            }
            sw.Close();
            return numberList;
        }
        static List<List<string>> writeStreamMasddf(string nameOfFile, List<List<string>> numberList)
        {
            StreamWriter sw = new StreamWriter(nameOfFile);
            foreach (List<string> s in numberList)
            {
                for (int i = 0; i < s.Count; i++)
                {
                    sw.Write(s[i] + " ");
                }
                sw.WriteLine('\t');
            }
            sw.Close();
            return numberList;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            List<string> tempList = new List<string>();
            string nameofdeleteuser = "";
            int index = -1;
            int indexoffile = -1;
            if (tb_del_user.Text != "")
            {
                foreach (List<string> item in (Application.Current.MainWindow as MainWindow).user)
                {
                    if (item[0]==tb_del_user.Text)
                    {
                        index = (Application.Current.MainWindow as MainWindow).user.IndexOf(item);
                        nameofdeleteuser = item[0];
                    }
                    else
                    {
                        tempList.Add(item[0]);
                    }
                }
            }


            if (nameofdeleteuser != "" && index!=-1)
            {
                (Application.Current.MainWindow as MainWindow).user.RemoveAt(index);
                (Application.Current.MainWindow as MainWindow).AccessMatrix.Remove((Application.Current.MainWindow as MainWindow).AccessMatrix[index]);
                (Application.Current.MainWindow as MainWindow).AccessTimeMatrix.Remove((Application.Current.MainWindow as MainWindow).AccessTimeMatrix[index]);

                for (int i = (Application.Current.MainWindow as MainWindow).objList.Count-1; i >=0; i--)
                {
                    if ((Application.Current.MainWindow as MainWindow).objList[i][2]== nameofdeleteuser)
                    {
                        (Application.Current.MainWindow as MainWindow).objList[i][2] = "deleted_user";
                        //indexoffile = i;
                        //(Application.Current.MainWindow as MainWindow).objList.RemoveAt(indexoffile);
                        //foreach (List<int> item in (Application.Current.MainWindow as MainWindow).AccessMatrix)
                        //{
                        //    item.Remove(item[indexoffile]);
                        //}
                    }
                }
                writeStreamMasddf("Пользователи и уровни.txt", (Application.Current.MainWindow as MainWindow).user);
                writeStreamMas("Матрица доступов.txt", (Application.Current.MainWindow as MainWindow).AccessMatrix);
                writeStreamMas("Матрица текущих доступов.txt", (Application.Current.MainWindow as MainWindow).AccessTimeMatrix);
                writeStreamMasddf("Объект уровень владелец.txt", (Application.Current.MainWindow as MainWindow).objList);

                if (tempList.Contains(tb_del_user.Text))
                {
                    MessageBox.Show("Ошибка! Пользователь не был удален.");
                }
                else
                {
                    MessageBox.Show("Пользователь успешно удален!");
                    tb_del_user.Text = "";
                }
              

            }
            else
            {
                MessageBox.Show("Такого пользователя не существут!");
            }
           
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            int temp = -1;
            int ds = -1;
            List<string> s = new List<string>();
            List<int> n = new List<int>();
            bool check = false;
            foreach (List<string> item in (Application.Current.MainWindow as MainWindow).user)
            {
                if (item[0] == tb_UN.Text)
                {
                    check = true;
                }
            }

            if (tb_UN.Text != "" && CB_lvl.SelectedIndex != -1 && !check)
            {
                //Как можно реализовать добавление строчки по-другому?
                s.Add(tb_UN.Text);
                s.Add(CB_lvl.SelectedValue.ToString());
                s.Add(CB_lvl.SelectedValue.ToString());
                (Application.Current.MainWindow as MainWindow).user.Add(s);

                List<int> rulesforUser = new List<int>();
                for (int i = 0; i < (Application.Current.MainWindow as MainWindow).objList.Count; i++)
                {
                    rulesforUser.Add(15);
                }
                (Application.Current.MainWindow as MainWindow).AccessMatrix.Add(rulesforUser);
                (Application.Current.MainWindow as MainWindow).AccessTimeMatrix.Add(rulesforUser);

                temp = (Application.Current.MainWindow as MainWindow).user.IndexOf(s);

                for (int i = 0; i <= (Application.Current.MainWindow as MainWindow).objList.Count - 1; i++)
                {
                    ds = (Application.Current.MainWindow as MainWindow).objList.IndexOf((Application.Current.MainWindow as MainWindow).objList[i]);
                    if (int.Parse(CB_lvl.SelectedValue.ToString()) > int.Parse((Application.Current.MainWindow as MainWindow).objList[i][1]))
                    {
                        (Application.Current.MainWindow as MainWindow).AccessTimeMatrix[temp][ds] = 12;
                        (Application.Current.MainWindow as MainWindow).AccessMatrix[temp][ds] = 12;
                    }

                    if (int.Parse(CB_lvl.SelectedValue.ToString()) == int.Parse((Application.Current.MainWindow as MainWindow).objList[i][1]))
                    {
                        (Application.Current.MainWindow as MainWindow).AccessTimeMatrix[temp][ds] = 2;
                        (Application.Current.MainWindow as MainWindow).AccessMatrix[temp][ds] = 2;
                    }

                    if (int.Parse(CB_lvl.SelectedValue.ToString()) < int.Parse((Application.Current.MainWindow as MainWindow).objList[i][1]))
                    {
                        (Application.Current.MainWindow as MainWindow).AccessTimeMatrix[temp][ds] = 5;
                        (Application.Current.MainWindow as MainWindow).AccessMatrix[temp][ds] = 5;
                    }

                }
                //foreach (List<string> item in (Application.Current.MainWindow as MainWindow).objList)
                //{
                //    if (int.Parse(CB_lvl.SelectedValue.ToString()) > int.Parse(item[1]))
                //    {
                //        ds = (Application.Current.MainWindow as MainWindow).objList.IndexOf(item);
                //        (Application.Current.MainWindow as MainWindow).AccessMatrix[temp][ds] = 12;

                //    }
                //    if (int.Parse(CB_lvl.SelectedValue.ToString()) == int.Parse(item[1]))
                //    {
                //        ds = (Application.Current.MainWindow as MainWindow).objList.IndexOf(item);
                //        (Application.Current.MainWindow as MainWindow).AccessMatrix[temp][ds] = 0;

                //    }
                //    if (int.Parse(CB_lvl.SelectedValue.ToString()) == int.Parse(item[1]))
                //    {
                //        ds = (Application.Current.MainWindow as MainWindow).objList.IndexOf(item);
                //        (Application.Current.MainWindow as MainWindow).AccessMatrix[temp][ds] = 5;

                //    }
                //}

                writeStreamMas("Матрица доступов.txt", (Application.Current.MainWindow as MainWindow).AccessMatrix);
                writeStreamMas("Матрица текущих доступов.txt", (Application.Current.MainWindow as MainWindow).AccessTimeMatrix);
                writeStreamMasddf("Пользователи и уровни.txt", (Application.Current.MainWindow as MainWindow).user);
            }
            else
                MessageBox.Show("Неверно заданы параметры");
        }
    }
}
