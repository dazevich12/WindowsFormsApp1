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

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {

        Form1 form1 = new Form1();

        public SqlConnection connection;
        public Form2()
        {
            InitializeComponent();
        }

        public void getConnection(SqlConnection connection)
        {
            this.connection = connection;
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private async void Button2_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("INSERT INTO [Invoice] (date, client, money)VALUES(@date, @client, @money)", this.connection);

            command.Parameters.AddWithValue("date", textBox2.Text);
            command.Parameters.AddWithValue("client", textBox1.Text);
            command.Parameters.AddWithValue("money", textBox3.Text);

            await command.ExecuteNonQueryAsync();

            this.connection.Close();

            form1.Show();
            Close();
            

        }
    }
}
