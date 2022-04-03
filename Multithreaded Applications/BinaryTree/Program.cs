using System;
using System.Collections;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace _1._5
{    
    public class SumData
    {
        public BinaryTree three { get; set; }
        public int sum { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int sum=0,number=0;  //сумма, кол-во         
            Random rnd = new Random(DateTime.Now.Millisecond); 
            var tree = new BinaryTree();
            Console.Write("Введите кол-во элементов дерева: ");
            int amt = Convert.ToInt32( Console.ReadLine());
            int[] elems = new int[amt];
            for (int i = 0; i < amt; ++i)
            {
                elems[i] = rnd.Next(1, 20);
                tree.Insert(elems[i]);
            }
            BinaryTreeExtensions.Print(tree);
            Thread thread1 = new Thread(delegate ()
            {
                tree.Sum(tree, ref sum);
            });
            Thread thread2 = new Thread(delegate ()
            {
                tree.Count(tree, ref number);
            });
            thread1.Start();
            thread2.Start();
            elems.ToList().ForEach(i => Console.Write(i+ " "));
            Console.WriteLine();
            Console.WriteLine("Сумма элементов дерева: " + sum);
            Console.WriteLine("Кол-во элементов дерева: " + number);
            Console.WriteLine("Среднее значение: " + sum/number);
        }
        
    } 
    public enum BinSide
    {
        Left,
        Right
    }
    public class BinaryTree
    {
        public long? Data { get; private set; }
        public BinaryTree Left { get; set; }
        public BinaryTree Right { get; set; }
        public BinaryTree Parent { get; set; }
        public void Sum(BinaryTree node,ref int sum)
        {         
            if (node != null)
            {
            sum+= (int)(node.Data);
            Sum(node.Left,ref sum);
            Sum(node.Right,ref sum); 
            }
        }

        public void Count(BinaryTree node, ref int number)
        {
            if (node != null)
            {
                number ++;
                Count(node.Left, ref number);
                Count(node.Right, ref number);
            }
        }

        public void Insert(long data)
        {
            if (Data == null || Data == data)
            {
                Data = data;
                return;
            }
            if (Data > data)
            {
                if (Left == null) Left = new BinaryTree();
                Insert(data, Left, this);
            }
            else
            {
                if (Right == null) Right = new BinaryTree();
                Insert(data, Right, this);
            }
        }
        private void Insert(long data, BinaryTree node, BinaryTree parent)
        {

            if (node.Data == null || node.Data == data)
            {
                node.Data = data;
                node.Parent = parent;
                return;
            }
            if (node.Data > data)
            {
                if (node.Left == null) node.Left = new BinaryTree();
                Insert(data, node.Left, node);
            }
            else
            {
                if (node.Right == null) node.Right = new BinaryTree();
                Insert(data, node.Right, node);
            }
        }
    }
    public class BinaryTreeExtensions
    {
        public static void Print(BinaryTree node)
        {
            if (node != null)
            {
                if (node.Parent == null)
                {
                    Console.WriteLine("ROOT:{0}", node.Data);
                }
                else
                {
                    if (node.Parent.Left == node)
                    {
                        Console.WriteLine("Left for {1}  --> {0}", node.Data, node.Parent.Data);
                    }

                    if (node.Parent.Right == node)
                    {
                        Console.WriteLine("Right for {1} --> {0}", node.Data, node.Parent.Data);
                    }
                }
                if (node.Left != null)
                {
                    Print(node.Left);
                }
                if (node.Right != null)
                {
                    Print(node.Right);
                }
            }
        }
    }
}
