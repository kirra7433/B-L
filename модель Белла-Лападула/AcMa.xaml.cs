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
    /// Логика взаимодействия для AcMa.xaml
    /// </summary>
    public partial class AcMa : Window
    {
        static List<List<int>> readStreamMas(string nameOfFile)
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
        public AcMa()
        {
            InitializeComponent();
           // (Application.Current.MainWindow as MainWindow).AccessMatrix.Add(new List<int>());
            foreach (List<int> s in (Application.Current.MainWindow as MainWindow).AccessMatrix)
            {
                foreach (int t in s)
                {
                    acMatrix.Text += t.ToString()+ " ";
                };
                acMatrix.Text += "\n";
            }
            acMatrix.Text += "\n\n";
            foreach (List<int> s in (Application.Current.MainWindow as MainWindow).AccessTimeMatrix)
            {
                foreach (int t in s)
                {
                    acMatrix.Text += t.ToString() + " ";
                };
                acMatrix.Text += "\n";

            }

        }
       

    }
}
