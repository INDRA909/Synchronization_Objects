using System;
using System.Threading;
using System.Windows.Forms;
namespace _3._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static double[] vector_1;
        static double[] vector_2;
        string[] str_1;
        string[] str_2;
        static double scalar;
        static Mutex mutexObj = new Mutex();
        private void MultScalar(int n)
        {
            for(int i=0;i<n;++i)
            {
                new Thread(new ParameterizedThreadStart(Scalar)).Start(i);
            }
        }
        private void Scalar(object j)
        {
            mutexObj.WaitOne();
            int i = (int)j;
            double.TryParse(str_1[i], out vector_1[i]);
            double.TryParse(str_2[i], out vector_2[i]);
            scalar += vector_1[i] * vector_2[i];
            mutexObj.ReleaseMutex();
        }
        private void button1_Click(object sender, EventArgs e)
        {               
            str_1 = textBox1.Text.Split(';');
            str_2 = textBox2.Text.Split(';');
            if (str_1.Length != str_2.Length) MessageBox.Show("Размерности векторов должнны совпадать");
            else
            {
              scalar = 0;           
              vector_1 = new double[str_1.Length];
              vector_2 = new double[str_2.Length];
              MultScalar(str_1.Length);
              Thread.Sleep(250);
              textBox3.Text = scalar.ToString();
            }
        }
    }
}
