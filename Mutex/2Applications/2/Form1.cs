using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace _3._2._2w
{
    public partial class Form1 : Form
    {
        private Mutex mutex;
        private StreamReader stream;
        private static bool flag = true;

        public Form1()
        {
            InitializeComponent();
            filePath = filePath + @"\\data.txt";
            
        }
        string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);      
        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                if (!Mutex.TryOpenExisting("Супер Мьютекс", out mutex))
                    throw new Exception("Мьютекс для продолжения совместной работы даже не создан в ведущем приложнии");
                else
                {
                   if (flag)
                   {
                    MessageBox.Show("Ожидание перехода в сигнальное положение...", "Информация");
                    mutex.WaitOne();
                    MessageBox.Show("Мьютекс освободился", "Информация");
                    flag = false;
                   }
                             
                   stream = new StreamReader(new FileStream(filePath,FileMode.Open,FileAccess.Read));                             
                   textBox1.Text = stream.ReadToEnd();                                     
                   stream.Close();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Егор", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
