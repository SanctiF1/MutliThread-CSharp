using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;

namespace MutliThread
{
    public partial class Form2 : Form
    {
        private string sCon;
        private bool chkRun;
        private DataTable dt;
        private Thread t;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
            
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            if (t == null || (t.ThreadState != ThreadState.Running && t.ThreadState != ThreadState.WaitSleepJoin ))
            {
                t = new Thread(getInfo);
                t.Start();
                btnStatus.Text = "Stop";
            }
            else
            {                
                t.Abort();
                btnStatus.Text = "Start";
            }
           
            
        }

        private void getInfo()
        {
            while (true)
            {
                Thread.Sleep(5000);
                    dt = new DataTable();
                    sCon = "Server= 10.0.10.21;Database=POSCOMMON;User Id=sa;Password=pwd#123;";
                    SqlConnection con;
                    SqlCommand command;
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();

                    con = new SqlConnection(sCon);
                    con.Open();
                    command = new SqlCommand(" select ROW_NUMBER() over(order by createdDate desc) as stt,  CreatedDate, id,DocEntry, Transtype, ShopCode, Docdate, Remarks,Company,  Messages, IsSuccess from POSSyncSaleLog order by createdDate desc ", con);
                    command.CommandType = CommandType.Text;
                    sqlDataAdapter.SelectCommand = command;
                    sqlDataAdapter.Fill(dt);
                    con.Close();
                    displayData(dt);
            };
            //dataGridView1.DataSource = dt;
            //dataGridView1.Refresh();
        }

        private void displayData(DataTable s)
        {
            if (dataGridView1 .InvokeRequired)
            {
                dataGridView1.BeginInvoke(new MethodInvoker(delegate() { displayData(s); }));
            }
            else
            {
                lock (dataGridView1)
                {
                    dataGridView1.DataSource = s;
                    dataGridView1.Refresh();
                }
            }
        }
        
    }
}
