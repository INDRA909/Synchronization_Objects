using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
namespace _1._4
{

    public partial class Form1 : Form
    {
        int x, y, h=100;//координаты,высота-ширина круга
        Pen pen1 = new Pen(Color.Red, 100);
        Pen pen2 = new Pen(Color.Green, 100);
        Pen pen3 = new Pen(Color.Yellow, 100);
        Random rand = new Random();
        int amt_g, amt_y, amt_r;
        Thread circle1;
        Thread circle2;
        Thread circle3;
        Graphics g;
        Thread[] ThreadPrority = new Thread[5];
        public ThreadPriority[] prorities = // Массив потков
        {
            ThreadPriority.Lowest,
            ThreadPriority.BelowNormal,
            ThreadPriority.Normal,
            ThreadPriority.AboveNormal,
            ThreadPriority.Highest
        };
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load_1(object sender, EventArgs e)
        {            
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;           
        }
        private void Red()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();            
            for (int i = 0; i < amt_r; ++i)
            {
                x = rand.Next(100, 400);
                y = rand.Next(100, 400);
                Rectangle rect = new Rectangle(x, y, h, h);
                g = this.CreateGraphics();
                g.DrawEllipse(pen1, rect);
                Invoke((Action)(() =>
                {
                    progressBar3.Value++;

                }));
                Thread.Sleep(200);
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("Red" + ts);
        }
        private void Green()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            for (int i = 0; i < amt_g; ++i)
            {
                x = rand.Next(100, 400);
                y = rand.Next(100, 400);
                Rectangle rect = new Rectangle(x, y, h, h);
                g = this.CreateGraphics();
                g.DrawEllipse(pen2, rect);
                Invoke((Action)(() =>
                {
                    progressBar1.Value++;
                }));
                Thread.Sleep(200);
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("Green "+ ts);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
        }
        private void Yellow()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            for (int i = 0; i < amt_y; ++i)
            {
                //Console.WriteLine("Yellow  circle on display");
                    x = rand.Next(100, 400);
                    y = rand.Next(100, 400);
                    Rectangle rect = new Rectangle(x, y, h, h);
                    g = this.CreateGraphics();
                    g.DrawEllipse(pen3, rect);
                Invoke((Action)(() =>
                {
                    progressBar2.Value++;
                }));
                Thread.Sleep(200);
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("Yellow "+ts);
        }

           //По умолчанию потоку задается значение Normal.
            //Однако мы можем изменить приоритет в процессе работы программы. 
            //Например, повысить важность потока, установив приоритет Highest.
            //Среда CLR будет считывать и анализировать значения приоритета и на их 
            //основании выделять данному потоку то или иное количество времени.
        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            progressBar2.Value = 0;
            progressBar3.Value = 0;  
            int.TryParse(textBox1.Text, out amt_g);
            int.TryParse(textBox5.Text, out amt_y);
            int.TryParse(textBox6.Text, out amt_r);
            progressBar1.Maximum = amt_g;
            progressBar2.Maximum = amt_y;
            progressBar3.Maximum = amt_r;
            circle2 = new Thread(Yellow);
            circle3 = new Thread(Red);
            circle1 = new Thread(Green);
            circle1.Name = "Green";
            circle2.Name = "Yellow";
            circle3.Name = "Red";
            Console.WriteLine("Thread starts...");
            circle1.Priority = prorities[comboBox1.SelectedIndex];
            circle2.Priority = prorities[comboBox2.SelectedIndex];
            circle3.Priority = prorities[comboBox3.SelectedIndex];
            circle1.Start();
            circle2.Start();
            circle3.Start();
            textBox2.Text = Convert.ToString(circle1.Priority);
            textBox3.Text = Convert.ToString(circle2.Priority);
            textBox4.Text = Convert.ToString(circle3.Priority);      
        }
    }
}
