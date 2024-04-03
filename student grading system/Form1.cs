using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace student_grading_system
{
    public partial class Form1 : Form
    {
        string connectionString = "Data Source=TUNG-HOI;Initial Catalog=BTEC;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
         }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
                // Tạo instance của Form3
            Form3 form3 = new Form3();
                // Hiển thị Form3
            form3.Show();
                // Đóng Form hiện tại nếu bạn muốn
                // this.Close();
            

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM [dbo].[Student] WHERE [username] = @Username AND [Password] = @Password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    try
                    {
                        connection.Open();
                        int count = (int)command.ExecuteScalar();

                        if (count > 0)
                        {
                            // Đăng nhập thành công với bảng Student
                            this.Hide();
                            Form2 form2 = new Form2(username);
                            form2.ShowDialog();
                        }
                        else
                        {
                            // Thử tìm trong bảng TrainingRoomManager
                            query = "SELECT COUNT(*) FROM [dbo].[TrainingRoomManager] WHERE [User] = @AdminUsername AND [Password] = @AdminPassword";
                            command.CommandText = query;
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@AdminUsername", username);
                            command.Parameters.AddWithValue("@AdminPassword", password);
                            count = (int)command.ExecuteScalar();

                            if (count > 0)
                            {
                                // Đăng nhập thành công với bảng TrainingRoomManager
                                this.Hide();
                                Form4 form4 = new Form4(username);
                                form4.ShowDialog();
                                // Thực hiện các hành động khi đăng nhập thành công vào vai trò admin
                            }
                            else
                            {
                                // Thử tìm trong bảng Parent
                                query = "SELECT COUNT(*) FROM [dbo].[Parent] WHERE [username] = @username AND [password] = @password";
                                command.CommandText = query;
                                command.Parameters.Clear();
                                command.Parameters.AddWithValue("@username", username);
                                command.Parameters.AddWithValue("@password", password);
                                count = (int)command.ExecuteScalar();

                                if (count > 0)
                                {
                                    // Đăng nhập thành công với bảng Parent
                                    this.Hide();
                                    Form2 form2 = new Form2(username);
                                    form2.ShowDialog();
                                    // Thực hiện các hành động khi đăng nhập thành công vào vai trò phụ huynh
                                }
                                else
                                {
                                    // Thông báo không tìm thấy tài khoản
                                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
            }



        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
