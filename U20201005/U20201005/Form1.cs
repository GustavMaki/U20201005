using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace U20201005
{
    public partial class Form1 : Form
    {
        SqlConnection myDBConnection = new SqlConnection();
        DataSet myData = new DataSet();

        public Form1()
        {
            InitializeComponent();
           
            myDBConnection.ConnectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=customers;Integrated Security=True";
            myDBConnection.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myData.Clear();
            SqlDataAdapter MySQLAdapter = new SqlDataAdapter("SELECT * FROM Customer", myDBConnection );
            MySQLAdapter.Fill(myData);

            listBox1.Items.Clear();
            foreach (DataRow item in myData.Tables[0].Rows) {
                listBox1.Items.Add(item[1] + " " + item[2]);
            }

            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {           
            DataRow myRow = myData.Tables[0].NewRow();
            myRow[1] = textBox1.Text;
            myRow[2] = textBox2.Text;
            myData.Tables[0].Rows.Add(myRow);

            listBox1.Items.Clear();
            foreach (DataRow item in myData.Tables[0].Rows) {
                listBox1.Items.Add(item[1] + " " + item[2]);
            }

            textBox1.Clear();
            textBox2.Clear();
            button3.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlDataAdapter MySQLAdapter = new SqlDataAdapter("SELECT * FROM Customer", myDBConnection);
            MySQLAdapter.Fill(myData);

            SqlCommandBuilder MyCmdBuilder = new SqlCommandBuilder(MySQLAdapter);
            MySQLAdapter.Update(myData);

            button2.Enabled = false;
            button3.Enabled = false;
        }
    }
}
