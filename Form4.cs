using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace newProject
{
    public partial class Form4 : Form
    {
        string email = Form3.to;

        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string password = textBox2.Text;
            if (textBox1.Text == password)
            {
                string connectionString = "Data Source=DESKTOP-M4FIU6G\\SQLEXPRESS;Initial Catalog=newDatabase;Integrated Security=True;";
                string query = "UPDATE [users4] SET [password] = @password, [salt] = @salt WHERE [email] = @email";



                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string salt = PasswordHelper.GenerateSalt();
                        string hashedPassword = PasswordHelper.HashPassword(textBox2.Text.Trim(), salt);
                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@password", hashedPassword);
                            cmd.Parameters.AddWithValue("@email", email);
                            cmd.Parameters.AddWithValue("@salt", salt);

                            connection.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Password successfully changed");
                                Form1 form1 = new Form1();
                                form1.Show();
                                this.Hide();

                            }
                            else
                            {
                                MessageBox.Show("No records updated. Please check the email.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Passwords do not match");
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
