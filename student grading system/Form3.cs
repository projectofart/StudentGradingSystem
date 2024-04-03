using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace student_grading_system
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hệ thống đã cấp mật khẩu mới tới email của bạn!");
        }
    }
}
