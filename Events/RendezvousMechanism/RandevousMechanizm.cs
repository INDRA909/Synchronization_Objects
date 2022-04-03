using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    internal class RandevousMechanizm
    {
       public double Exp(double x, int n = 0, double precision = 1e-10)
        {
            EventClass eventClass = new EventClass();
            eventClass.MyEvent += new EventDelegate(Power(x, n));
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
            //CalcPowerApprovedEvent?.Invoke(this, power);
        }
        private double Factorial(int n)
        {
            if (n <= 1)
            {
                return 1;

            }
            return n * Factorial(n - 1);
        }
    }
}
