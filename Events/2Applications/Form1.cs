using System;
using System.IO;
using System.Windows.Forms;

namespace WinFormUI
{
    public partial class Form1 : Form
    {
        Function function = new Function();
        public Form1()
        {
            InitializeComponent();
            WireUpForm();
        }
        string file = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\\5.4.2w.txt";
        private void WireUpForm()
        {
            textBox2.Text = function.Mult.ToString();
            textBox1.Text = function.StringFreqWord;
            textBox3.Text = function.MinMax;
            textBox5.Text = function.PrimeNum;
            function.MultApprovedEvent += Function_MultApprovedEvent;
            function.RemoveDuplApprovedEvent += Function_RemoveDuplApprovedEvent;
            function.PrimaNumbersApprovedEvent += Function_PrimaNumbersApprovedEvent;
            function.MostWordApprovedEvent += Function_MostWordApprovedEvent;
            function.MinAndMaxApprovedEvent += Function_MinAndMaxApprovedEvent;
            Form2 client = new Form2(function);
            client.Show();
        }

        private void Function_MinAndMaxApprovedEvent(object sender, string e)
        {
            textBox3.Text = e;
        }

        private void Function_MostWordApprovedEvent(object sender, string e)
        {
            textBox1.Text = e;
        }

        private void Function_PrimaNumbersApprovedEvent(object sender, string e)
        {
            textBox5.Text = e;
        }

        private void Function_RemoveDuplApprovedEvent(object sender, string e)
        {
            using (StreamWriter sw = new StreamWriter(file, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(e);
            }
        }

        private void Function_MultApprovedEvent(object sender, decimal e)
        {
            textBox2.Text = e.ToString();
        }

    }
}
