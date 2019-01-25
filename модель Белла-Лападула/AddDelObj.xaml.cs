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
    /// Логика взаимодействия для AddDelObj.xaml
    /// </summary>
    public partial class AddDelObj : Window
    {

        public AddDelObj()
        {

            InitializeComponent();
            for (int i = 0; i <= 3; i++)
            {
                CBlvl.Items.Add(i);
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

        private void button_Click(object sender, RoutedEventArgs e)//удаление объекта
        {
            List<string> tempList = new List<string>();
            string nameofdeletefile = "";
            int index = -1; 
            if (TB_del_obj.Text != "")
            {
                foreach (List<string> item in (Application.Current.MainWindow as MainWindow).objList)
                {
                    if (item[0] == TB_del_obj.Text && ((item[2] == (Application.Current.MainWindow as MainWindow).userName.Text || (Application.Current.MainWindow as MainWindow).userName.Text == "root")))
                    {
                        index = (Application.Current.MainWindow as MainWindow).objList.IndexOf(item);
                        nameofdeletefile = TB_del_obj.Text;
                        (Application.Current.MainWindow as MainWindow).objList.RemoveAt(index);
                        foreach (List<int> item1 in (Application.Current.MainWindow as MainWindow).AccessMatrix)
                        {
                            item1.Remove(item1[index]);
                        }
                        foreach (List<int> item1 in (Application.Current.MainWindow as MainWindow).AccessTimeMatrix)
                        {
                            item1.Remove(item1[index]);
                        }
                        writeStreamMasddf("Объект уровень владелец.txt", (Application.Current.MainWindow as MainWindow).objList);
                        writeStreamMas("Матрица доступов.txt", (Application.Current.MainWindow as MainWindow).AccessMatrix);
                        writeStreamMas("Матрица текущих доступов.txt", (Application.Current.MainWindow as MainWindow).AccessTimeMatrix);
                        MessageBox.Show("Файл успешно удален!");
                        TB_del_obj.Text = "";
                        return;
                    }
                }
                if (index == -1)
                    MessageBox.Show("Такого файла не существут либо вы не являетесь владельцем данного файла!");
            }
            //if (nameofdeletefile != "")
            //{
            //    (Application.Current.MainWindow as MainWindow).objList.RemoveAt(index);

            //    foreach (List<int> item in (Application.Current.MainWindow as MainWindow).AccessMatrix)
            //    {
            //        item.Remove(item[index]);
            //    }
            //    writeStreamMasddf("Объект уровень владелец.txt", (Application.Current.MainWindow as MainWindow).objList);
            //    writeStreamMas("Матрица доступов.txt", (Application.Current.MainWindow as MainWindow).AccessMatrix);
            //    if (tempList.Contains(TB_del_obj.Text))
            //    {
            //        MessageBox.Show("Ошибка! Файл не был удален.");
            //    }
            //    else
            //    {
            //        MessageBox.Show("Файл успешно удален!");
            //        TB_del_obj.Text = "";
            //    }

            //}
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            int temp = -1;
            int ds = -1;
            List<string> s = new List<string>();
            List<int> n = new List<int>();
            bool check = false;
            foreach (List<string> item in (Application.Current.MainWindow as MainWindow).objList)
            {
                if (item[0] == tb_add_file.Text)
                {
                    check = true;
                }
            }
            if (tb_add_file.Text != "" && CBlvl.SelectedIndex != -1 && !check)
            {
                //Как можно реализовать добавление строчки по-другому?
                s.Add(tb_add_file.Text);
                s.Add(CBlvl.SelectedValue.ToString());
                s.Add((Application.Current.MainWindow as MainWindow).userName.Text);

                List<int> rulesforUser = new List<int>();
                for (int i = 0; i <= (Application.Current.MainWindow as MainWindow).user.Count - 1; i++)
                {
                    (Application.Current.MainWindow as MainWindow).AccessMatrix[i].Add(15);
                    (Application.Current.MainWindow as MainWindow).AccessTimeMatrix[i].Add(15);
                }
                (Application.Current.MainWindow as MainWindow).objList.Add(s);
                temp = (Application.Current.MainWindow as MainWindow).objList.IndexOf(s);
                for (int i = 0; i <= (Application.Current.MainWindow as MainWindow).user.Count - 1; i++)
                {
                    ds = (Application.Current.MainWindow as MainWindow).user.IndexOf((Application.Current.MainWindow as MainWindow).user[i]);
                    if (int.Parse(CBlvl.SelectedValue.ToString()) > int.Parse((Application.Current.MainWindow as MainWindow).user[i][2]))
                    {
                        (Application.Current.MainWindow as MainWindow).AccessMatrix[ds][temp] = 12;
                        (Application.Current.MainWindow as MainWindow).AccessTimeMatrix[ds][temp] = 12;
                    }
                    if (int.Parse(CBlvl.SelectedValue.ToString()) == int.Parse((Application.Current.MainWindow as MainWindow).user[i][2]))
                    {
                        (Application.Current.MainWindow as MainWindow).AccessMatrix[ds][temp] = 2;
                        (Application.Current.MainWindow as MainWindow).AccessTimeMatrix[ds][temp] = 2;
                    }
                    if (int.Parse(CBlvl.SelectedValue.ToString()) < int.Parse((Application.Current.MainWindow as MainWindow).user[i][2]))
                    {
                        (Application.Current.MainWindow as MainWindow).AccessMatrix[ds][temp] = 5;
                        (Application.Current.MainWindow as MainWindow).AccessTimeMatrix[ds][temp] = 5;
                    }
                }
            }
            else
                MessageBox.Show("Данные введены не верно");
            writeStreamMas("Матрица доступов.txt", (Application.Current.MainWindow as MainWindow).AccessMatrix);
            writeStreamMas("Матрица текущих доступов.txt", (Application.Current.MainWindow as MainWindow).AccessTimeMatrix);
            writeStreamMasddf("Объект уровень владелец.txt", (Application.Current.MainWindow as MainWindow).objList);
        }
    }
}
