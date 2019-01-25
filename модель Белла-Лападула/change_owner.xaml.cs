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
    /// Логика взаимодействия для change_owner.xaml
    /// </summary>
    public partial class change_owner : Window
    {
        public change_owner()
        {
            InitializeComponent();
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            int fileIndex = -1;
            int userIndex = -1;
            bool check = false;
            if (TB_obj.Text != "" && TB_subj.Text != "")
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
                    if (t[0] == TB_subj.Text)
                    {
                        userIndex = (Application.Current.MainWindow as MainWindow).user.IndexOf(t);
                    }
                }
                if (fileIndex != -1 && userIndex != -1)
                {
                    (Application.Current.MainWindow as MainWindow).objList[fileIndex][2] = TB_subj.Text;
                    MessageBox.Show("Владелец успешно изменен");
                }
                else
                    MessageBox.Show("Неверно заданы параметры");
            }
            writeStreamMasddf("Объект уровень владелец.txt", (Application.Current.MainWindow as MainWindow).objList);
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
    }
}
