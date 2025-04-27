using System.Diagnostics;
using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Diagnostics;
using System.Globalization;

namespace WinFormsApp1
    
{
    public partial class FORM1 : Form
    {
        SQLiteConnection dt; 
        public FORM1()
        {
            InitializeComponent();
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

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "http://www.youtube.com/@TruongLaiNguyenTrinh", 
                UseShellExecute = true 
            });
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.youtube.com/watch?v=BO8JFsr5aIY",
                UseShellExecute = true
            });
        }
        private Dictionary<string, string> hangXeDictionary = new Dictionary<string, string>();

        private void LoadHangDaoTao()
        {
            string connectionString = "Data Source=ThiThuLaiXe.db;Version=3;";

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT ten_hang, hinh_anh FROM HangXe";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        comboBox1.Items.Clear();
                        hangXeDictionary.Clear();

                        while (reader.Read())
                        {
                            string tenHang = reader["ten_hang"] != DBNull.Value ? reader["ten_hang"].ToString() : "";
                            string hinhAnh = reader["hinh_anh"] != DBNull.Value ? reader["hinh_anh"].ToString() : "";

                            comboBox1.Items.Add(tenHang);
                            hangXeDictionary[tenHang] = hinhAnh;
                        }
                    }
                }
            }
            catch (SQLiteException sqlEx)
            {
                MessageBox.Show("Lỗi SQLite: " + sqlEx.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách Hạng đào tạo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedHang = comboBox1.SelectedItem.ToString();
            if (hangXeDictionary.ContainsKey(selectedHang))
            {
                string imagePath = hangXeDictionary[selectedHang];

                try
                {
                    pictureBox3.Image = Image.FromFile(imagePath);
                    pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải hình ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            flowLayoutBoDe.Controls.Clear();
            string connectionString = "Data Source=ThiThuLaiXe.db;Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = @"
            SELECT BoDe.id, BoDe.ten_bo_de 
            FROM BoDe 
            JOIN HangXe ON BoDe.id_hang_xe = HangXe.id 
            WHERE HangXe.ten_hang = @ten_hang";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ten_hang", selectedHang);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idBoDe = reader.GetInt32(0);
                            string tenBoDe = reader.GetString(1);

                            Button btn = new Button();
                            btn.Text = tenBoDe;
                            btn.Width = 40;
                            btn.Height = 40;
                            btn.Margin = new Padding(5);
                            btn.BackColor = Color.Yellow;

                            btn.Click += (s, ev) =>
                            {
                                Form3 form3 = new Form3(selectedHang, idBoDe);
                                form3.ShowDialog();
                            };
                            flowLayoutBoDe.Controls.Add(btn);
                        }
                    }
                }
            }
        }




        private void FORM1_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=ThiThuLaiXe.db;Version=3;";
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open) // Kiểm tra trạng thái kết nối
                    {
                       // MessageBox.Show("Kết nối thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Lỗi kết nối: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadHangDaoTao();
            comboBox1.SelectedItem = "A1";
            comboBox1_SelectedIndexChanged(comboBox1, EventArgs.Empty);
        }


        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2("B");
            form2.WindowState = FormWindowState.Maximized;
            form2.Show(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2("RoMooc");
            form2.WindowState = FormWindowState.Maximized;
            form2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string hang = "A1"; 
            Form2 form2 = new Form2(hang);
            form2.WindowState = FormWindowState.Maximized;
            form2.Show();
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2("B1");
            form2.WindowState = FormWindowState.Maximized;
            form2.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2("A");
            form2.WindowState = FormWindowState.Maximized;
            form2.Show();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.facebook.com/profile.php?id=61573430033214&locale=vi_VN",
                UseShellExecute = true
            });
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://nguyentrinh.com.vn/post/dao-tao-sat-hach-lai-xe",
                UseShellExecute = true 
            });
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}