using System;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace newProject
{
    public partial class Form3 : Form
    {
        string randomCode;
        public static string to;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        int vCode = 1000;
        private void button1_Click(object sender, EventArgs e)
        {
            string from, pass, mail, messageBody;
            Random random = new Random();
            randomCode = (random.Next(vCode)).ToString();
            MailMessage message = new MailMessage();
            to = (textBox1.Text).ToString();
            from = "beyzaest@gmail.com";
            mail = vCode.ToString();


            pass = "pxzd fbgj bnap grvp";

            messageBody = $"Your reset code is {randomCode}";
            message.To.Add(to);
            message.From = new MailAddress(from);
            message.Body = messageBody;
            message.Subject = "Password Reset Code";

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.EnableSsl = true; // TLS 
            smtpClient.Port = 587;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Credentials = new NetworkCredential(from, pass);

            try
            {
                smtpClient.Send(message);
                MessageBox.Show("Verification code sent successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (randomCode == (textBox2.Text).ToString())
            {
                to = textBox1.Text;
                Form4 form4 = new Form4();
                this.Hide();
                form4.Show();
            }
            else
            {
                MessageBox.Show("Wrong Code");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            vCode += 10;
            if (vCode == 9999)
            {
                vCode = 1000;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
