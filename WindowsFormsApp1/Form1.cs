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
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection;
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "database1DataSet.Invoice". При необходимости она может быть перемещена или удалена.
            this.invoiceTableAdapter.Fill(this.database1DataSet.Invoice);
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Егор\source\repos\WindowsFormsApp1\WindowsFormsApp1\Database1.mdf;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();


            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [Invoice]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                   listBox1.Items.Add("("+Convert.ToString(sqlReader["Id"])+""+") "+Convert.ToString(sqlReader["client"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if(sqlReader != null)
                {
                    sqlReader.Close();
                }
            }
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            string client_request = textBox1.Text;
            for (int item = 0; item < listBox1.Items.Count; item++)
            {
                string str = Convert.ToString(listBox1.Items[item]);
                if(str.Contains(client_request))
                {
                    listBox2.Items.Add(listBox1.Items[item]);
                }
            }
        }

        private async void TextBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Введите фио...")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
            listBox1.Visible = false;
            listBox2.Visible = true;

        }

        private void TextBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Введите фио...";
                textBox1.ForeColor = Color.LightGray;
                listBox1.Visible = true;
                listBox2.Visible = false;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.getConnection(sqlConnection);
            form2.Show();
            Hide();
        }

        private async void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataReader sqlReader = null;
            string text = Convert.ToString(listBox1.GetItemText(listBox1.SelectedItem));
            int firstIndex = text.IndexOf("(") + 1;
            int lastIndex = text.IndexOf(")") - 1;
            int lenght = lastIndex - firstIndex + 1;
            string id = text.Substring(firstIndex, lenght);
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            button4.Visible = true;
            button5.Visible = true;
            try
            {
                SqlCommand command = new SqlCommand(" SELECT * FROM [Invoice] WHERE [Id]=" + id, sqlConnection);
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    label6.Text = Convert.ToString(sqlReader["id"]);
                    label7.Text = Convert.ToString(sqlReader["client"]);
                    label8.Text = Convert.ToString(sqlReader["date"]);
                    label9.Text = Convert.ToString(sqlReader["money"]);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                {
                    sqlReader.Close();
                }
            }
        }

        private async void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataReader sqlReader = null;
            string text = Convert.ToString(listBox2.GetItemText(listBox2.SelectedItem));
            int firstIndex = text.IndexOf("(") + 1;
            int lastIndex = text.IndexOf(")") - 1;
            int lenght = lastIndex - firstIndex + 1;
            string id = text.Substring(firstIndex, lenght);
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            button4.Visible = true;

            try
            {
                SqlCommand command = new SqlCommand(" SELECT * FROM [Invoice] WHERE [Id]=" + id, sqlConnection);
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    label6.Text = Convert.ToString(sqlReader["id"]);
                    label7.Text = Convert.ToString(sqlReader["client"]);
                    label8.Text = Convert.ToString(sqlReader["date"]);
                    label9.Text = Convert.ToString(sqlReader["money"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                {
                    sqlReader.Close();
                }
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            this.Activate();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            textBox3.Text = label7.Text;
            textBox4.Text = label8.Text;
            textBox5.Text = label9.Text;

            textBox3.Visible = true;
            textBox4.Visible = true;
            textBox5.Visible = true;

            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;

            button3.Visible = true;
            button4.Visible = false;
        }

        private async void Button3_Click(object sender, EventArgs e)
        {
            SqlDataReader sqlReader = null;

            string messageBox = "Вы точно хотите изменить данные счета №";
            string caption = "Подтверждение";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            MessageBoxIcon icon = MessageBoxIcon.Question;

            string id = label6.Text;
            string newClient = textBox3.Text;
            string newDate = textBox4.Text;
            string newMoney = textBox5.Text;

            DialogResult result = MessageBox.Show(messageBox + id, caption, buttons, icon);
           switch(result)
            {
                case DialogResult.Yes:
                    try
                    {
                        SqlCommand command = new SqlCommand(" UPDATE [Invoice] SET [client]=@newClient, [date]=@newDate, [money]=@newMoney WHERE [Id]=@id ", sqlConnection);

                        command.Parameters.AddWithValue("newClient", newClient);
                        command.Parameters.AddWithValue("newDate", newDate);
                        command.Parameters.AddWithValue("newMoney", newMoney);
                        command.Parameters.AddWithValue("id", id);

                        await command.ExecuteNonQueryAsync();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        if (sqlReader != null)
                        {
                            sqlReader.Close();
                        }
                        textBox3.Visible = false;
                        textBox4.Visible = false;
                        textBox5.Visible = false;

                        label7.Visible = true;
                        label8.Visible = true;
                        label9.Visible = true;

                        button3.Visible = false;
                        button4.Visible = true;

                        try
                        {
                            SqlCommand command = new SqlCommand(" SELECT * FROM [Invoice] WHERE [Id]=" + id, sqlConnection);
                            sqlReader = await command.ExecuteReaderAsync();
                            while (await sqlReader.ReadAsync())
                            {
                                label6.Text = Convert.ToString(sqlReader["id"]);
                                label7.Text = Convert.ToString(sqlReader["client"]);
                                label8.Text = Convert.ToString(sqlReader["date"]);
                                label9.Text = Convert.ToString(sqlReader["money"]);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            if (sqlReader != null)
                            {
                                sqlReader.Close();
                            }
                        }

                    }
                    break;

                case DialogResult.No:
                    textBox3.Visible = false;
                    textBox4.Visible = false;
                    textBox5.Visible = false;

                    label7.Visible = true;
                    label8.Visible = true;
                    label9.Visible = true;

                    button3.Visible = false;
                    button4.Visible = true;
                    break;
            }
        }

        private async void Button5_Click(object sender, EventArgs e)
        {
            SqlDataReader sqlReader = null;

            string messageBox = "Вы точно хотите удалить данные счета №";
            string caption = "Подтверждение";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            MessageBoxIcon icon = MessageBoxIcon.Warning;

            string id = label6.Text;

            DialogResult result = MessageBox.Show(messageBox + id, caption, buttons, icon);
            switch (result)
            {
                case DialogResult.Yes:
                    try
                    {
                        SqlCommand command = new SqlCommand(" DELETE FROM [Invoice] WHERE [Id]=@id ", sqlConnection);

                        command.Parameters.AddWithValue("id", id);

                        await command.ExecuteNonQueryAsync();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        if (sqlReader != null)
                        {
                            sqlReader.Close();
                        }
                        textBox3.Visible = false;
                        textBox4.Visible = false;
                        textBox5.Visible = false;

                        label7.Visible = true;
                        label8.Visible = true;
                        label9.Visible = true;

                        button3.Visible = false;
                        button4.Visible = true;

                        Form1 form1 = new Form1();
                        Form3 form3 = new Form3();


                        Hide();           
                        form3.Show();
                        

                    }
                    break;

                case DialogResult.No:
                    textBox3.Visible = false;
                    textBox4.Visible = false;
                    textBox5.Visible = false;

                    label7.Visible = true;
                    label8.Visible = true;
                    label9.Visible = true;

                    button3.Visible = false;
                    button4.Visible = true;
                    break;
            }
        }

        private void ВыходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sqlConnection.Close();
            Close();
        }

        private void СправкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            Hide();
            about.Show();
        }
    }
}
