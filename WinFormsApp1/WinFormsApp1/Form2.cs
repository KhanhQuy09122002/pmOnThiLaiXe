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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp1
{
   
    public partial class Form2 : Form
    {
        private string connectionString = "Data Source=ThiThuLaiXe.db;Version=3;";
        private SQLiteConnection dt;
      
        private string hangDuocChon;
        private List<string> danhSachHinhAnh = new List<string>();
        private int indexCauHoi = 0;
        public Form2(string hang)
        {
            InitializeComponent();
            this.hangDuocChon = hang;
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

                    // Kiểm tra xem bảng HangXe có tồn tại không
                    string checkTableQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='HangXe';";
                    using (SQLiteCommand checkCmd = new SQLiteCommand(checkTableQuery, conn))
                    {
                        object result = checkCmd.ExecuteScalar();
                        if (result == null)
                        {
                            MessageBox.Show("Bảng HangXe không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return; // Dừng lại nếu bảng không tồn tại
                        }
                        else
                        {
                           
                        }
                    }

                    // Nếu bảng tồn tại, tiếp tục lấy dữ liệu
                    string query = "SELECT ten_hang, hinh_anh FROM HangXe";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        cbbForm2.Items.Clear();
                        hangXeDictionary.Clear();

                        while (reader.Read())
                        {
                            string tenHang = reader["ten_hang"] != DBNull.Value ? reader["ten_hang"].ToString() : "";
                            string hinhAnh = reader["hinh_anh"] != DBNull.Value ? reader["hinh_anh"].ToString() : "";

                            cbbForm2.Items.Add(tenHang);
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


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbForm2.SelectedItem == null)
                return; 
            string hangDuocChon = cbbForm2.SelectedItem.ToString();
            int idHang = GetIdHangFromTenHang(hangDuocChon);
            checkPhanLoai = hangDuocChon;
            if (idHang > 0)
            {     
                int demCauHoi = DemCauHoiTheoHang(hangDuocChon);
                labelTongSoCau.Text = demCauHoi.ToString();

                
                danhSachHinhAnh.Clear();
                danhSachHinhAnh = LayDanhSachHinhAnhCauHoi(hangDuocChon);

                if (danhSachHinhAnh.Count > 0)
                {
                    indexCauHoi = 0;
                    HienThiCauHoi();
                }
            }
            else
            {
                labelTongSoCau.Text = "Không tìm thấy hạng!";
            }
            labelDapAn.Text = "";
        }

        private void HienThiCauHoi()
        {
            if (danhSachHinhAnh.Count > 0 && indexCauHoi >= 0 && indexCauHoi < danhSachHinhAnh.Count)
            {
                labelCau.Text = $"Câu: {indexCauHoi + 1} / {danhSachHinhAnh.Count}";

                string duongDanAnh = danhSachHinhAnh[indexCauHoi];

                if (File.Exists(duongDanAnh))
                {
                    pictureBoxCauHoi.Image = Image.FromFile(duongDanAnh);
                }
                else
                {
                    pictureBoxCauHoi.Image = null; // Nếu không có ảnh, hiển thị trống
                }

                // Kiểm tra nếu câu hỏi là Câu Điểm Liệt
                if (KiemTraCauLiet(duongDanAnh))
                {
                    labelCauLiet.Text = "CÂU ĐIỂM LIỆT";
                    labelCauLiet.ForeColor = Color.Red; // Tô đỏ để nổi bật
                    labelCauLiet.Visible = true;
                }
                else
                {
                    labelCauLiet.Text = "";
                    labelCauLiet.Visible = false;
                }
            }
        }
        private bool KiemTraCauLiet(string duongDanAnh)
        {
            bool isCauLiet = false;
            string query = "SELECT cau_liet FROM DanhSachCauHoi WHERE hinh_anh = @hinhAnh";

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@hinhAnh", duongDanAnh);

                conn.Open();
                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)  // Kiểm tra NULL trước khi chuyển đổi
                {
                    isCauLiet = Convert.ToInt32(result) == 1;  // Chuyển đổi từ int sang bool (SQLite không có kiểu bool)
                }
            }

            return isCauLiet;
        }



        private List<string> LayDanhSachHinhAnhCauHoi(string hangDuocChon)
        {
            List<string> dsHinhAnh = new List<string>();

            string query = "";

            // Xây dựng truy vấn SQL dựa trên giá trị của hạng được chọn
            if (hangDuocChon == "A1")
            {
                query = "SELECT hinh_anh FROM DanhSachCauHoi WHERE check_a1 = 1";
            }
            else if (hangDuocChon == "A")
            {
                query = "SELECT hinh_anh FROM DanhSachCauHoi WHERE check_a = 1";
            }
            else if (hangDuocChon == "B")
            {
                query = "SELECT hinh_anh FROM DanhSachCauHoi WHERE check_b = 1";
            }
            else if (hangDuocChon == "B1")
            {
                query = "SELECT hinh_anh FROM DanhSachCauHoi WHERE check_b1 = 1";
            }
            else if (new List<string> { "C", "C1", "D1", "D2", "D", "RoMooc" }.Contains(hangDuocChon))
            {
                query = "SELECT hinh_anh FROM DanhSachCauHoi"; // Lấy tất cả các bản ghi
            }
            else
            {
                MessageBox.Show("Hạng được chọn không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return dsHinhAnh;
            }

            // Thực hiện truy vấn
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                conn.Open();
                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dsHinhAnh.Add(reader["hinh_anh"].ToString());
                }
            }

            return dsHinhAnh;
        }



        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // pictureBoxCauHoi.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxCauHoi.SizeMode = PictureBoxSizeMode.StretchImage;
            string connectionString = "Data Source=ThiThuLaiXe.db;Version=3;";
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open) // Kiểm tra trạng thái kết nối
                    {
                        
                    }
                }
            }
            catch (Exception ex)
            {
              //  MessageBox.Show("Lỗi kết nối: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           LoadHangDaoTao();

            this.BeginInvoke(new Action(() =>
            {
                cbbForm2.SelectedItem = hangDuocChon;
                

                // Sau khi ComboBox đã chọn đúng hạng, lấy id_hang
                int idHang = GetIdHangFromTenHang(hangDuocChon);
                if (idHang > 0)
                {
                    int demCauHoi = DemCauHoiTheoHang(hangDuocChon);
                    labelTongSoCau.Text = $"{demCauHoi}";
                }
                else
                {
                    labelTongSoCau.Text = "Không tìm thấy hạng!";
                }
            }));
            cbbForm2.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
        }
        private int GetIdHangFromTenHang(string tenHang)
        {
            int idHang = -1;
            string connectionString = "Data Source=ThiThuLaiXe.db;Version=3;";

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                // Kiểm tra xem bảng HangXe có tồn tại trước khi truy vấn
                string checkTableQuery = "SELECT count(*) FROM sqlite_master WHERE type='table' AND name='HangXe'";
                using (SQLiteCommand checkCmd = new SQLiteCommand(checkTableQuery, conn))
                {
                    int tableExists = Convert.ToInt32(checkCmd.ExecuteScalar());
                    if (tableExists == 0)
                    {
                        MessageBox.Show("Lỗi: Bảng HangXe không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
                    }
                }

                // Truy vấn lấy ID từ HangXe
                string query = "SELECT id FROM HangXe WHERE ten_hang = ?";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("?", tenHang);
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        idHang = Convert.ToInt32(result);
                    }
                }
            }

            return idHang;
        }


        private int DemCauHoiTheoHang(string hangDuocChon)
        {
            int count = 0;
            string checkTableQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='DanhSachCauHoi';";
            string countQuery = "";

            // Xây dựng truy vấn SQL dựa trên giá trị của hạng được chọn
            if (hangDuocChon == "A1")
            {
                countQuery = "SELECT COUNT(*) FROM DanhSachCauHoi WHERE check_a1 = 1";
            }
            else if (hangDuocChon == "A")
            {
                countQuery = "SELECT COUNT(*) FROM DanhSachCauHoi WHERE check_a = 1";
            }
            else if (hangDuocChon == "B")
            {
                countQuery = "SELECT COUNT(*) FROM DanhSachCauHoi WHERE check_b = 1";
            }
            else if (hangDuocChon == "B1")
            {
                countQuery = "SELECT COUNT(*) FROM DanhSachCauHoi WHERE check_b1 = 1";
            }
            else if (new List<string> { "C", "C1", "D1", "D2", "D", "RoMooc" }.Contains(hangDuocChon))
            {
                countQuery = "SELECT COUNT(*) FROM DanhSachCauHoi"; // Lấy tổng số câu hỏi
            }
            else
            {
                MessageBox.Show("Hạng được chọn không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1; // Trả về -1 để báo lỗi
            }

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                // 1️⃣ Kiểm tra xem bảng có tồn tại không
                using (SQLiteCommand checkCmd = new SQLiteCommand(checkTableQuery, conn))
                {
                    object tableExists = checkCmd.ExecuteScalar();
                    if (tableExists == null)
                    {
                        MessageBox.Show("Không có bảng DanhSachCauHoi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return -1; // Trả về -1 để báo lỗi
                    }
                }

                // 2️⃣ Nếu bảng tồn tại, tiến hành đếm số câu hỏi
                using (SQLiteCommand cmd = new SQLiteCommand(countQuery, conn))
                {
                    object result = cmd.ExecuteScalar(); // Thực thi truy vấn

                    if (result != null && int.TryParse(result.ToString(), out int temp))
                    {
                        count = temp;
                    }
                }
            }

            return count;
        }





        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void labelTongSoCau_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (indexCauHoi > 0)
            {
                indexCauHoi--;
                HienThiCauHoi();
            }
            labelDapAn.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (indexCauHoi < danhSachHinhAnh.Count - 1)
            {
                indexCauHoi++;
                HienThiCauHoi();
            }
            labelDapAn.Text = "";
        }

        private void labelDapAn_Click(object sender, EventArgs e)
        {

        }
        private string LayDapAnCauHoi(string hangDuocChon, int viTriCauHoi)
        {
            string dapAn = "Không tìm thấy đáp án!";
            string query = "";

            // Xác định điều kiện truy vấn dựa vào hạng được chọn
            if (hangDuocChon == "A1")
            {
                query = @"
                   SELECT dap_an FROM DanhSachCauHoi 
                   WHERE check_a1 = 1 
                   ORDER BY id
                   LIMIT @LimitValue, 1";
            }
            else if (hangDuocChon == "A")
            {
                query = @"
                SELECT dap_an FROM DanhSachCauHoi 
                WHERE check_a = 1 
                ORDER BY id
                LIMIT @LimitValue, 1";
            }
            else if (hangDuocChon == "B")
            {
                query = @"
                SELECT dap_an FROM DanhSachCauHoi 
                WHERE check_b = 1 
                ORDER BY id
                LIMIT @LimitValue, 1";
            }
            else if (hangDuocChon == "B1")
            {
                query = @"
                SELECT dap_an FROM DanhSachCauHoi 
                WHERE check_b1 = 1 
                ORDER BY id
                LIMIT @LimitValue, 1";
            }
            else if (new List<string> { "C", "C1", "D1", "D2", "D", "RoMooc" }.Contains(hangDuocChon))
            {
                query = @"
                SELECT dap_an FROM DanhSachCauHoi 
                ORDER BY id
                LIMIT @LimitValue, 1";
            }
            else
            {
                MessageBox.Show("Hạng được chọn không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Lỗi hạng không hợp lệ!";
            }

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@LimitValue", viTriCauHoi);

                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    dapAn = result.ToString();
                }
            }

            return dapAn;
        }

        private string LayDapAnCauHoiLGTDB(string hangDuocChon, int viTriCauHoi)
        {
            string dapAn = "Không tìm thấy đáp án!";
            string query = "";

            // Xác định điều kiện truy vấn dựa vào hạng được chọn
            if (hangDuocChon == "A1")
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE check_a1 = 1 AND phan_loai = 'LGTDB'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else if (hangDuocChon == "A")
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE check_a = 1 AND phan_loai = 'LGTDB'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else if (hangDuocChon == "B")
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE check_b = 1 AND phan_loai = 'LGTDB'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else if (hangDuocChon == "B1")
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE check_b1 = 1 AND phan_loai = 'LGTDB'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else if (new List<string> { "C", "C1", "D1", "D2", "D", "RoMooc" }.Contains(hangDuocChon))
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE phan_loai = 'LGTDB'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else
            {
                MessageBox.Show("Hạng được chọn không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Lỗi hạng không hợp lệ!";
            }

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@LimitValue", viTriCauHoi);

                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    dapAn = result.ToString();
                }
            }

            return dapAn;
        }
        private string LayDapAnCauHoiNVVT(string hangDuocChon, int viTriCauHoi)
        {
            string dapAn = "Không tìm thấy đáp án!";
            string query = "";

            // Xác định điều kiện truy vấn dựa vào hạng được chọn
            if (hangDuocChon == "A1")
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE check_a1 = 1 AND phan_loai = 'NVVT'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else if (hangDuocChon == "A")
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE check_a = 1 AND phan_loai = 'NVVT'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else if (hangDuocChon == "B")
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE check_b = 1 AND phan_loai = 'NVVT'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else if (hangDuocChon == "B1")
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE check_b1 = 1 AND phan_loai = 'NVVT'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else if (new List<string> { "C", "C1", "D1", "D2", "D", "RoMooc" }.Contains(hangDuocChon))
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE phan_loai = 'NVVT'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else
            {
                MessageBox.Show("Hạng được chọn không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Lỗi hạng không hợp lệ!";
            }

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@LimitValue", viTriCauHoi);

                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    dapAn = result.ToString();
                }
            }

            return dapAn;
        }

        private string LayDapAnCauHoiKTLX(string hangDuocChon, int viTriCauHoi)
        {
            string dapAn = "Không tìm thấy đáp án!";
            string query = "";

            // Xác định điều kiện truy vấn dựa vào hạng được chọn
            if (hangDuocChon == "A1")
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE check_a1 = 1 AND phan_loai = 'KTLX'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else if (hangDuocChon == "A")
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE check_a = 1 AND phan_loai = 'KTLX'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else if (hangDuocChon == "B")
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE check_b = 1 AND phan_loai = 'KTLX'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else if (hangDuocChon == "B1")
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE check_b1 = 1 AND phan_loai = 'KTLX'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else if (new List<string> { "C", "C1", "D1", "D2", "D", "RoMooc" }.Contains(hangDuocChon))
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE phan_loai = 'KTLX'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else
            {
                MessageBox.Show("Hạng được chọn không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Lỗi hạng không hợp lệ!";
            }

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@LimitValue", viTriCauHoi);

                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    dapAn = result.ToString();
                }
            }

            return dapAn;
        }

        private string LayDapAnCauHoiVHDD(string hangDuocChon, int viTriCauHoi)
        {
            string dapAn = "Không tìm thấy đáp án!";
            string query = "";

            // Xác định điều kiện truy vấn dựa vào hạng được chọn
            if (hangDuocChon == "A1")
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE check_a1 = 1 AND phan_loai = 'VHDD'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else if (hangDuocChon == "A")
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE check_a = 1 AND phan_loai = 'VHDD'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else if (hangDuocChon == "B")
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE check_b = 1 AND phan_loai = 'VHDD'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else if (hangDuocChon == "B1")
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE check_b1 = 1 AND phan_loai = 'VHDD'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else if (new List<string> { "C", "C1", "D1", "D2", "D", "RoMooc" }.Contains(hangDuocChon))
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE phan_loai = 'VHDD'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else
            {
                MessageBox.Show("Hạng được chọn không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Lỗi hạng không hợp lệ!";
            }

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@LimitValue", viTriCauHoi);

                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    dapAn = result.ToString();
                }
            }

            return dapAn;
        }

        private string LayDapAnCauHoiCTSCX(string hangDuocChon, int viTriCauHoi)
        {
            string dapAn = "Không tìm thấy đáp án!";
            string query = "";

            // Xác định điều kiện truy vấn dựa vào hạng được chọn
            if (hangDuocChon == "A1")
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE check_a1 = 1 AND phan_loai = 'CTSCX'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else if (hangDuocChon == "A")
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE check_a = 1 AND phan_loai = 'CTSCX'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else if (hangDuocChon == "B")
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE check_b = 1 AND phan_loai = 'CTSCX'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else if (hangDuocChon == "B1")
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE check_b1 = 1 AND phan_loai = 'CTSCX'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else if (new List<string> { "C", "C1", "D1", "D2", "D", "RoMooc" }.Contains(hangDuocChon))
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE phan_loai = 'CTSCX'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else
            {
                MessageBox.Show("Hạng được chọn không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Lỗi hạng không hợp lệ!";
            }

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@LimitValue", viTriCauHoi);

                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    dapAn = result.ToString();
                }
            }

            return dapAn;
        }
        private string LayDapAnCauHoiSH(string hangDuocChon, int viTriCauHoi)
        {
            string dapAn = "Không tìm thấy đáp án!";
            string query = "";

            // Xác định điều kiện truy vấn dựa vào hạng được chọn
            if (hangDuocChon == "A1")
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE check_a1 = 1 AND phan_loai = 'SH'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else if (hangDuocChon == "A")
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE check_a = 1 AND phan_loai = 'SH'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else if (hangDuocChon == "B")
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE check_b = 1 AND phan_loai = 'SH'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else if (hangDuocChon == "B1")
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE check_b1 = 1 AND phan_loai = 'SH'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else if (new List<string> { "C", "C1", "D1", "D2", "D", "RoMooc" }.Contains(hangDuocChon))
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE phan_loai = 'SH'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else
            {
                MessageBox.Show("Hạng được chọn không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Lỗi hạng không hợp lệ!";
            }

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@LimitValue", viTriCauHoi);

                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    dapAn = result.ToString();
                }
            }

            return dapAn;
        }
        private string LayDapAnCauHoiBB(string hangDuocChon, int viTriCauHoi)
        {
            string dapAn = "Không tìm thấy đáp án!";
            string query = "";

            // Xác định điều kiện truy vấn dựa vào hạng được chọn
            if (hangDuocChon == "A1")
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE check_a1 = 1 AND phan_loai = 'BB'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else if (hangDuocChon == "A")
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE check_a = 1 AND phan_loai = 'BB'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else if (hangDuocChon == "B")
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE check_b = 1 AND phan_loai = 'BB'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else if (hangDuocChon == "B1")
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE check_b1 = 1 AND phan_loai = 'BB'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else if (new List<string> { "C", "C1", "D1", "D2", "D", "RoMooc" }.Contains(hangDuocChon))
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE phan_loai = 'BB'
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else
            {
                MessageBox.Show("Hạng được chọn không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Lỗi hạng không hợp lệ!";
            }

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@LimitValue", viTriCauHoi);

                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    dapAn = result.ToString();
                }
            }

            return dapAn;
        }
        private string LayDapAnCauHoiCauLiet(string hangDuocChon, int viTriCauHoi)
        {
            string dapAn = "Không tìm thấy đáp án!";
            string query = "";

            // Xác định điều kiện truy vấn dựa vào hạng được chọn
            if (hangDuocChon == "A1")
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE check_a1 = 1 AND cau_liet = 1
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else if (hangDuocChon == "A")
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE check_a = 1 AND cau_liet = 1
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else if (hangDuocChon == "B")
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE check_b = 1 AND cau_liet = 1
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else if (hangDuocChon == "B1")
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE check_b1 = 1 AND cau_liet = 1
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else if (new List<string> { "C", "C1", "D1", "D2", "D", "RoMooc" }.Contains(hangDuocChon))
            {
                query = @"
            SELECT dap_an FROM DanhSachCauHoi 
            WHERE cau_liet = 1
            ORDER BY id
            LIMIT @LimitValue, 1";
            }
            else
            {
                MessageBox.Show("Hạng được chọn không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Lỗi hạng không hợp lệ!";
            }

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@LimitValue", viTriCauHoi);

                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    dapAn = result.ToString();
                }
            }

            return dapAn;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (cbbForm2.SelectedItem != null && danhSachHinhAnh.Count > 0)
            {
                string hangDuocChon = cbbForm2.SelectedItem.ToString();
                int idHang = GetIdHangFromTenHang(hangDuocChon);

                if (idHang > 0)
                {
                    string dapAn = "";

                    // Kiểm tra loại câu hỏi để gọi hàm tương ứng
                    if (checkPhanLoai == "LGTDB")
                        dapAn = LayDapAnCauHoiLGTDB(hangDuocChon, indexCauHoi);
                    else if (checkPhanLoai == "NVVT")
                        dapAn = LayDapAnCauHoiNVVT(hangDuocChon, indexCauHoi);
                    else if (checkPhanLoai == "KTLX")
                        dapAn = LayDapAnCauHoiKTLX(hangDuocChon, indexCauHoi);
                    else if (checkPhanLoai == "CTSCX")
                        dapAn = LayDapAnCauHoiCTSCX(hangDuocChon, indexCauHoi);
                    else if (checkPhanLoai == "VHDD")
                        dapAn = LayDapAnCauHoiVHDD(hangDuocChon, indexCauHoi);
                    else if (checkPhanLoai == "SH")
                        dapAn = LayDapAnCauHoiSH(hangDuocChon, indexCauHoi);
                    else if (checkPhanLoai == "BB")
                        dapAn = LayDapAnCauHoiBB(hangDuocChon, indexCauHoi);
                    else if (checkPhanLoai == "cauLiet")
                        dapAn = LayDapAnCauHoiCauLiet(hangDuocChon, indexCauHoi);
                    else
                        dapAn = LayDapAnCauHoi(hangDuocChon, indexCauHoi);

                    labelDapAn.Text = "ĐÁP ÁN : " + dapAn;
                }
                else
                {
                    labelDapAn.Text = "Không tìm thấy hạng!";
                }
            }
            else
            {
                labelDapAn.Text = "Vui lòng chọn hạng và câu hỏi!";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            checkPhanLoai = "LGTDB";
            if (cbbForm2.SelectedItem != null)
            {
                string hangDuocChon = cbbForm2.SelectedItem.ToString();
                int idHang = GetIdHangFromTenHang(hangDuocChon);

                if (idHang > 0)
                {
                    // Lấy danh sách câu hỏi với phan_loai = "Luật giao thông đường bộ"
                    danhSachHinhAnh = LayDanhSachHinhAnhCauHoi(hangDuocChon, "LGTDB");

                    // Đếm số lượng câu hỏi loại "LGTDB"
                    int demCauHoi = danhSachHinhAnh.Count;
                    labelTongSoCau.Text = demCauHoi.ToString();

                    if (danhSachHinhAnh.Count > 0)
                    {
                        indexCauHoi = 0;
                        HienThiCauHoi();
                    }
                    else
                    {
                        MessageBox.Show("Không có câu hỏi nào thuộc loại LGTDB!");
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy hạng được chọn!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hạng trước!");
            }
            labelDapAn.Text = "";

        }
        private List<string> LayDanhSachHinhAnhCauHoi(string hangDuocChon, string phanLoai)
        {
            List<string> danhSach = new List<string>();
            string query = "";

            // Xác định điều kiện truy vấn dựa vào hạng được chọn
            if (hangDuocChon == "A1")
            {
                query = @"
        SELECT hinh_anh FROM DanhSachCauHoi 
        WHERE check_a1 = 1 AND phan_loai = @phanLoai 
        ORDER BY id";
            }
            else if (hangDuocChon == "A")
            {
                query = @"
        SELECT hinh_anh FROM DanhSachCauHoi 
        WHERE check_a = 1 AND phan_loai = @phanLoai 
        ORDER BY id";
            }
            else if (hangDuocChon == "B")
            {
                query = @"
        SELECT hinh_anh FROM DanhSachCauHoi 
        WHERE check_b = 1 AND phan_loai = @phanLoai 
        ORDER BY id";
            }
            else if (hangDuocChon == "B1")
            {
                query = @"
        SELECT hinh_anh FROM DanhSachCauHoi 
        WHERE check_b1 = 1 AND phan_loai = @phanLoai 
        ORDER BY id";
            }
            else if (new List<string> {"C", "C1", "D1", "D2", "D", "RoMooc" }.Contains(hangDuocChon))
            {
                query = @"
        SELECT hinh_anh FROM DanhSachCauHoi 
        WHERE phan_loai = @phanLoai 
        ORDER BY id";
            }
            else
            {
                MessageBox.Show("Hạng được chọn không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return danhSach; // Trả về danh sách rỗng
            }

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@phanLoai", phanLoai);

                conn.Open();
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    danhSach.Add(reader["hinh_anh"].ToString());
                }
            }

            return danhSach;
        }


        private void button5_Click(object sender, EventArgs e)
        {
            checkPhanLoai = "NVVT";
            if (cbbForm2.SelectedItem != null)
            {
                string hangDuocChon = cbbForm2.SelectedItem.ToString();
                int idHang = GetIdHangFromTenHang(hangDuocChon);

                if (idHang > 0)
                {
                    // Lấy danh sách câu hỏi với phan_loai = "Nghiệp vụ vận tải"
                    danhSachHinhAnh = LayDanhSachHinhAnhCauHoi(hangDuocChon, "NVVT");

                    // Đếm số lượng câu hỏi loại "LGTDB"
                    int demCauHoi = danhSachHinhAnh.Count;
                    labelTongSoCau.Text = demCauHoi.ToString();

                    if (danhSachHinhAnh.Count > 0)
                    {
                        indexCauHoi = 0;
                        HienThiCauHoi();
                    }
                    else
                    {
                        MessageBox.Show("Không có câu hỏi nào thuộc loại NVVT!");
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy hạng được chọn!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hạng trước!");
            }
            labelDapAn.Text = "";
        }

        private void ttnTimCau_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu textbox không rỗng
            if (!string.IsNullOrWhiteSpace(tbTimCau.Text))
            {
                // Chuyển đổi giá trị từ textbox thành số nguyên
                if (int.TryParse(tbTimCau.Text, out int soThuTu))
                {
                    // Kiểm tra xem số thứ tự có hợp lệ không
                    if (soThuTu > 0 && soThuTu <= danhSachHinhAnh.Count)
                    {
                        // Chuyển đến câu hỏi tương ứng (lưu ý index bắt đầu từ 0)
                        indexCauHoi = soThuTu - 1;
                        HienThiCauHoi();
                    }
                    else
                    {
                        MessageBox.Show("Số thứ tự câu không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập số hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập số thứ tự câu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            labelDapAn.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            checkPhanLoai = "KTLX";
            if (cbbForm2.SelectedItem != null)
            {
                string hangDuocChon = cbbForm2.SelectedItem.ToString();
                int idHang = GetIdHangFromTenHang(hangDuocChon);

                if (idHang > 0)
                {
                    // Lấy danh sách câu hỏi với phan_loai = "Kĩ thuật lái"
                    danhSachHinhAnh = LayDanhSachHinhAnhCauHoi(hangDuocChon, "KTLX");

                    // Đếm số lượng câu hỏi loại "LGTDB"
                    int demCauHoi = danhSachHinhAnh.Count;
                    labelTongSoCau.Text = demCauHoi.ToString();

                    if (danhSachHinhAnh.Count > 0)
                    {
                        indexCauHoi = 0;
                        HienThiCauHoi();
                    }
                    else
                    {
                        MessageBox.Show("Không có câu hỏi nào thuộc loại Kỹ thuật lái!");
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy hạng được chọn!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hạng trước!");
            }
            labelDapAn.Text = "";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            checkPhanLoai = "VHDD";
            if (cbbForm2.SelectedItem != null)
            {
                string hangDuocChon = cbbForm2.SelectedItem.ToString();
                int idHang = GetIdHangFromTenHang(hangDuocChon);

                if (idHang > 0)
                {
                    // Lấy danh sách câu hỏi với phan_loai = "Đạo đức văn hóa"
                    danhSachHinhAnh = LayDanhSachHinhAnhCauHoi(hangDuocChon, "VHDD");

                    // Đếm số lượng câu hỏi loại "DDVH"
                    int demCauHoi = danhSachHinhAnh.Count;
                    labelTongSoCau.Text = demCauHoi.ToString();

                    if (danhSachHinhAnh.Count > 0)
                    {
                        indexCauHoi = 0;
                        HienThiCauHoi();
                    }
                    else
                    {
                        MessageBox.Show("Không có câu hỏi nào thuộc loại Đạo đức văn hóa!");
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy hạng được chọn!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hạng trước!");
            }
            labelDapAn.Text = "";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            checkPhanLoai = "CTSCX";
            if (cbbForm2.SelectedItem != null)
            {
                string hangDuocChon = cbbForm2.SelectedItem.ToString();
                int idHang = GetIdHangFromTenHang(hangDuocChon);

                if (idHang > 0)
                {
                    // Lấy danh sách câu hỏi với phan_loai = "Cấu tạo Ô tô"
                    danhSachHinhAnh = LayDanhSachHinhAnhCauHoi(hangDuocChon, "CTSCX");

                    // Đếm số lượng câu hỏi loại "CTOT"
                    int demCauHoi = danhSachHinhAnh.Count;
                    labelTongSoCau.Text = demCauHoi.ToString();

                    if (danhSachHinhAnh.Count > 0)
                    {
                        indexCauHoi = 0;
                        HienThiCauHoi();
                    }
                    else
                    {
                        MessageBox.Show("Không có câu hỏi nào thuộc loại Cấu tạo ô tô!");
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy hạng được chọn!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hạng trước!");
            }
            labelDapAn.Text = "";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            checkPhanLoai = "BB";
            if (cbbForm2.SelectedItem != null)
            {
                string hangDuocChon = cbbForm2.SelectedItem.ToString();
                int idHang = GetIdHangFromTenHang(hangDuocChon);

                if (idHang > 0)
                {
                    // Lấy danh sách câu hỏi với phan_loai = "Biển báo"
                    danhSachHinhAnh = LayDanhSachHinhAnhCauHoi(hangDuocChon, "BB");

                    // Đếm số lượng câu hỏi loại "BB"
                    int demCauHoi = danhSachHinhAnh.Count;
                    labelTongSoCau.Text = demCauHoi.ToString();

                    if (danhSachHinhAnh.Count > 0)
                    {
                        indexCauHoi = 0;
                        HienThiCauHoi();
                    }
                    else
                    {
                        MessageBox.Show("Không có câu hỏi nào thuộc loại Biển báo!");
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy hạng được chọn!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hạng trước!");
            }
            labelDapAn.Text = "";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            checkPhanLoai = "SH";
            if (cbbForm2.SelectedItem != null)
            {
                string hangDuocChon = cbbForm2.SelectedItem.ToString();
                int idHang = GetIdHangFromTenHang(hangDuocChon);

                if (idHang > 0)
                {
                    // Lấy danh sách câu hỏi theo phân loại "SH" (Sa hình)
                    danhSachHinhAnh = LayDanhSachHinhAnhCauHoi(hangDuocChon, "SH");

                    // Đếm số lượng câu hỏi
                    int demCauHoi = danhSachHinhAnh.Count;
                    labelTongSoCau.Text = demCauHoi.ToString();

                    if (demCauHoi > 0)
                    {
                        indexCauHoi = 0; // Đặt vị trí câu hỏi đầu tiên
                        HienThiCauHoi(); // Hiển thị nội dung câu hỏi

                        // Lấy đáp án của câu hỏi đầu tiên theo phân loại "NVVT"
                    }
                    else
                    {
                        MessageBox.Show("Không có câu hỏi nào thuộc loại Sa hình!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        labelDapAn.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy hạng được chọn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hạng trước!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            labelDapAn.Text = "";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            checkPhanLoai = "cauLiet";
            if (cbbForm2.SelectedItem != null)
            {
                string hangDuocChon = cbbForm2.SelectedItem.ToString();

                // Lấy danh sách câu hỏi chỉ có cau_liet = 1 (bỏ phan_loai)
                danhSachHinhAnh = LayDanhSachCauHoiLiet(hangDuocChon);

                // Đếm số lượng câu hỏi cau_liet = 1
                int demCauHoi = danhSachHinhAnh.Count;
                labelTongSoCau.Text = demCauHoi.ToString();

                if (danhSachHinhAnh.Count > 0)
                {
                    indexCauHoi = 0;
                    HienThiCauHoi();
                }
                else
                {
                    MessageBox.Show("Không có câu hỏi nào là câu liệt!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hạng trước!");
            }
            labelDapAn.Text = "";
        }


        private List<string> LayDanhSachCauHoiLiet(string hangDuocChon)
        {
            List<string> danhSach = new List<string>();
            string query = "";

            // Xác định truy vấn theo từng hạng
            if (hangDuocChon == "A1")
            {
                query = @"
        SELECT hinh_anh FROM DanhSachCauHoi 
        WHERE check_a1 = 1 AND cau_liet = 1 
        ORDER BY id";
            }
            else if (hangDuocChon == "A")
            {
                query = @"
        SELECT hinh_anh FROM DanhSachCauHoi 
        WHERE check_a = 1 AND cau_liet = 1 
        ORDER BY id";
            }
            else if (hangDuocChon == "B")
            {
                query = @"
        SELECT hinh_anh FROM DanhSachCauHoi 
        WHERE check_b = 1 AND cau_liet = 1 
        ORDER BY id";
            }
            else if (hangDuocChon == "B1")
            {
                query = @"
        SELECT hinh_anh FROM DanhSachCauHoi 
        WHERE check_b1 = 1 AND cau_liet = 1 
        ORDER BY id";
            }
            else if (new List<string> { "C", "C1", "D1", "D2", "D", "RoMooc" }.Contains(hangDuocChon))
            {
                query = @"
        SELECT hinh_anh FROM DanhSachCauHoi 
        WHERE cau_liet = 1 
        ORDER BY id";
            }
            else
            {
                MessageBox.Show("Hạng được chọn không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return danhSach; // Trả về danh sách rỗng nếu không hợp lệ
            }

            // Kết nối và truy vấn SQLite
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand(query, conn);

                conn.Open();
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!reader.IsDBNull(reader.GetOrdinal("hinh_anh"))) // Kiểm tra NULL trước khi lấy dữ liệu
                    {
                        danhSach.Add(reader["hinh_anh"].ToString());
                    }
                }
            }

            return danhSach;
        }

        private void pictureBoxCauHoi_Click(object sender, EventArgs e)
        {

        }
        private string checkPhanLoai;
    }
    }
    
    

