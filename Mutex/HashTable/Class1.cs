using System;
using System.Collections.Generic;
using System.Threading;

namespace _3._3
{
    public class HashTable<T>
    {
        List<List<int>> items;
        int K;
        List<Mutex> mutex = new List<Mutex>();
        public HashTable(int size,int K)
        {
            items = new List<List<int>>();
            this.K = K;

            for(int i=0;i<size;++i)
            {
                items.Add(new List<int>());
                mutex.Add(new Mutex(false));
            }
        }
        public void Add(int item)
        {
            
            var key = GetHash(item);
            mutex[key].WaitOne();
            items[key].Add(item);
            mutex[key].ReleaseMutex();
        }    
        private int GetHash(int item)
        {
            return item % K;
        }
        public void Show()
        {
            for (int i=0;i<items.Count;++i)
            {
                for (int j = 0; j < items[i].Count; ++j)
                {
                    Console.Write(items[i][j] + "\t" );
                }
                Console.WriteLine();
            }
            
        }
    }
}
