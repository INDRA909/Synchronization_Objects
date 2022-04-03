using System;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public EventHandler<double> ExpApprovedEvent;
        public double Exp(double x, int n = 0, double precision = 1e-2)
        {
            var power = Power(x, n);
            var factorial = Factorial(n);
            var curent = power / factorial;
            if (curent < precision)
            {
                return curent;
            }
            return curent + Exp(x, n + 1, precision);
        }
        public double Power(double x, int pow)
        {
            double power = Math.Pow(x, pow);
            return power;
        }
        public double Factorial(int n)
        {
            double res = 1;
            for (int i = 2; i <= n; i++)
            {
                res *= i;
            }
            return res;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ExpApprovedEvent += Trouble;          
            double x = Convert.ToDouble(textBox1.Text);     
            ExpApprovedEvent?.Invoke(this, Exp(x));
        }
        private void Trouble(object sender, double e)
        {
            MessageBox.Show($"Exp calculate! {e}");
        }
    }
}
