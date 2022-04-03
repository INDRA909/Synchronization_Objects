using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace _3._2._1w
{
    public partial class Form1 : Form
    {
        private static Mutex mutex;
        private static StreamWriter stream;
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
                               
              stream = new StreamWriter(filePath);
              string str = textBox1.Text;
              MessageBox.Show("666");
              stream.Write(str + "\r\n");
              stream.Flush();
              stream.Close();
              MessageBox.Show("Запись осуществленна");
              if (flag)
              {
               mutex = new Mutex(true, "Супер Мьютекс");
               mutex.ReleaseMutex();
               MessageBox.Show("Мьютекс переведён в сигнальное состояние");
               flag = false;
              }
               
            }            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Егор", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }    
    }
}
