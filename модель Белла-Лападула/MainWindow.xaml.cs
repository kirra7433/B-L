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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace модель_Белла_Лападула
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    //0 - U 1 - C 2 - S 3 - TS
    public partial class MainWindow : Window
    {
        static List<List<string>> readStreamMas(string nameOfFile)
        {
            StreamReader sr = new StreamReader(nameOfFile, Encoding.Default);
            List<List<string>> numberList = new List<List<string>>();

            string line = string.Empty;

            while (!string.IsNullOrEmpty(line = sr.ReadLine()))
            {
                List<string> jg = new List<string>();
                string[] strs = line.Split(new[] { '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string num in strs)
                {
                    string kent = num;
                    jg.Add(kent);
                }
                numberList.Add(jg);

            }

            sr.Close();
            return numberList;
        }
        public List<List<string>> objList = readStreamMas("Объект уровень владелец.txt");
        public List<List<int>> AccessMatrix = readStreamMasInt("Матрица доступов.txt");
        public List<List<string>> user = readStreamMas("Пользователи и уровни.txt");
        public List<List<int>> AccessTimeMatrix = readStreamMasInt("Матрица текущих доступов.txt");
        static List<List<int>> readStreamMasInt(string nameOfFile)
        {
            StreamReader sr = new StreamReader(nameOfFile, Encoding.Default);
            List<List<int>> numberList = new List<List<int>>();

            string line = string.Empty;

            while (!string.IsNullOrEmpty(line = sr.ReadLine()))
            {
                List<int> jg = new List<int>();
                string[] strs = line.Split(new[] { '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string num in strs)
                {
                    int kent = int.Parse(num);
                    jg.Add(kent);
                }
                numberList.Add(jg);

            }

            sr.Close();
            return numberList;
        }

        public MainWindow()
        {
            InitializeComponent();
            CBAcObj.IsEnabled = false;
           
            textBlock2.Text += "0 - read, write, execute, append\n1 - read, write, append\n2 - read, write, execute\n3 - read, execute, append\n4 - read, write\n5 - read, execute\n6 - read, append\n7 - read\n" +
                               "8 - write, execute, append\n9 -write, append\n10 - write, execute\n11 - write\n" +
                               "12 - execute, append\n13 - execute\n14 - append\n15 - -";
            CBobj.Items.Clear();
            foreach (List<string> temp in objList)
            {
                CBobj.Items.Add(temp[0]);
            }
            for (int i = 0; i <= 3; i++)
            {
                CBAcObj.Items.Add(i);
            }
        }
        //TODO 
        //добавить изменение владельца файла

        private void button_Click(object sender, RoutedEventArgs e)// show access matrix 
        {
            if (!check_username())
                return;
            AcMa w = new AcMa();
            w.ShowDialog();
        }

        private void read_Click(object sender, RoutedEventArgs e)
        {
            if (!check_username())
                return;
            if (userName.Text != "")
            {
                string f = CBobj.SelectedValue.ToString();

                int fileIndex = -1;
                int userIndex = -1;
                foreach (List<string> s in objList)
                {
                    if (s[0].Contains(f))
                    {
                        fileIndex = objList.IndexOf(s);
                    }
                }

                foreach (List<string> t in user)
                {
                    if (t[0].Contains(userName.Text))
                    {
                        userIndex = user.IndexOf(t);
                    }
                }

                //если в матрице доступа есть право на чтение и уровень конфиденциальности объекта ниже либо равен максимальному уровню доступа субъекта, либо пользователь и есть владелец 
                //TODO ТОЛЬКО ЛИ ЧТЕНИЕ ИЛИ ЕСЛИ ЧТЕНИЕ В НАБОРЕ ПРАВИЛ
                if (userName.Text != "root")
                {
                    if (AccessMatrix[userIndex][fileIndex] <= 7 && AccessTimeMatrix[userIndex][fileIndex] <= 7 && (int.Parse(objList[fileIndex][1]) <= int.Parse(user[userIndex][2])))
                    {
                        MessageBox.Show("Чтение...");
                    }
                    else
                    {
                        MessageBox.Show("Доступ запрещен!");
                    }
                }
                else
                {
                    MessageBox.Show("Чтение...");
                }
               
            }
            else
            {
                MessageBox.Show("Введите имя пользователя!");
            }
       
        }

        private void write_Click(object sender, RoutedEventArgs e)
        {
            if (!check_username())
                return;
            if (userName.Text != "")
            {
                string f = CBobj.SelectedValue.ToString();

                int fileIndex = -1;
                int userIndex = -1;
                foreach (List<string> s in objList)
                {
                    if (s[0].Contains(f))
                    {
                        fileIndex = objList.IndexOf(s);
                    }
                }

                foreach (List<string> t in user)
                {
                    if (t[0].Contains(userName.Text))
                    {
                        userIndex = user.IndexOf(t);
                    }
                }

                //если в матрице доступа есть право на запись и уровень конфиденциальности объекта равен уровню доступа субъекта, либо пользователь и есть владелец
                if (userName.Text != "root")
                {
                        if ( ((AccessTimeMatrix[userIndex][fileIndex] <= 2) || (AccessTimeMatrix[userIndex][fileIndex] == 4) || (AccessTimeMatrix[userIndex][fileIndex] <= 11 && (AccessTimeMatrix[userIndex][fileIndex] >=8)))
                        && (int.Parse(objList[fileIndex][1]) == int.Parse(user[userIndex][1])))            
                    {
                        MessageBox.Show("Запись...");
                    }
                
                    else
                    {
                        MessageBox.Show("Доступ запрещен!");
                    }
                }
                else
                {
                    MessageBox.Show("Запись...");
                }

            }
            else
            {
                MessageBox.Show("Введите имя пользователя!");
            }
        }

        private void exec_Click(object sender, RoutedEventArgs e)
        {
            if (!check_username())
                return;
            if (userName.Text != "")
            {
                string f = CBobj.SelectedValue.ToString();

                int fileIndex = -1;
                int userIndex = -1;
                foreach (List<string> s in objList)
                {
                    if (s[0].Contains(f))
                    {
                        fileIndex = objList.IndexOf(s);
                    }
                }

                foreach (List<string> t in user)
                {
                    if (t[0].Contains(userName.Text))
                    {
                        userIndex = user.IndexOf(t);
                    }
                }

                //если в матрице доступа есть ВЫПОЛНЕНИЕ  либо пользователь и есть владелец
                if (userName.Text != "root")
                {
                    if (AccessTimeMatrix[userIndex][fileIndex] == 0 || AccessTimeMatrix[userIndex][fileIndex] == 2 || AccessTimeMatrix[userIndex][fileIndex] == 3 || AccessTimeMatrix[userIndex][fileIndex] == 5 ||
                        AccessTimeMatrix[userIndex][fileIndex] == 8 || AccessTimeMatrix[userIndex][fileIndex] == 10 || AccessTimeMatrix[userIndex][fileIndex] == 12 ||
                        AccessTimeMatrix[userIndex][fileIndex] == 13 || userName.Text == "root")
                    {
                        MessageBox.Show("Выполнение...");
                    }
                    else
                    {
                        MessageBox.Show("Доступ запрещен!");
                    }
                }
                else
                {
                    MessageBox.Show("Выполнение...");
                }

            }
            else
            {
                MessageBox.Show("Введите имя пользователя!");
            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            if (!check_username())
                return;
            if (userName.Text != "")
            {
                string f = CBobj.SelectedValue.ToString();

                int fileIndex = -1;
                int userIndex = -1;
                foreach (List<string> s in objList)
                {
                    if (s[0].Contains(f))
                    {
                        fileIndex = objList.IndexOf(s);
                    }
                }

                foreach (List<string> t in user)
                {
                    if (t[0].Contains(userName.Text))
                    {
                        userIndex = user.IndexOf(t);
                    }
                }

                //если в матрице доступа есть право на добавление и уровень конфиденциальности объекта выше либо равен текущему уровню доступа субъекта, либо пользователь и есть владелец 

                if (userName.Text != "root")
                {
                    if (AccessTimeMatrix[userIndex][fileIndex] == 0 || AccessTimeMatrix[userIndex][fileIndex] == 1 || AccessTimeMatrix[userIndex][fileIndex] == 3 || AccessTimeMatrix[userIndex][fileIndex] == 6 ||
                        AccessTimeMatrix[userIndex][fileIndex] == 8 || AccessTimeMatrix[userIndex][fileIndex] == 9 || AccessTimeMatrix[userIndex][fileIndex] == 12 ||
                        AccessTimeMatrix[userIndex][fileIndex] == 14 || userName.Text=="root")
                    {
                        MessageBox.Show("Добавление...");
                    }
                    else
                    {
                        MessageBox.Show("Доступ запрещен!");
                    }
                }
                else
                {
                    MessageBox.Show("Добавление...");
                }
            }
            else
            {
                MessageBox.Show("Введите имя пользователя!");
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

        //private void CBacSubj_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    CBacSubj.Items.Clear();

        //    int index = 0;
        //    if (userName.Text != "")
        //    {
        //        foreach (List<string> t in user)
        //        {
        //            if (t[0].Contains(userName.Text))
        //            {
        //                index = int.Parse(t[2]);
        //            }
        //        }
        //    }

        //    for (int i = index; i >= 0; i--)
        //    {

        //        CBacSubj.Items.Add(i);

        //    }
        //}

        private void CBobj_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //CBAcObj.IsEnabled = false;
            //CBAcObj.Items.Clear();
            int index = 0;
            string owner = "";
            if (userName.Text != "" )
            {
                foreach (List<string> m in objList)
                {
                    if (CBobj.SelectedValue.ToString()==m[0])
                    {
                        index = objList.IndexOf(m);
                        owner = m[2];
                    }
                }

                //foreach (List<string> t in user)
                //{
                   
                //    if (t[0]==userName.Text && int.Parse(t[2])==3)
                //    {
                //        CBAcObj.IsEnabled = true;
                //    }
                //}            

                //for (int i = 0; i <= 3; i++)
                //{

                //    CBAcObj.Items.Add(i);

                //}
            }
            if (userName.Text == "root")
            {
                btnChConfObj.IsEnabled = true;
                CBAcObj.IsEnabled = true;
            }

        }

        private void userName_TextChanged(object sender, TextChangedEventArgs e)
        {
            CBAcObj.IsEnabled = false;
            CBobj.SelectedValue = "";
            bool tru = false;
            foreach (List<string> t in user)
            {
                if (t[0]==userName.Text&& userName.Text!="")
                {
                    tru = true;
                }
            }
            if (tru)
            {
                add_del_obj.IsEnabled = true;
                //add_del_subj.IsEnabled = true;
                change_owner.IsEnabled = true;
                add_del_rights.IsEnabled = true;
                btnAccesMatrix.IsEnabled = true;
                button1.IsEnabled = true;
            }
            else
            {
                change_owner.IsEnabled = false;
                add_del_obj.IsEnabled = false;
                add_del_subj.IsEnabled = false;
                add_del_rights.IsEnabled = false;
                btnAccesMatrix.IsEnabled = false;
                button1.IsEnabled = false;
            }
            if(userName.Text == "root")
            {
                btnChConfObj.IsEnabled = true;
                CBAcObj.IsEnabled = true;
                add_del_obj.IsEnabled = true;
                add_del_subj.IsEnabled = true;
                add_del_rights.IsEnabled = true;
                button1.IsEnabled = true;
                btnAccesMatrix.IsEnabled = true;
                change_owner.IsEnabled = true;
            }
        }

        private void add_del_obj_Click(object sender, RoutedEventArgs e)
        {
            if (!check_username())
                return;
            if (userName.Text != "")
            {
                foreach (List<string> t in user)
                {
                    if (t[0]==userName.Text)
                    {
                        AddDelObj d = new AddDelObj();
                        d.ShowDialog();
                    }

                }
              
            }
            else
            {
                MessageBox.Show("Введите имя!");
            }
            CBobj.Items.Clear();
            foreach (List<string> temp in objList)
            {
                CBobj.Items.Add(temp[0]);
            }

        }

        private void add_del_subj_Click(object sender, RoutedEventArgs e)
        {
            if (!check_username())
                return;
            if (userName.Text == "root")
            {
                Add_Del_Subj s = new Add_Del_Subj();
                s.ShowDialog();
            }
            else
            {
                MessageBox.Show("Введите имя!");
            }
        }

        private void add_del_rights_Click(object sender, RoutedEventArgs e)
        {
            if (!check_username())
                return;
            Add_Del_Rights s = new Add_Del_Rights();
            s.ShowDialog();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (!check_username())
                return;
            if (userName.Text != "")
            {
                change_lvl cl = new change_lvl();
                cl.ShowDialog();
            }
            else
            {
                MessageBox.Show("Введите имя!");
            }
        }

        private bool check_username()
        {
            bool tru = false;
            foreach (List<string> t in user)
            {
                if (t[0].Contains(userName.Text))
                {
                    tru = true;
                }
            }
            if (tru || userName.Text=="root")
                return true;
            else
            {
                MessageBox.Show("Такого пользователя не существует");
                return false;
            }

        }

        private void change_owner_Click(object sender, RoutedEventArgs e)
        {
            if (!check_username())
                return;
            change_owner s = new change_owner();
            s.ShowDialog();
        }

        private void CBAcObj_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //CBAcObj.IsEnabled = false;
            
        }

        private void btnChConfObj_Click(object sender, RoutedEventArgs e)
        {
            int index = -1;
            foreach (var v in objList)
            {
                if (v[0] == CBobj.SelectedItem.ToString())
                    index = objList.IndexOf(v);
            }
            if(index!=-1)
                objList[index][1] = CBAcObj.SelectedItem.ToString();
            writeStreamMasddf("Объект уровень владелец.txt", objList);
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

        private void btnShowCurULvl_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = -1;
                foreach(var r in user)
                {
                    if (r[0] == userName.Text)
                        index = user.IndexOf(r);
                }
                if(index!=-1)
                    MessageBox.Show("Текущий: "+user[index][1]+" Максимальный: " + user[index][2]);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Укажите существующего пользователя");
            }
}

        private void btnShowCurFLvl_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = CBobj.SelectedIndex;
                MessageBox.Show("Уровень: " + objList[index][1]);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Выберите файл");
            }
        }

        private void btnShowCurFOwn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = CBobj.SelectedIndex;
                MessageBox.Show("Владелец: " + objList[index][2]);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Выберите файл");
            }
        }
    }
}
