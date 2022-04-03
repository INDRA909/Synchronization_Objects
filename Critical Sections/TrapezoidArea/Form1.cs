using System;
using System.Threading;
using System.Windows.Forms;

namespace _2._3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        float a, b, length,length_m;
        int n,m;
        double sum;
        double f(double X) => X * X ;
        static object locker = new object();
        private void ThreadRightRectangle()
        {      
            for (int j = 0; j < n; ++j)
            {
                new Thread(new ParameterizedThreadStart(RightRectangle)).Start(j);
            }
        }
        private void RightRectangle(object i)
        {
            double left_border = a+length * (int)i;
            for (int j=0;j<=m-1;++j)
            {
                double X = left_border + j * length_m;
                lock (locker)
                {
                    sum += f(X)*length_m;
                }
            }           
        }
        private void button1_Click(object sender, EventArgs e)
        {
            float.TryParse(textBox2.Text, out a);
            float.TryParse(textBox3.Text, out b);
            int.TryParse(textBox4.Text, out m);
            int.TryParse(textBox5.Text, out n);
            length = (b - a)/n;
            length_m = length / m;
            sum = 0;
            ThreadRightRectangle();
            Thread.Sleep(200);
            textBox6.Text = sum.ToString();
        }
    }
}
