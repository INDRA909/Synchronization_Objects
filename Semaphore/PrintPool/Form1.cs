using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace _4._1
{
    public partial class Form1 : Form
    {
        private static StreamReader stream_r1, stream_r2, stream_r3;
        private static StreamWriter stream_w;
        private static Semaphore ResPool = new Semaphore(1, 2);
        private static string filePath_w = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private static string filePath_r1 = filePath_w, filePath_r2 = filePath_w,filePath_r3 = filePath_w;
        private static int n;
        private static Queue<char> pool = new Queue<char>();
        static object locker = new object();
        List<StreamReader> stream_r = new List<StreamReader>();
        int flag = 0;
        public Form1()
        {
            InitializeComponent();
            filePath_w += @"\\data_w.txt";
            filePath_r1 += @"\\data_r1.txt";
            filePath_r2 += @"\\data_r2.txt";
            filePath_r3 += @"\\data_r3.txt";
        }       
        private  void Reader(object t)
        {           
            ResPool.WaitOne();
            int i = (int)t;
            while (!stream_r[i].EndOfStream)
            {
                while ((pool.Count < n) && !stream_r[i].EndOfStream)
                {
                    pool.Enqueue((char)stream_r[i].Read());
                }
                if (stream_r[i].EndOfStream) ResPool.Release();
                Writer();
            }
            stream_w.WriteLine();
            flag++;
        }
        private void Pool()
        {
            stream_w = new StreamWriter(filePath_w);
            stream_r1 = new StreamReader(new FileStream(filePath_r1, FileMode.Open, FileAccess.Read));
            stream_r2 = new StreamReader(new FileStream(filePath_r2, FileMode.Open, FileAccess.Read));
            stream_r3= new StreamReader(new FileStream(filePath_r3, FileMode.Open, FileAccess.Read));

            stream_r = new List<StreamReader>() { stream_r1, stream_r2, stream_r3 };
            for (int j = 0; j < 3; ++j)
            {
                new Thread(new ParameterizedThreadStart(Reader)).Start(j);
            }          
        }
        private static void Writer()
        {
            lock (locker)
            {
                while (pool.Count != 0)
                {
                    stream_w.Write(pool.Dequeue());
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int.TryParse(textBox2.Text, out n);
            Pool();
            bool flag2= true;
            while(flag2)
            if(flag==3 && pool.Count==0)
            {
                 Thread.Sleep(300);
                flag2 = false;
                stream_w.Flush();
                stream_w.Close();
                MessageBox.Show("Завершенно");
            }
        }
    }
}
