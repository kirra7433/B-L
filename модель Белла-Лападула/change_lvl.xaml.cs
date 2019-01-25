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
    /// Логика взаимодействия для change_lvl.xaml
    /// </summary>
    public partial class change_lvl : Window
    {
        public change_lvl()
        {
            InitializeComponent();
            if((Application.Current.MainWindow as MainWindow).userName.Text!="root")
            {
                TBsubj.Text = (Application.Current.MainWindow as MainWindow).userName.Text;
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
        private void CBacSubj_MouseEnter(object sender, MouseEventArgs e)
        {
            bool super = false;
            foreach (List<string> t in (Application.Current.MainWindow as MainWindow).user)
            {

                if (((Application.Current.MainWindow as MainWindow).userName.Text)=="root" && TBsubj.Text != "")
                {
                    super = true;
                }
            }

            CBacSubj.Items.Clear();

            int index = -1;
            if (TBsubj.Text != "" && !super)
            {
                foreach (List<string> t in (Application.Current.MainWindow as MainWindow).user)
                {
                    if (t[0]==(TBsubj.Text))
                    {
                        index = int.Parse(t[2]);
                    }
                }
                if (index != -1 && (Application.Current.MainWindow as MainWindow).userName.Text == TBsubj.Text)
                {
                    for (int i = index; i >= 0; i--)
                    {
                        CBacSubj.Items.Add(i);
                    }
                }
                else
                {
                    MessageBox.Show("Такого пользователя не существует");
                }
            }
            else if (super)
            {
                for (int i = 3; i >=0; i--)
                {
                    CBacSubj.Items.Add(i);
                }
            }
            else if (TBsubj.Text != "")
            {
                MessageBox.Show("Невозможно поменять уровень доступа");
                TBsubj.Text = "";
            }
           

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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            bool rot = ((Application.Current.MainWindow as MainWindow).userName.Text=="root");
            int inf = -1;
            int ds = -1;
            if (TBsubj.Text != "" && !rot)
            {
                foreach (List<string> t in (Application.Current.MainWindow as MainWindow).user)
                {
                    if (t[0] == (TBsubj.Text) && (Application.Current.MainWindow as MainWindow).userName.Text == (TBsubj.Text))
                    {
                        //t[2] = CBacSubj.SelectedValue.ToString();
                        t[1] = CBacSubj.SelectedValue.ToString();
                    }
                }
            }
            else
            {
                if (TBsubj.Text != "")
                {
                    foreach (List<string> t in (Application.Current.MainWindow as MainWindow).user)
                    {
                        if (t[0] == (TBsubj.Text))
                        {
                            t[2] = CBacSubj.SelectedValue.ToString();
                            t[1] = CBacSubj.SelectedValue.ToString();
                        }
                    }
                }
            }

            foreach (List<string> item in (Application.Current.MainWindow as MainWindow).user)
            {
                if (item[0]==(TBsubj.Text))
                {
                    inf = (Application.Current.MainWindow as MainWindow).user.IndexOf(item);
                }
            }
            foreach (List <string> item in (Application.Current.MainWindow as MainWindow).objList)
            {
                ds = (Application.Current.MainWindow as MainWindow).objList.IndexOf(item);
                if (int.Parse(CBacSubj.SelectedValue.ToString()) > int.Parse(item[1]))
                {
                    (Application.Current.MainWindow as MainWindow).AccessTimeMatrix[inf][ds] = 12;
                    (Application.Current.MainWindow as MainWindow).AccessMatrix[inf][ds] = 12;
                }
                if (int.Parse(CBacSubj.SelectedValue.ToString()) == int.Parse(item[1]))
                {
                    (Application.Current.MainWindow as MainWindow).AccessTimeMatrix[inf][ds] = 2;
                    (Application.Current.MainWindow as MainWindow).AccessMatrix[inf][ds] = 2;
                }
                if (int.Parse(CBacSubj.SelectedValue.ToString()) < int.Parse(item[1]))
                {
                    (Application.Current.MainWindow as MainWindow).AccessTimeMatrix[inf][ds] = 5;
                    (Application.Current.MainWindow as MainWindow).AccessMatrix[inf][ds] = 5;
                }
            }
            writeStreamMas("Матрица доступов.txt", (Application.Current.MainWindow as MainWindow).AccessMatrix);
            writeStreamMas("Матрица текущих доступов.txt", (Application.Current.MainWindow as MainWindow).AccessTimeMatrix);
            writeStreamMasddf("Пользователи и уровни.txt", (Application.Current.MainWindow as MainWindow).user);
        }
    }
}
