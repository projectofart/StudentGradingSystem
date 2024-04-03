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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace student_grading_system
{
    public partial class Form4 : Form
    {
        string connectionString = "Data Source=TUNG-HOI;Initial Catalog=BTEC;Integrated Security=True";
        public Form4(string username)
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void UpdateGrade()
        {
            // Get StudentID from TextBox1, CourseID from TextBox3, and new grade from TextBox2
            if (int.TryParse(textBox4.Text, out int studentID) && int.TryParse(textBox1.Text, out int courseID))
            {
                string newGrade = textBox5.Text; // Grade as string (varchar)

                // Update the grade in the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string updateQuery = "UPDATE [dbo].[Grade] SET [Grade] = @NewGrade WHERE [StudentID] = @StudentID AND [CourseID] = @CourseID";
                    string selectQuery = "SELECT * FROM [dbo].[Student] WHERE [StudentID] = @StudentID";
                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                    using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@NewGrade", newGrade);
                        updateCommand.Parameters.AddWithValue("@StudentID", studentID);
                        updateCommand.Parameters.AddWithValue("@CourseID", courseID);
                        selectCommand.Parameters.AddWithValue("@StudentID", studentID);

                        try
                        {
                            connection.Open();
                            // Update grade
                            int rowsAffected = updateCommand.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Đã cập nhật điểm cho học sinh có StudentID " + studentID + " và mã môn " + courseID);
                                // Clear TextBoxes after updating
                                textBox4.Clear();
                                textBox1.Clear();
                                textBox5.Clear();

                                // Display student information in DataGridView
                                SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);
                                dataGridView1.DataSource = dataTable;
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy học sinh có StudentID " + studentID + " hoặc không có mã môn " + courseID);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng cho StudentID, CourseID và điểm!");
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateGrade();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đã cập nhật điểm cho học sinh có StudentID ");
        }
    }
}
