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
using Excel=Microsoft.Office.Interop.Excel;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;
using System.Windows.Forms;

namespace july2
{
    public partial class listing : Form
    {

        string connectionString = "Server=.\\SQLEXPRESS;Database=dummy;Integrated Security=True;";
        private object dataGridViewEmployees;

        public listing()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Hide();
            f.ShowDialog();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

        }


        private void listing_Load(object sender, EventArgs e)
        {
            string query = "SELECT EmployeeID, Name, Address, Age, Employee_no, department FROM employee";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                DataTable employeeTable = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(employeeTable);
                    dataGridView1.AutoGenerateColumns = true; // Ensure auto generation of columns
                    dataGridView1.DataSource = employeeTable;

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading employee data: {ex.Message}");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Assuming this button is placed on your form and configured in the designer
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected EmployeeID from the DataGridView
                string employeeID = dataGridView1.SelectedRows[0].Cells["EmployeeID"].Value.ToString();

                // Confirm deletion with user
                DialogResult result = MessageBox.Show("Are you sure you want to delete this employee?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    // Delete from DataGridView
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);

                    // Delete from Database
                    DeleteEmployeeFromDatabase(employeeID); // Implement this method
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "No Row Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void DeleteEmployeeFromDatabase(string employeeID)
        {
            string query = "DELETE FROM employee WHERE EmployeeID = @EmployeeID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@EmployeeID", employeeID);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Employee deleted successfully from database.");
                    }
                    else
                    {
                        MessageBox.Show("No rows deleted from database.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting employee from database: {ex.Message}");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        private void ExportToExcel()
        {
            {
                try
                {
                    // Create a new Excel document
                    string filePath = "C:\\Users\\User\\Desktop\\employee_data.xlsx";
                    SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook);

                    // Add a WorkbookPart to the document
                    WorkbookPart workbookPart = spreadsheetDocument.AddWorkbookPart();
                    workbookPart.Workbook = new Workbook();

                    // Add a WorksheetPart to the WorkbookPart
                    WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                    worksheetPart.Worksheet = new Worksheet(new SheetData());

                    Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild(new Sheets());
                    Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Employee Data" };
                    sheets.Append(sheet);

                    // Get the DataTable from DataGridView
                    DataTable table = new DataTable();
                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        table.Columns.Add(column.HeaderText);
                    }

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        DataRow dataRow = table.NewRow();
                        for (int i = 0; i < dataGridView1.Columns.Count; i++)
                        {
                            dataRow[i] = row.Cells[i].Value;
                        }
                        table.Rows.Add(dataRow);
                    }

                    // Populate Excel sheet with data
                    SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
                    foreach (DataRow row in table.Rows)
                    {
                        Row newRow = new Row();
                        foreach (object cellValue in row.ItemArray)
                        {
                            Cell cell = new Cell();
                            cell.DataType = CellValues.String;
                            cell.CellValue = new CellValue(cellValue.ToString());
                            newRow.AppendChild(cell);
                        }
                        sheetData.AppendChild(newRow);
                    }

                    // Save changes to the SpreadsheetDocument
                    workbookPart.Workbook.Save();


                    MessageBox.Show("Data exported to Excel successfully.", "Export Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting data to Excel: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void List_Click(object sender, EventArgs e)
        {
             
            }

            private void button1_Click_1(object sender, EventArgs e)
            {
                Employee employe = new Employee();
                this.Hide();
                employe.ShowDialog();

            }
        }
    }







 
