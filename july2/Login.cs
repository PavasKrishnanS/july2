using Azure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace july2
{
    public partial class Login : Form
    {
        private object txtMessage;

        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            // Check if username and password meet the specified guidelines
            if (!ContainsDigit(username))
            {
                textBox1.Text = "Username must contain at least one digit.";
                return;
            }

            if (!IsValidPassword(password))
            {
                textBox2.Text = "Password must be at least 7 characters long and contain at least one uppercase letter.";
                return;
            }

            // If both conditions are met, perform further validation or database checks here

            // Simulate successful login for testing purposes
            MessageBox.Show("Login successful!");

            // Optionally, you can redirect to another form or perform additional actions here
            Employee employe = new Employee();
            this.Hide();
            employe.ShowDialog();
        }

        private bool ContainsDigit(string input)
        {
            // Check if the input string contains at least one digit
            return input.Any(char.IsDigit);
        }

        private bool IsValidPassword(string password)
        {
            // Password must be at least 7 characters long and contain at least one uppercase letter
            return password.Length >= 7 && password.Any(char.IsUpper);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}




