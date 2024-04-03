using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Data.SqlClient;

namespace student_grading_system
{
   
    public partial class Form2 : Form
    {
        string username2;
     
        public Form2(string username)
        {
            InitializeComponent();
            CenterToScreen();
            label2.Text = "Welcome, " + username + "!";
            username2 = username;
            
        }
        private int GetStudentIDByUsername(string username)
        {
            string connectionString = "Data Source=TUNG-HOI;Initial Catalog=BTEC;Integrated Security=True"; // Thay đổi chuỗi kết nối tương ứng với cơ sở dữ liệu của bạn
            string query = "SELECT StudentID FROM [dbo].[Student] WHERE [username] = @Username";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            return Convert.ToInt32(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
            }

            return -1; 
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=TUNG-HOI;Initial Catalog=BTEC;Integrated Security=True";
            string query = "SELECT Student.StudentID, Student.FirstName, Student.LastName, Student.Class, " +
                           "Course.CourseCode, Course.CourseName, Grade.Grade " +
                           "FROM Grade " +
                           "INNER JOIN Course ON Grade.CourseID = Course.CourseID " +
                           "INNER JOIN Student ON Grade.StudentID = Student.StudentID " +
                           "WHERE Grade.StudentID = @StudentID";

            // Lấy studentID từ username
            int studentID = GetStudentIDByUsername(username2);

            if (studentID != -1)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StudentID", studentID);

                        try
                        {
                            connection.Open();
                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // Hiển thị dữ liệu trên DataGridView
                            dataGridView1.DataSource = dataTable;
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
                MessageBox.Show("Không tìm thấy thông tin sinh viên.");
            }


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Cảm ơn bạn đã gửi phản hồi, chúng tôi sẽ trả lời bạn trong thời gian sớm nhất!");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
