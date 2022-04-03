using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace _5._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }       
    private void button1_Click(object sender, EventArgs e)
        {
            
            int[] array = textBox1.Text.Split(';').Select(n => Convert.ToInt32(n)).ToArray();
            ParallelSort.Sort(array);
            textBox1.Text += Environment.NewLine;
            foreach (var r in array) textBox1.Text += $"{r};";           
        }
    }
}
