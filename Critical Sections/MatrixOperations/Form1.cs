using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace _2._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        const int n=25;//кол-во потоков
        int m;//кол-во столбцов
        double sum;
        private static double[][] Mtrx = new double[n][];
        static object[] locker = new object[n];        
        Random random = new Random(DateTime.Now.Millisecond);
        private void InitMatrix()
        {
            for (int i = 0; i < n; ++i)
            {
                new Thread(new ParameterizedThreadStart(InitRows)).Start(i);
                new Thread(new ParameterizedThreadStart(SumMatrix)).Start(i);
            }
        }
        private void InitRows(object j)//создане строк
        {
            lock (locker[(int)j])
            {
                Console.WriteLine($"{j} row start creat");
                Mtrx[(int)j] = new double[m];              
                for (int i = 0; i < m; i++)
                {
                    Mtrx[(int)j][i] = (double)Math.Round(random.Next(0, 250) / 3.3, 2);
                }
                Thread.Sleep(100);
               Console.WriteLine($"{j} row created");
            }
        }
        private void SumMatrix(object j)//сумма строк
        {
            Thread.Sleep(50);
            lock (locker[(int)j])
            {
                Console.WriteLine($"                    {j} row start to sum");
                for (int i = 0; i < m; i++)
                {
                    sum+= Mtrx[(int)j][i];
                }
            }
    }
        private void ShowSumMatrix()
        {
            textBox2.Text = sum.ToString();
        }
        private void Showmatrix()
        {
            double sum2=0;
            double sum22 = 0;
            dataGridView1.RowCount = n;
            dataGridView1.ColumnCount = m;
            dataGridView2.RowCount = n+1;
            dataGridView2.ColumnCount = 1;
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < m; ++j)
                {
                    dataGridView1.Rows[i].Cells[j].Value = Mtrx[i][j].ToString();
                    sum2 += Mtrx[i][j];
                }
                sum22 += sum2;
                dataGridView2.Rows[i].Cells[0].Value =sum2.ToString();
                sum2 = 0;
            }
            dataGridView2.Rows[n].Cells[0].Value = sum22.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            for(int i=0;i<n;++i)
            {
                locker[i] = new object();
            }
            int.TryParse(textBox1.Text, out m);
            sum = 0;
            Stopwatch watch = new Stopwatch();
            watch.Start();
            InitMatrix(); 
            Showmatrix();
            Thread.Sleep(100);
            watch.Stop();
            ShowSumMatrix();           
           Console.WriteLine(
            "Время выполнения программы в миллисекундах : " + watch.ElapsedMilliseconds + "мс.\r\n");
        }
    }
}
