using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace july2
{
    public partial class Form1 : Form
    {
        string connectionString = "Server=.\\SQLEXPRESS;Database=dummy;Integrated Security=True;";

        public Form1(string? id)
        {
            InitializeComponent();
        }

        public Form1()
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
         if (string.IsNullOrWhiteSpace(textBox1.Text) ||
        string.IsNullOrWhiteSpace(textBox2.Text) ||
        string.IsNullOrWhiteSpace(textBox3.Text) ||
        string.IsNullOrWhiteSpace(textBox4.Text) ||
        string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }
            string query = "INSERT INTO employee (Name, Address, Age, Employee_no, department) " +
                           "VALUES (@Name, @Address, @Age, @Employee_no, @department)";

            // Create SqlConnection and SqlCommand objects within a using block
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Add parameters to the SqlCommand
                command.Parameters.AddWithValue("@Name", textBox1.Text);
                command.Parameters.AddWithValue("@Address", textBox2.Text);
                command.Parameters.AddWithValue("@Age", textBox3.Text); // Assuming Age is stored as a string in the database
                command.Parameters.AddWithValue("@Employee_no", textBox4.Text);
                command.Parameters.AddWithValue("@department", textBox5.Text);

                try
                {
                    connection.Open(); // Open the connection

                    int rowsAffected = command.ExecuteNonQuery(); // Execute the query and get the number of rows affected

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Employee details inserted successfully.");
                        ClearFormFields(); // Clear form fields after successful insertion
                    }
                    else
                    {
                        MessageBox.Show("No rows inserted.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error inserting employee details: {ex.Message}");
                }
                finally
                {
                    connection.Close(); // Ensure the connection is properly closed
                }
              
            }
        }

        private void ClearFormFields()
        {
            // Clear all text boxes
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            // Add more fields as needed
        }
    }
}
