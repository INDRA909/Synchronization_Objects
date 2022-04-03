using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace _5._1
{
    public static class ParallelSort
    {
        public static int Counter { get; set; }
        public static void Sort<T>(T[] array, IComparer<T> comparer = null)
        {
            if (comparer == null)
                comparer = Comparer<T>.Default;
            if (array == null)
                throw new ArgumentNullException("array");
            if (array.Length == 0)
                throw new ArgumentException("array");
            if (array.Length <= Environment.ProcessorCount * 2)
            {
                Array.Sort(array, comparer);
            }
            T[] auxArray = new T[array.Length];
            int totalWorkers = 8;
            Task[] workers = new Task[totalWorkers - 1];
            int iterations = (int)Math.Log(totalWorkers, 2);
            int partitionSize = array.Length / totalWorkers;
            int remainder = array.Length % totalWorkers;
            Barrier barrier = new Barrier(totalWorkers, (b) =>
            {
                partitionSize <<= 1;
                var temp = auxArray;
                auxArray = array;
                array = temp;
            });
            Action<object> workAction = (obj) =>
            {
                int index = (int)obj;
                int low = index * partitionSize;
                if (index > 0)
                    low += remainder;
                int high = (index + 1) * partitionSize - 1 + remainder;
                Array.Sort(array, low, high - low + 1, comparer);
                barrier.SignalAndWait();
                for (int j = 0; j < iterations; j++)
                {
                    if (index % 2 == 1)
                    {
                        barrier.RemoveParticipant();
                        break;
                    }
                    int newHigh = high + partitionSize / 2;
                    index >>= 1;
                    Merge(array, auxArray, low, high, high + 1, newHigh, comparer);
                    high = newHigh;
                    barrier.SignalAndWait();
                }
            };
            for (int i = 0; i < workers.Length; i++)
            {
                workers[i] = Task.Factory.StartNew(obj => workAction(obj), i + 1);
            }
            workAction(0);
            if (iterations % 2 != 0)
                Array.Copy(auxArray, array, array.Length);
        }
        public static void MergeSort<T>(T[] array, T[] auxArray, int low, int high, IComparer<T> comparer)
        {
            Counter++;
            if (low >= high) return;
            int mid = (high + low) / 2;
            MergeSort<T>(auxArray, array, low, mid, comparer);
            MergeSort<T>(auxArray, array, mid + 1, high, comparer);
            Merge<T>(array, auxArray, low, mid, mid + 1, high, comparer);
        }
        private static void Merge<T>(T[] array, T[] auxArray, int low1, int low2, int high1, int high2, IComparer<T> comparer)
        {
            int ptr1 = low1;
            int ptr2 = high1;
            int ptr = low1;
            for (; ptr <= high2; ptr++)
            {
                if (ptr1 > low2)
                    array[ptr] = auxArray[ptr2++];
                else if (ptr2 > high2)
                    array[ptr] = auxArray[ptr1++];

                else
                {
                    if (comparer.Compare(auxArray[ptr1], auxArray[ptr2]) <= 0)
                    {
                        array[ptr] = auxArray[ptr1++];
                    }
                    else
                        array[ptr] = auxArray[ptr2++];
                }
            }
        }
        public static void QuickSort<T>(T[] array, int low, int high, IComparer<T> comparer)
        {
            Counter++;
            T pivot;
            int l_hold, h_hold;
            l_hold = low;
            h_hold = high;
            pivot = array[low];
            while (low < high)
            {
                while (comparer.Compare(pivot, array[high]) <= 0 && (low < high))
                {
                    high--;
                }
                if (low != high)
                {
                    array[low] = array[high];
                    low++;
                }
                while (comparer.Compare(pivot, array[low]) >= 0 && (low < high))
                {
                    low++;
                }

                if (low != high)
                {
                    array[high] = array[low];
                    high--;
                }
            }
            array[low] = pivot;
            int mid = low;
            low = l_hold;
            high = h_hold;
            if (low < mid)
            {
                QuickSort(array, low, mid - 1, comparer);
            }

            if (high > mid)
            {
                QuickSort(array, mid + 1, high, comparer);
            }
        }
    }
}
