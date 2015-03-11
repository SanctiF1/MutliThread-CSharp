using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace MutliThread
{
    public partial class Form1 : Form
    {
        Thread t1, t2;

        public Form1()
        {
            InitializeComponent();

            label2.Text = DateTime.Now.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        static void ThreadJob()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Other thread: " + i.ToString (), i);
                Thread.Sleep(1000);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ThreadStart job = new ThreadStart(ThreadJob);
            Thread thread = new Thread(job);
            thread.Start();
            //for (int i = 0; i < 10; i++)
            //{
            //    label1.Text = "Main thread: " + i.ToString();
            //    Thread.Sleep(2000);
            //}

            Thread main = new Thread(ThreadMain);
            main.Start();


            

            
            
           
        }

        private void ThreadMain()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Main thread: " + i.ToString(), i);
                Thread.Sleep(2000);
                ShowThread("thread " + i.ToString());
            }
        }

        private void ThreadForm()
        {
            Form2 frm = new Form2();
            frm.Show();
            Application.Run();
        }
        private void ShowThread(string s)
        {
            if (label1.InvokeRequired)
            {
                label1.BeginInvoke(new MethodInvoker(delegate() { ShowThread(s); }));
            }
            else
            {
                lock (label1)
                {
                    label1.Text = s;                    
                }
            }
        }



        private void AppendText(string s)
        {
            if (textBox1.InvokeRequired)
            {
                textBox1.BeginInvoke(new MethodInvoker(delegate() { AppendText(s); }));
            }
            else
            {
                lock (textBox1)
                { 
                    textBox1.AppendText(s);
                    textBox1.AppendText(Environment.NewLine);
                }
            }
        }

        private void Thread1()
        {
            while (true)
            {
                AppendText("This is thread 1");
                Thread.Sleep(500);
            }
        }

        private void Thread2()
        {
            while (true)
            {
                AppendText("This is thread 2");
                Thread.Sleep(500);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            t1 = new Thread(Thread1);
            t2 = new Thread(Thread2);

            t1.Start();
            t2.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
            Thread thrform = new Thread(ThreadForm);
            thrform.Start();
        }

    }
}
