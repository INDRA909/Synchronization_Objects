using System;
using System.Threading;
using System.Windows.Forms;

namespace _1._3
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();     
        }
        int[] col_sums; //Массив сумм
        int[,] Mtrx;//Матрица m на n
        int m, n,k,a,b,c; // n- строк
                          // m- столбцов
                          // k- множитель
                          // a, b, с- прогресс бар потока
                          
        private void InitRows(object j) //Создание строк
        {
            Random random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < m; i++)
            {
                Mtrx[(int)j, i] = random.Next(0, 38);
                BeginInvoke((Action)(() =>
                {
                    if ((int)j == (a-1)) progressBar1.Value++;
                    if ((int)j == (b-1)) progressBar2.Value++; 
                    if ((int)j == (c - 1)) progressBar3.Value++;
                }));
            }
        }
        private void InitMatrix()//Создание матрицы
        {
            for (int j = 0; j < n; ++j)
            {
                new Thread(new ParameterizedThreadStart(InitRows)).Start(j);
            }
            Thread.Sleep(100);
        }
        private void ShowMatrix(int[,] matrix) //Вывод матрицы в датагрид
        {
            dataGridView1.RowCount = n;
            dataGridView1.ColumnCount = m;
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < m; ++j)
                {
                    dataGridView1.Rows[i].Cells[j].Value = matrix[i, j].ToString();
                }
            }
        }
        private void Mult() //Умножение матрицы на множитель
        {
            for (int j = 0; j < n; ++j)
            {
                new Thread(new ParameterizedThreadStart(MultRows)).Start(j);               
            }
            Thread.Sleep(100);
        }
        private void MultRows(object j) //Умножение строк
        {           
            for (int i = 0; i < m; i++)
            {
                Mtrx[(int)j, i] *= k; 
                BeginInvoke((Action)(() =>
                {
                    if ((int)j == (a-1)) progressBar1.Value++;
                    if ((int)j == (b-1)) progressBar2.Value++;
                    if ((int)j == (c-1)) progressBar3.Value++;
                }));
            }
        }
        private void Sort()//Сортировка матрицы
        {
            for (int j = 0; j < n; ++j)
            {
                new Thread(new ParameterizedThreadStart(SortRows)).Start(j) ;
            }
            Thread.Sleep(100);
        }
        private void SortRows(object j)//Сортировка матрицы
        {
            CountingSort((int)j);//Сортировка подсчётом
        }
        private  void CountingSort(int j)
        {
                //поиск минимального и максимального значений
         var min = Mtrx[j,0];
         var max = Mtrx[j,0];
         for (int i = 0; i < n; ++i) 
         {
            if (Mtrx[j,i] > max)
            {
                max = Mtrx[j, i];
            }
            else if (Mtrx[j, i] < min)
            {
                min = Mtrx[j, i];
            }
         }

                //поправка
         var correctionFactor = min != 0 ? -min : 0;
         max += correctionFactor;

         var count = new int[max + 1];
         for (var i = 0; i < n; i++)
         {
            count[Mtrx[j, i] + correctionFactor]++;
         }

         var index = 0;
         for (var i = count.Length-1; i>=0; i--)
         {
            for (var k = 0; k < count[i]; k++)
            {
               Mtrx[j,index] = i - correctionFactor;
                index++;
            }
         }
        }
       
        private void Sum()//Поис суммы
        {
            col_sums = new int[n];
            for (int j = 0; j < n; ++j)
            {
                new Thread(new ParameterizedThreadStart(SumRowVolum)).Start(j);
            }
            Thread.Sleep(100);
        }
        private void MinSum()//Поиск минимальной суммы
        {
            int num_min = 0;
            int min = col_sums[0];
            for (int i = 1; i < n; i++)
            {
                if (min > col_sums[i])
                {
                    min = col_sums[i];
                    num_min = i;
                }
            }
            textBox7.Text = Convert.ToString(min);
        }
        private void SumRowVolum(object j)//Сумма по строке
        { 
            col_sums[(int)j] = 0;
            for (int i = 0; i < m; i++)
            {
                col_sums[(int)j] += Mtrx[(int)j,i];
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            
            int.TryParse(textBox1.Text, out n);
            int.TryParse(textBox2.Text, out m);
            int.TryParse(textBox4.Text, out a);
            int.TryParse(textBox5.Text, out b);
            int.TryParse(textBox6.Text, out c);
            Mtrx = new int[n,m];
            progressBar1.Maximum = m;
            progressBar2.Maximum = m;
            progressBar3.Maximum = m;
            progressBar1.Value = 0;
            progressBar2.Value = 0;
            progressBar3.Value = 0;
            InitMatrix();
            ShowMatrix(Mtrx);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            progressBar2.Value = 0;
            progressBar3.Value = 0;
            int.TryParse(textBox4.Text, out a);
            int.TryParse(textBox5.Text, out b);
            int.TryParse(textBox6.Text, out c);
            int.TryParse(textBox3.Text, out k);
            Mult();
            ShowMatrix(Mtrx);
        }      
        private void button3_Click(object sender, EventArgs e)
        {
            Sort();
            ShowMatrix(Mtrx);
        }   
        private void button4_Click(object sender, EventArgs e)
        {
            Sum();
            MinSum();          
        }
    }
}
