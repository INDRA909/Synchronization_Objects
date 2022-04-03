using System;
using System.Threading;
using System.Windows.Forms;
namespace _3._3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static int n, k;
        Random rnd = new Random(DateTime.Now.Millisecond);

        HashTable<int> HashTable1;
        private void Generate()
        {
            HashTable1 = new HashTable<int>(n, k);
            for (int i = 0; i < n; ++i)
            {
                new Thread(GenerateNumb).Start();
            }
        }       
        private void GenerateNumb(object HashTable)
        {
            for (int i = 0; i < n; ++i)
            {              
                int numb = rnd.Next(1, 200);
                HashTable1.Add(numb);
                BeginInvoke((Action)(() =>
                {
                    progressBar1.Value++;
                }));
            }
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            Console.Clear();
            HashTable1.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            int.TryParse(textBox1.Text, out n);
            int.TryParse(textBox1.Text, out k);
            progressBar1.Minimum = 0;
            progressBar1.Maximum = n * n;
            Generate();
        }
    }
}
