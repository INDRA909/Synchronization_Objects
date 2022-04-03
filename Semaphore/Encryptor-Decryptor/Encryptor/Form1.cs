using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace _4._2._1
{
    public partial class Form1 : Form
    {
        private static StreamReader stream_r;
        private static StreamWriter stream_w;
        private static Semaphore semaphore;
        private static string filePath_r = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private static string filePath_w = filePath_r;
        static int m, n;
        static List<MyType> buffer = new List<MyType>();
        private static Mutex mutex;
        private static bool flag = true;
        public Form1()
        {
            InitializeComponent();
            filePath_r += @"\\data_cr.txt";
            filePath_w += @"\\data_cw.txt";
        }
        public class MyType
        {
            public char symbol;
            public bool search;
        }
        private void Encrypter()
        {
            for (int i = 0; i < m; ++i)
            {
                new Thread(new ParameterizedThreadStart(Encrypt)).Start(i);
            }            
        }
        private static bool Search(int index)
        {
            
            MyType elem = buffer.Find(x => x.search == false);
            if (buffer.Contains(elem))
            {
                index = buffer.FindIndex(x => x.search == false);
                buffer[index].search = true;
                return true;
            }
            else return false;
        }
        private static void Writer()
        {
            while (buffer.Count != 0)
            {
                stream_w.Write(buffer[0].symbol);
                buffer.RemoveAt(0);
            }
        }
        private static void Encrypt(object i)
        {
               int  index = (int)i;
            semaphore.WaitOne();
            if ((int)i < buffer.Count)
            {
                if (char.IsLower(buffer[index].symbol) && buffer[index].symbol >= 'a' && buffer[index].symbol <= 'z')
                {
                    if (buffer[index].symbol == 'z') buffer[index].symbol = 'a';
                    else buffer[index].symbol++;
                    buffer[index].symbol = char.ToUpper(buffer[index].symbol);
                }
                else if (buffer[index].symbol >= 'A' & buffer[index].symbol <= 'Z')
                {
                    if (buffer[index].symbol == 'Z') buffer[index].symbol = 'A';
                    else buffer[index].symbol++;
                    buffer[index].symbol = char.ToLower(buffer[index].symbol);
                }
            }
               semaphore.Release();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                int.TryParse(textBox2.Text, out m);
                int.TryParse(textBox3.Text, out n);
                semaphore = new Semaphore(m, m);
                stream_w = new StreamWriter(filePath_w);
                stream_r = new StreamReader(new FileStream(filePath_r, FileMode.Open, FileAccess.Read));

                textBox1.Text = stream_r.ReadToEnd();
                stream_r.BaseStream.Position = 0;

                while (!stream_r.EndOfStream)
                {
                    while ((buffer.Count < m) && !stream_r.EndOfStream)
                    {
                        buffer.Add(new MyType() { symbol = (char)stream_r.Read(), search = false });
                    }
                    Encrypter();
                    textBox1.Text += " ";
                    Thread.Sleep(200);
                    foreach (var item in buffer)
                        textBox1.Text += item.symbol.ToString();
                    Writer();
                }
                stream_w.Flush();
                stream_w.Close();
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
