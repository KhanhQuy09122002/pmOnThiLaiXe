using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;






namespace WinFormsApp1
{
    public partial class Form3 : Form
    {
        private string hangXe;
        private int boDe;
        public Form3(string hangXe, int boDe)
        {
            InitializeComponent();
            this.hangXe = hangXe;
            this.boDe = boDe;
        }
      

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void ptbUser_Paint(object sender, PaintEventArgs e)
        {
            Control control = sender as Control;
            using (Pen pen = new Pen(Color.Black, 2)) // Màu đen, độ dày 2px
            {
                e.Graphics.DrawLine(pen, 0, 0, control.Width, 0); // Viền trên
                e.Graphics.DrawLine(pen, 0, 0, 0, control.Height); // Viền trái
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true; 
            this.KeyDown += new KeyEventHandler(Form3_KeyDown);
            comboBox1.SelectedItem = " TRUNG TÂM SÁT HẠCH LÁI XE NGUYỄN TRÌNH";
            if (comboBox1.SelectedIndex == -1 && comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
            ptbUser.Paint += ptbUser_Paint;


            // Danh sách giá trị tương ứng với hạng xe
            Dictionary<string, string> khoaDictionary = new Dictionary<string, string>
               {
                  { "A1", "A1-K25" },
                  { "A", "A-K25" },
                  { "B1", "B1-K25" },
                  { "B", "B-K25" },
                  { "C1", "C1-K25" },
                  { "C", "C-K25" },
                  { "D1", "D1-K25" },
                  { "D2", "D2-K25" },
                  { "D", "D-K25" },
                  { "RoMooc", "RoMooc-K25" }
               };
            cbbKhoa.Items.Clear();
            if (khoaDictionary.ContainsKey(hangXe))
            {
                string khoaValue = khoaDictionary[hangXe];
                cbbKhoa.Items.Add(khoaValue);
                cbbKhoa.SelectedIndex = 0; 
            }
        }

        private void Form3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ptbUser.Image == null) // Kiểm tra nếu PictureBox không có dữ liệu
                {
                    btnKTTT.PerformClick(); // Kích hoạt btnKTTT
                }
                else
                {
                    btnVaoThi.PerformClick(); // Kích hoạt btnVaoThi
                }

                e.Handled = true; // Ngăn sự kiện tiếp tục được xử lý
            }
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void btnKTTT_Click(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtSoBaoDanh.Text, @"^\d+$"))
            { 
                ptbUser.Image = Image.FromFile("images/User2.jpg"); 
                lbLoaiGPLX.Text = hangXe;
                lbHoTen.Text = "Vũ Thị Khánh Huyền";
                lbNgaySinh.Text = "09/12/2004";
                lbCCCD.Text = "8401372655";
                lbDC.Text = "Trà Vinh";
                lbThi.Text = "CHƯA THI";
            }
            else
            {
                MessageBox.Show("Số báo danh không hợp lệ! Vui lòng nhập SBD từ 01 trở đi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnVaoThi_Click(object sender, EventArgs e)
        {
            if (ptbUser.Image == null)
            {
                MessageBox.Show("Vui lòng nhập Số báo danh!");
                return;
            }

            string SBD = txtSoBaoDanh.Text;
            Form4 form4 = new Form4(hangXe, boDe, SBD);
            form4.WindowState = FormWindowState.Maximized;
            form4.Show();

           
        }



    }
}
