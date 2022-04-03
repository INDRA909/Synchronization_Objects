using System;
using System.Threading;
using System.Windows.Forms;

namespace _1._67
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string key,text_n;
        string[] block;
        int num_block,k;
        bool flag;
        private void makeThread()//создание потоков шифровки
        {
            for (int i = 0; i < num_block; ++i)
            {
                new Thread(new ParameterizedThreadStart(encryption)).Start(i);
            }
            Thread.Sleep(200);
        }
        private void encryption(object i)// шифровка блока
        {
                char[] blockchar = block[(int)i].ToCharArray();// блок шифровки
                char[] keychar = key.ToCharArray();// ключ
                for (int j = 0; j < block[(int)i].Length; ++j)
                {
                blockchar[j]= (char)(blockchar[j] ^ keychar[j]);
                }
                block[(int)i] = new string(blockchar);                             
        } 
        private void separation(string text)
        {         
            num_block = ((text.Length % k)==0) ? text.Length / k : text.Length / k+1;// кол-во блоков
            block=new string[num_block];    //массив блоков    
            for (int i = 0; i < num_block; ++i)
            {
              for(int j=0;j<k && (i*k+j)<text.Length;++j)
              {
                    block[i] += text[i * k + j];
              }             
            }
        }
        private void Union()// Обьединение
        {
            text_n="";
            for (int i = 0; i < num_block; ++i)
            {
                text_n += block[i];
            }
        }
        private void permutation()// gthtcnfyjdrf
        {           
            for (int i = 0; i < num_block; ++i)
            {
                char[] outputString = block[i].ToCharArray();
                Array.Reverse(outputString);
                block[i] = new string(outputString);
            }
        }

        private void button2_Click(object sender, EventArgs e) // Очистка формы
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        //private void OUT()
        //{
        //    for (int i = 0; i < num_block; ++i)
        //    {                
        //        if (flag) textBox2.Text += block[i]+ " ";
        //        else textBox3.Text += block[i] + " ";
        //    }
        //}
        private void Зашифровать_Click(object sender, EventArgs e)
        {
            flag = true;
            string text = textBox1.Text;          
            key = textBox4.Text;
            k = key.Length;
            separation(text);
            //OUT();
            //textBox2.Text += Environment.NewLine;
            permutation();
            //OUT();
            //textBox2.Text += Environment.NewLine;
            makeThread();
            //OUT();                 
            //textBox2.Text += Environment.NewLine;
            Union();
            Console.WriteLine(text_n);
            textBox2.Text +=text_n;
            textBox2.Text += Environment.NewLine;
        }
       private void button1_Click(object sender, EventArgs e)
        {
            flag = false;
            key = textBox5.Text;
            k = key.Length;
            separation(text_n);
            //OUT();
            //textBox3.Text += Environment.NewLine;
            makeThread();
            //OUT();
            //textBox3.Text += Environment.NewLine;
            permutation();
            for (int i = 0; i < num_block; ++i)
            {
              textBox3.Text += block[i] ;
               Console.Write(block[i]);
            }
            textBox3.Text += Environment.NewLine;
            //OUT();
            //textBox3.Text += Environment.NewLine;
        }
    }
}
