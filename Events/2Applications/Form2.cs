using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WinFormUI
{
    public partial class Form2 : Form
    {
        private Function _function;
        public Form2(Function function)
        {
            InitializeComponent();
            _function = function;
        }
          
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                decimal[] numbers = new decimal[2];
                numbers[0] = Convert.ToInt32(textBox1.Text);
                numbers[1] = Convert.ToInt32(textBox2.Text);
                _function.Multiply(numbers);
            }
            if (checkBox2.Checked)
            {
                List<double> list = new List<double>();
                list = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\\5.4.2r.txt").Split(' ').Select(Convert.ToDouble).ToList();
                _function.RemoveDuplicates(list);
            }
            if (checkBox3.Checked)
            {
                string str = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\\5.4.3r.txt");
                _function.MostFrequentWord(str);
            }
            if (checkBox4.Checked)
            {
                int n = Convert.ToInt32(textBox4.Text);
                _function.MinAndMax(n);
            }
            if (checkBox5.Checked)
            {
                int n = Convert.ToInt32(textBox5.Text); _function.PrimeNumbers(n);
            }
        }
    }
}
