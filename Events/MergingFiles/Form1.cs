using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _5._3
{
    public partial class Form1 : Form
    {
        public static string file_path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\5.3\{0}.txt";
        static List<Reader> readers;
        static Merger merger;

        static List<EventWaitHandle> handles = new List<EventWaitHandle>();

        static bool merger_closed = false;
        public Form1()
        {
            InitializeComponent();
        }
        public void AppendTextBox(string value)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(AppendTextBox), new object[] { value });
                return;
            }
           textBox1.Text += value;
        }
        class Reader
        {
            int index; int current_num_in_file = int.MinValue;
            Thread thr_reader;
            StreamReader stream_reader;
            public Reader(int index)
            {
                this.index = index;
                stream_reader = new StreamReader(String.Format(file_path, index));
                thr_reader = new Thread(ReadFromFile);
            }

            public void StartRead()
            {
                thr_reader.Start();
            }

            private void ReadFromFile()
            {
                while (!merger_closed)
                {
                    handles[index].WaitOne();
                    if (stream_reader.EndOfStream)
                    {
                        current_num_in_file = int.MaxValue;
                    }
                    else
                    {
                        current_num_in_file = Convert.ToInt16(stream_reader.ReadLine());
                    }
                    handles[index].Set();
                    Thread.Sleep(5);
                }

            }
            public int CurrentNum { get { return current_num_in_file; } set { current_num_in_file = value; } }
        }

        class Merger
        {
            StreamWriter stream_writer;
            Thread thr_merger;
            Form1 form = (Form1)Application.OpenForms["Form1"];
            public Merger(int readers_amnt)
            {
                stream_writer = new StreamWriter(String.Format(file_path, "result"));
                thr_merger = new Thread(MergeFiles);
            }
            public void StartMerge()
            {
                thr_merger.Start();
            }
            private void MergeFiles()
            {
                while (true)
                {
                    WaitForAllHandles();
                    //Console.WriteLine("[merger] дождался...");
                    string nums_to_write = "";
                    int amnt_fictions = 0;

                    foreach (Reader r in readers)
                    {
                        if (r.CurrentNum != int.MaxValue)
                        {
                            if (r.CurrentNum != int.MinValue)
                            {
                                nums_to_write += $"{r.CurrentNum} ";
                                r.CurrentNum = int.MinValue;
                            }
                        }
                        else amnt_fictions++;
                    }
                    if (amnt_fictions == readers.Count)
                    {
                        merger_closed = true;
                        stream_writer.Close();
                        //Console.Write("[merger] вышел");
                        return;
                    }
                    form.AppendTextBox(nums_to_write);
                    stream_writer.Write(nums_to_write);
                    SetAllHandles();
                }
            }
           
        }
        static public void WaitForAllHandles()
        {
            foreach (EventWaitHandle ev in handles) ev.WaitOne();
        }
        static public void SetAllHandles()
        {
            foreach (EventWaitHandle ev in handles) ev.Set();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            int files_amnt = 4;

            readers = new List<Reader>();
            merger = new Merger(files_amnt);
            for (int i = 0; i < files_amnt; ++i)
            {
                handles.Add(new EventWaitHandle(true, EventResetMode.ManualReset));
                readers.Add(new Reader(i));
            }
            merger.StartMerge();
            for (int i = 0; i < files_amnt; ++i)
                readers[i].StartRead();
        }

    }

}

