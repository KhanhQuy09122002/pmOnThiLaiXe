using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace WinFormsApp1
{
    public partial class Form5 : Form
    {
        private System.Windows.Forms.Timer timer;

        private string connectionString = "Data Source=ThiThuLaiXe.db;Version=3;";
        private string SBD;
        private string hangXe;
        private int checkCauDung;
        private int boDe;
        private int cauLiet;
        public Form5(string SBD, string hangXe, int checkCauDung, int boDe,int cauLiet)
        {
            InitializeComponent();
            this.SBD = SBD;
            this.hangXe = hangXe;
            this.checkCauDung = checkCauDung;
            this.boDe = boDe;
            this.cauLiet = cauLiet;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            timer = new System.Windows.Forms.Timer(); 

            timer.Interval = 5000; 
            timer.Tick += Timer_Tick;
            timer.Start();
            ///
            labelCauLiet.Text = cauLiet.ToString();
            labelSBD.Text = SBD;
            labelHang.Text = hangXe;
            labelCauDung.Text = checkCauDung.ToString();
            int cauDatToiThieu = 0;
            int soCauThi = 0;

            // Kết nối đến database
            string connectionString = "Data Source=ThiThuLaiXe.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Truy vấn lấy cả so_cau_dat_toi_thieu và so_cau_thi
                string query = "SELECT so_cau_dat_toi_thieu, so_cau_thi FROM BoDe WHERE id = @boDe";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@boDe", boDe);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // Kiểm tra nếu có dữ liệu
                        {
                            cauDatToiThieu = reader.GetInt32(0); // Lấy giá trị cột so_cau_dat_toi_thieu
                            soCauThi = reader.GetInt32(1); // Lấy giá trị cột so_cau_thi
                        }
                    }
                }
            }
            if (cauLiet == 1)
            {
                labelKetQua.Text = "RỚT";
                int soCauSai = soCauThi - checkCauDung;
                labelCauSai.Text = soCauSai.ToString();
                labelCauLiet.Text = "Sai câu liệt";
            }
            else
            {
                int soCauSai = soCauThi - checkCauDung;
                labelCauSai.Text = soCauSai.ToString();
                labelCauLiet.Text = "";
                if (checkCauDung < cauDatToiThieu)
                {
                    labelKetQua.Text = "TRƯỢT";
                }
                else
                {
                    labelKetQua.Text = "ĐẠT";
                }
            }

        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();  // Dừng Timer
            this.Close();  // Đóng Form
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Form> formsToClose = new List<Form>();

            foreach (Form frm in Application.OpenForms)
            {
                if (frm is Form4 || frm is Form5)
                {
                    formsToClose.Add(frm);
                }
            }
            foreach (Form frm in formsToClose)
            {
                frm.Close();
            }
        }

        private void labelCauDung_Click(object sender, EventArgs e)
        {

        }
    }
}
