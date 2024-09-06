using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace newProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-M4FIU6G\\SQLEXPRESS;Initial Catalog=newDatabase;Integrated Security=True;");

        private void label5_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button_login_Click(object sender, EventArgs e)
        {
            if (connection.State != ConnectionState.Open)
            {
                try
                {
                    connection.Open();
                    String selectData = "SELECT password, salt FROM users4 WHERE email = @email";
                    using (SqlCommand cmd = new SqlCommand(selectData, connection))
                    {
                        cmd.Parameters.AddWithValue("@email", textBox1.Text.Trim());

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string storedHash = reader["password"].ToString();
                                string storedSalt = reader["salt"].ToString();
                                string inputHash = PasswordHelper.HashPassword(textBox2.Text.Trim(), storedSalt); //it s not adding a new salt. it uses the old salt. so you can implement it to the api.

                                if (storedHash == inputHash)
                                {
                                    MessageBox.Show("Logged in successfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Incorrect username or password", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("User could not be found", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error connecting database: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            this.Hide();
            form3.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0';

            }
            else
            {
                textBox2.PasswordChar = '*';
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

       
    }
}
