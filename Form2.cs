using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace newProject
{
    public partial class Form2 : Form
    {
        private readonly SqlConnection _connection;

        public Form2()
        {
            InitializeComponent();
            _connection = new SqlConnection("Data Source=DESKTOP-M4FIU6G\\SQLEXPRESS;Initial Catalog=newDatabase;Integrated Security=True;");
        }

        private async void button_signup_Click(object sender, EventArgs e)
        {
            var email = textBox3.Text.Trim();
            var password = textBox2.Text.Trim();
            var confirmPassword = textBox3.Text.Trim(); // Şifre doğrulama (confirmation)

            // Şifre ve doğrulama şifresi eşleşiyor mu kontrolü
            if (password != confirmPassword)
            {
                MessageBox.Show("Şifreler uyuşmuyor.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kullanıcıyı veritabanına kaydetme işlemi
            if (await RegisterUserAsync(email, password))
            {
                MessageBox.Show("Kayıt başarılı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Kayıt sırasında bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Kullanıcıyı veritabanına kaydetme işlemi
        private async Task<bool> RegisterUserAsync(string email, string password)
        {
            bool isRegistered = false;

            if (_connection.State != ConnectionState.Open)
            {
                try
                {
                    _connection.Open();

                    // Salt oluştur ve şifreyi hash'le
                    string salt = PasswordHelper.GenerateSalt();
                    string hashedPassword = PasswordHelper.HashPassword(password, salt);

                    // Kullanıcıyı veritabanına ekle
                    string query = "INSERT INTO users4 (email, password, salt) VALUES (@Email, @Password, @Salt)";
                    using (SqlCommand cmd = new SqlCommand(query, _connection))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", hashedPassword);
                        cmd.Parameters.AddWithValue("@Salt", salt);

                        int result = await cmd.ExecuteNonQueryAsync();
                        isRegistered = result > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _connection.Close();
                }
            }

            return isRegistered;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
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
    }
}
