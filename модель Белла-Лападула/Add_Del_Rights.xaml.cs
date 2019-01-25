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
    /// Логика взаимодействия для Add_Del_Rights.xaml
    /// </summary>
    public partial class Add_Del_Rights : Window
    {
        public Add_Del_Rights()
        {
            InitializeComponent();
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

        private void button_Click(object sender, RoutedEventArgs e)
        {

            int fileIndex = -1;
            int userIndex = -1;
            bool check = false;
            if (TB_obj.Text!="" && TB_subj.Text!="" && tb_rights.Text!="")
            {
                if ((Application.Current.MainWindow as MainWindow).userName.Text != "root")
                {
                    foreach (List<string> s in (Application.Current.MainWindow as MainWindow).objList)
                    {
                        if (s[0] == TB_obj.Text && s[2] == (Application.Current.MainWindow as MainWindow).userName.Text)
                        {
                            check = true;
                            fileIndex = (Application.Current.MainWindow as MainWindow).objList.IndexOf(s);
                        }
                    }
                }
                else
                {
                    foreach (List<string> s in (Application.Current.MainWindow as MainWindow).objList)
                    {
                        if (s[0] == TB_obj.Text)
                        {
                            check = true;
                            fileIndex = (Application.Current.MainWindow as MainWindow).objList.IndexOf(s);
                        }
                    }
                }
                if (!check)
                {
                    MessageBox.Show("Такого файла не существует либо вы не являетесь его владельцем");
                    return;
                }
                foreach (List<string> t in (Application.Current.MainWindow as MainWindow).user)
                {
                    if (t[0]==TB_subj.Text)
                    {
                        userIndex = (Application.Current.MainWindow as MainWindow).user.IndexOf(t);
                    }
                }
                if (fileIndex != -1 && userIndex != -1)
                {
                    if ((Application.Current.MainWindow as MainWindow).userName.Text == "root")
                    {
                        if (int.Parse(tb_rights.Text) <= 15 && int.Parse(tb_rights.Text) >= 0)
                        {
                            (Application.Current.MainWindow as MainWindow).AccessMatrix[userIndex][fileIndex] = int.Parse(tb_rights.Text);
                            (Application.Current.MainWindow as MainWindow).AccessTimeMatrix[userIndex][fileIndex] = int.Parse(tb_rights.Text);
                        }
                        else
                        {
                            MessageBox.Show("Поле должно содержать число от 0 до 15");
                        }
                    }
                    else
                    {
                        List<int> trans1 = new List<int> { 15, 14, 13, 12, 11, 9, 10, 8, 7, 6, 5, 3, 4, 1, 2, 0 };
                        string stemp = Convert.ToString(trans1.IndexOf(int.Parse(tb_rights.Text)), 2);
                        string stempmax = Convert.ToString(trans1.IndexOf((Application.Current.MainWindow as MainWindow).AccessMatrix[userIndex][fileIndex]), 2);
                        string new_r;
                        while (stemp.Length < 4)
                            stemp = "0" + stemp;
                        while (stempmax.Length < 4)
                            stempmax = "0" + stempmax;
                        {
                            if (stemp[0] == '0')
                                new_r = "0";
                            else
                            {
                                if (stempmax[0] == '1')
                                    new_r = "1";
                                else
                                {
                                    MessageBox.Show("Нет привелегий для назначения прав");
                                    return;
                                }
                            }
                            if (stemp[1] == '0')
                                new_r += "0";
                            else
                            {
                                if (stempmax[1] == '1')
                                    new_r += "1";
                                else
                                {
                                    MessageBox.Show("Нет привелегий для назначения прав");
                                    return;
                                }
                            }
                            if (stemp[2] == '0')
                                new_r += "0";
                            else
                            {
                                if (stempmax[2] == '1')
                                    new_r += "1";
                                else
                                {
                                    MessageBox.Show("Нет привелегий для назначения прав");
                                    return;
                                }
                            }
                            if (stemp[3] == '0')
                                new_r += "0";
                            else
                            {
                                if (stempmax[3] == '1')
                                    new_r += "1";
                                else
                                {
                                    MessageBox.Show("Нет привелегий для назначения прав");
                                    return;
                                }
                            }
                            int resq = trans1[Convert.ToInt32(new_r, 2)];
                            (Application.Current.MainWindow as MainWindow).AccessTimeMatrix[userIndex][fileIndex] = resq;
                        }

                    }
                }
            }
            else
            {
                MessageBox.Show("Данные некорректны");
            }
            writeStreamMas("Матрица доступов.txt", (Application.Current.MainWindow as MainWindow).AccessMatrix);
            writeStreamMas("Матрица текущих доступов.txt", (Application.Current.MainWindow as MainWindow).AccessTimeMatrix);
        }
    }
}
