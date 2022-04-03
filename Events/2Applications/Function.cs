using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WinFormUI
{
    public class Function
    {
        public event EventHandler<decimal> MultApprovedEvent;
        public event EventHandler<string> RemoveDuplApprovedEvent;
        public event EventHandler<string> MostWordApprovedEvent;
        public event EventHandler<string> MinAndMaxApprovedEvent;
        public event EventHandler<string> PrimaNumbersApprovedEvent;
        public decimal Mult { get; set; }
        public string StringDupl { get; set; }
        public string StringFreqWord { get; set; }
        public string MinMax { get; set; }
        public string PrimeNum { get; set; }
        public void Multiply(decimal[] numbers)
        {
            Mult = numbers[0] * numbers[1];
            MultApprovedEvent?.Invoke(this, Mult);
        }
        public void RemoveDuplicates(List<double> list)
        {
            StringDupl = string.Join(" ", list.Distinct());
            RemoveDuplApprovedEvent?.Invoke(this, StringDupl);
        }
        public void MostFrequentWord(string str)
        {
            List<string> list = new List<string>();
            list = str.Split(' ').ToList();
            int most = list.GroupBy(i => i).OrderByDescending(grp => grp.Count())
             .Select(grp => grp.Count()).First();
            IEnumerable<string> mostVal = list.GroupBy(i => i).OrderByDescending(grp => grp.Count())
                .Where(grp => grp.Count() >= most)
                .Select(grp => grp.Key);

            StringFreqWord = string.Join(" ", mostVal)+" ";
            StringFreqWord += most.ToString();
            MostWordApprovedEvent?.Invoke(this, StringFreqWord);
        }
        private int[,] GenerateMatrix(int n)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);         
            int[,] MyMatrix = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    MyMatrix[i, j] = rnd.Next(1, 100);
                    Console.Write(MyMatrix[i,j]+" ");
                }
                Console.WriteLine();
            }                
            return MyMatrix;
        }
        private int MinMyMatrix(int[,] MyMatrix)
        {
            int min = int.MaxValue;
            for (int i = 0; i < MyMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (MyMatrix[i, j] < min) min = MyMatrix[i, j];
                }
            }
            return min;
        }
        private int MaxMyMatrix(int[,] MyMatrix)
        {
            int max = int.MinValue;
            for (int i = 0; i < MyMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < MyMatrix.GetLength(0) - i; j++)
                {
                    if (MyMatrix[i, j] > max) max = MyMatrix[i, j];
                }
            }
            return max;
        }
        public void MinAndMax(int n)
        {
            MinMax = null;
            int[,] MyMatrix = GenerateMatrix(n);
            MinMax += MinMyMatrix(MyMatrix)+" ";
            MinMax += MaxMyMatrix(MyMatrix);
            MinAndMaxApprovedEvent?.Invoke(this, MinMax);
        }
        private static List<int> SieveEratosthenes(int n)
        {
            var numbers = new List<int>();
            for (var i = 2; i < n; i++)
            {
                numbers.Add(i);
            }
            for (var i = 0; i < numbers.Count; i++)
            {
                for (var j = 2; j < n; j++)
                {
                    numbers.Remove(numbers[i] * j);
                }
            }
            return numbers;
        }
        public void PrimeNumbers(int n)
        {
            PrimeNum = string.Join(" ", SieveEratosthenes(n));
            PrimaNumbersApprovedEvent?.Invoke(this, PrimeNum);
        }
    }
}
