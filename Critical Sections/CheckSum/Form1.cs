using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace _2._2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string str;
        int k;
        byte check;
        byte[] data;
        Random rnd = new Random(DateTime.Now.Millisecond);
        static object locker = new object();
        private void CheckSum()
        {
            k = rnd.Next(1,str.Length);
            data = Encoding.ASCII.GetBytes(str);           
            for (int i = 0; i < k; ++i)
            {
                new Thread(new ParameterizedThreadStart(CheckSum)).Start(i);
            }
        }
        private void CheckSum(object j)
        {
           for (int s = 0; ((int)j + k * s) < str.Length; ++s)
           {
              lock (locker)
              {
                    check += data[((int)j + k * s)];
              }
           }
        }
        private void button1_Click(object sender, EventArgs e)
        {
           check = 0;
           str = textBox1.Text;
           CheckSum();
            Thread.Sleep(80);
            textBox2.Text = check.ToString();
        }
    }
}
