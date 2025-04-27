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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using Microsoft.VisualBasic;
using System.Reflection;
using System.Globalization;

namespace WinFormsApp1
{
   
    public partial class Form4 : Form
    {
        private string connectionString = "Data Source=ThiThuLaiXe.db;Version=3;";
        private string hangXe;
        private int boDe;
        private int soCauThi;
        private string SBD;
        private int remainingTime = 1000;
        private List<string> danhSachHinhAnh;
        private int cauHoiHienTai = 0;
     
        private int index = 0;
        private List<Panel> danhSachCauHoiPanel = new List<Panel>(); // Danh sách panel câu hỏi
        private List<int> danhSachDapAn = new List<int>();
        private List<int> danhSachSoDapAn = new List<int>();
        private List<int> danhSachCauLiet = new List<int>();
        private Dictionary<int, bool> trangThaiCauHoi = new Dictionary<int, bool>();
        public Form4(string hangXe, int boDe, string SBD)
        {
            InitializeComponent();
            this.hangXe = hangXe;
            this.boDe = boDe;
            this.SBD = SBD;
          
            remainingTime = LayThoiGianThi(boDe) * 60;
            timerThi.Interval = 1000;
            this.KeyPreview = true; // Cho phép Form nhận sự kiện phím
            this.KeyDown += new KeyEventHandler(Form4_KeyDown);
            this.KeyPress += new KeyPressEventHandler(Form4_KeyPress);

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
      


        private void Form4_Load(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new CultureInfo("en-US"));
            this.KeyPreview = true;
            this.WindowState = FormWindowState.Maximized;
            this.ControlBox = false;  
            this.Text = "";           
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            ptbCauHoiThi.SizeMode = PictureBoxSizeMode.StretchImage; 

            labelHangXe.Text = hangXe;
            labelSoBaoDanh.Text = SBD;
            this.Text = $"Thi sát hạch {hangXe}";
            timerThi.Interval = 1000; 
            timerThi.Start(); 
            lblTimerThi.Text = $"{remainingTime / 60:D2}:{remainingTime % 60:D2}";
            danhSachHinhAnh = LayDanhSachHinhAnh(boDe); 
            danhSachDapAn = LayDanhSachDapAn(boDe);
            danhSachSoDapAn = LayDanhSachSoDapAn(boDe);
            danhSachCauLiet = LayDanhSachCauLiet(boDe);
            if (danhSachHinhAnh.Count > 0)
            {
                HienThiCauHoi(cauHoiHienTai);
            }
            
            this.KeyPreview = true; 
            this.KeyDown += Form4_KeyDown; 
         
            string connectionString = "Data Source=ThiThuLaiXe.db;Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT so_cau_thi FROM BoDe WHERE id = @boDe";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@boDe", boDe);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        soCauThi = Convert.ToInt32(result);
                    }
                }
            }
            LoadCauHoi();
            HighlightSelectedQuestion(index);
        }

        private Panel selectedPanel = null;
        private Label selectedLabel = null;


        private void LoadCauHoi()
        {
         
            panelCauTraLoi.Controls.Clear();
            panelCauTraLoi.AutoScroll = true;
            danhSachCauHoiPanel.Clear();

            int totalQuestions = soCauThi;
            int rows = 15;
            int columns = (soCauThi == 45 || soCauThi == 40 || soCauThi == 35) ? 3 : 2;

            TableLayoutPanel tableLayout = new TableLayoutPanel
            {
                ColumnCount = columns,
                RowCount = rows,
                AutoSize = true,
                Dock = DockStyle.Top,
                Padding = new Padding(5),
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None
            };

            for (int c = 0; c < columns; c++)
            {
                for (int r = 0; r < rows; r++)
                {
                    int questionNumber = c * rows + r;
                    if (questionNumber >= totalQuestions) break;

                    // Lấy số lượng đáp án từ danh sách danhSachSoDapAn
                    int soDapAn = danhSachSoDapAn[questionNumber];

                    // Panel chứa toàn bộ câu hỏi
                    Panel containerPanel = new Panel
                    {
                        Size = new Size(100, 36),
                        BackColor = trangThaiCauHoi.ContainsKey(questionNumber + 1) && trangThaiCauHoi[questionNumber + 1] ? Color.LimeGreen : Color.LightBlue,
                        BorderStyle = BorderStyle.FixedSingle,
                        Tag = questionNumber + 1
                    };
                    danhSachCauHoiPanel.Add(containerPanel);

                    // Panel chứa số thứ tự trong khung
                    Panel sttPanel = new Panel
                    {
                        Size = new Size(30, 20),
                        BackColor = Color.White,
                        BorderStyle = BorderStyle.FixedSingle,
                        Location = new Point(0, 10)
                    };

                    Label lblSTT = new Label
                    {
                        Text = (questionNumber + 1).ToString(),
                        ForeColor = Color.Blue,
                        Font = new Font("Arial", 10, FontStyle.Bold),
                        AutoSize = false,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Dock = DockStyle.Fill
                    };

                    sttPanel.Controls.Add(lblSTT);
                    containerPanel.Controls.Add(sttPanel);

                    // Panel chứa phần trả lời
                    Panel answerPanel = new Panel
                    {
                        Size = new Size(70, 50),
                        Location = new Point(30, 0)
                    };

                    Panel numberPanel = new Panel { Size = new Size(65, 15), Location = new Point(3, 2) };
                    for (int j = 1; j <= soDapAn; j++) // Hiển thị số lượng đúng số đáp án
                    {
                        Label lblNumber = new Label
                        {
                            Text = j.ToString(),
                            Font = new Font("Arial", 8, FontStyle.Bold),
                            ForeColor = Color.Blue,
                            AutoSize = true,
                            Location = new Point((j - 1) * 15, 0)
                        };
                        numberPanel.Controls.Add(lblNumber);
                    }
                    answerPanel.Controls.Add(numberPanel);

                    // CheckBox chọn đáp án
                    Panel checkboxPanel = new Panel { Size = new Size(65, 20), Location = new Point(3, 20) };
                    List<CheckBox> checkBoxes = new List<CheckBox>();

                    for (int j = 1; j <= soDapAn; j++) // Tạo đúng số lượng checkbox
                    {
                        CheckBox cb = new CheckBox
                        {
                            Size = new Size(15, 15),
                            Location = new Point((j - 1) * 15, 0)
                        };

                        // Nếu câu này đã được chọn từ trước, đặt lại trạng thái
                        if (trangThaiCauHoi.ContainsKey(questionNumber + 1) && trangThaiCauHoi[questionNumber + 1])
                        {
                            cb.Checked = true;
                        }

                        checkBoxes.Add(cb);
                        checkboxPanel.Controls.Add(cb);
                    }

                    foreach (var cb in checkBoxes)
                    {
                        cb.CheckedChanged += (sender, e) =>
                        {
                            bool isAnswered = checkBoxes.Any(chk => chk.Checked);
                            trangThaiCauHoi[questionNumber + 1] = isAnswered; // Cập nhật trạng thái vào Dictionary

                            // Nếu người dùng chưa chọn đáp án nào, đổi màu cam
                            containerPanel.BackColor = isAnswered ? Color.LimeGreen : Color.Orange;
                        };
                    }

                    answerPanel.Controls.Add(checkboxPanel);
                    containerPanel.Controls.Add(answerPanel);
                    tableLayout.Controls.Add(containerPanel, c, r);
                }
            }
            panelCauTraLoi.Controls.Add(tableLayout);
        }

        private void HighlightSelectedQuestion(Panel panel, Label lblSTT)
        {
            foreach (var pnl in danhSachCauHoiPanel)
            {
                pnl.BackColor = Color.LightBlue; 
                foreach (Control ctrl in pnl.Controls)
                {
                    if (ctrl is Label lbl)
                    {
                        lbl.ForeColor = Color.Blue; 
                    }
                }
            }
            selectedPanel = panel;
            selectedLabel = lblSTT;

            selectedPanel.BackColor = Color.Orange; 
            selectedLabel.ForeColor = Color.Red;
           
        }

        private int cauLiet = 0;
        private int HienThiDapAn()
        {
            if (danhSachDapAn == null || danhSachDapAn.Count == 0)
                return 0;

            int checkCauDung = 0;
            int checkCauLiet = 0;

            for (int i = 0; i < danhSachDapAn.Count; i++)
            {
                int dapAnDung = danhSachDapAn[i];
              //  MessageBox.Show($"Câu {i + 1}: Đáp án đúng = {danhSachDapAn[i]}, Câu liệt = {(danhSachCauLiet.Count > i ? danhSachCauLiet[i] : "N/A")}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (i >= danhSachCauHoiPanel.Count) continue;
                Panel containerPanel = danhSachCauHoiPanel[i];

                Panel answerPanel = containerPanel.Controls.OfType<Panel>()
                                            .FirstOrDefault(p => p.Size == new Size(70, 50));
                if (answerPanel == null) continue;

                Panel checkboxPanel = answerPanel.Controls.OfType<Panel>()
                                            .FirstOrDefault(p => p.Size == new Size(65, 20));
                if (checkboxPanel == null) continue;

                List<CheckBox> checkBoxes = checkboxPanel.Controls.OfType<CheckBox>().ToList();
                int dapAnNguoiDung = checkBoxes.FindIndex(cb => cb.Checked) + 1;

                if (dapAnNguoiDung == dapAnDung)
                {
                    checkCauDung++;
                }
                else
                {
                    // Kiểm tra danhSachCauLiet có giá trị hợp lệ không
                    if (danhSachCauLiet != null && danhSachCauLiet.Count > i && danhSachCauLiet[i] == 1)
                    {
                        checkCauLiet = 1;
                    }
                }

                // Đánh dấu màu sắc
                if (dapAnDung >= 1 && dapAnDung <= checkBoxes.Count)
                {
                    checkBoxes[dapAnDung - 1].BackColor = Color.LightGreen;
                }

                if (dapAnNguoiDung != 0 && dapAnNguoiDung != dapAnDung)
                {
                    checkBoxes[dapAnNguoiDung - 1].BackColor = Color.LightCoral;
                }
            }

            cauLiet = checkCauLiet; // Lưu trạng thái câu liệt

            return checkCauDung;
        }









        private void HienThiCauHoi(int index)
        {
            if (index < 0 || index >= danhSachHinhAnh.Count) return;

            string imageName = Path.GetFileName(danhSachHinhAnh[index]);
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images", imageName);

            if (File.Exists(imagePath))
            {
                ptbCauHoiThi.Image = Image.FromFile(imagePath);
            }
            else
            {
                MessageBox.Show($"Không tìm thấy ảnh: {imagePath}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            HighlightSelectedQuestion(index);
        }
        private int previousIndex = -1;
        private Color previousColor;

        private void HighlightSelectedQuestion(int index)
        {
            if (index < 0 || index >= danhSachCauHoiPanel.Count)
                return;

            if (checkSukien == 1)
            {
                // Nếu có ô trước đó hợp lệ, khôi phục màu cũ
                if (previousIndex >= 0 && previousIndex < danhSachCauHoiPanel.Count)
                {
                    danhSachCauHoiPanel[previousIndex].BackColor = previousColor;
                }

                // Lưu màu nền của ô mới trước khi đổi sang màu cam
                previousColor = danhSachCauHoiPanel[index].BackColor;

                // Đổi màu cam cho ô hiện tại
                danhSachCauHoiPanel[index].BackColor = Color.Orange;

                // Cập nhật previousIndex
                previousIndex = index;
                return;
            }

            // Trường hợp checkSukien != 1, xử lý bình thường
            for (int i = 0; i < danhSachCauHoiPanel.Count; i++)
            {
                if (trangThaiCauHoi.ContainsKey(i + 1) && trangThaiCauHoi[i + 1])
                {
                    danhSachCauHoiPanel[i].BackColor = Color.LimeGreen;
                }
                else
                {
                    danhSachCauHoiPanel[i].BackColor = Color.LightBlue;
                }
            }

            // Đánh dấu màu cam cho ô đang chọn
            danhSachCauHoiPanel[index].BackColor = Color.Orange;
        }




        private void Form4_KeyDown(object sender, KeyEventArgs e)
        {
            if (danhSachHinhAnh.Count == 0) return;

            // Điều hướng câu hỏi
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                if (cauHoiHienTai < danhSachHinhAnh.Count - 1)
                {
                    cauHoiHienTai++;
                    HienThiCauHoi(cauHoiHienTai);
                }
            }
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                if (cauHoiHienTai > 0)
                {
                    cauHoiHienTai--;
                    HienThiCauHoi(cauHoiHienTai);
                }
            }
        }
        // Chọn đáp án 
        private void Form4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (danhSachHinhAnh.Count == 0) return;
            int index = -1;
            if (e.KeyChar == '1') index = 0;
            else if (e.KeyChar == '2') index = 1;
            else if (e.KeyChar == '3') index = 2;
            else if (e.KeyChar == '4') index = 3;

            if (index != -1)
            {
                ChonDapAn(index);
                this.Invalidate(); // Cập nhật giao diện
            }
        }



        private void ChonDapAn(int index)
        {
            // Lấy panel chứa câu hỏi hiện tại
            Panel containerPanel = danhSachCauHoiPanel[cauHoiHienTai];

            // Tìm panel chứa các checkbox
            Panel answerPanel = containerPanel.Controls
                .OfType<Panel>()
                .FirstOrDefault(p => p.Size == new Size(70, 50));

            if (answerPanel == null)
            {
                Console.WriteLine("Lỗi: Không tìm thấy panel chứa câu trả lời!");
                return;
            }

            Panel checkboxPanel = answerPanel.Controls
                .OfType<Panel>()
                .FirstOrDefault(p => p.Size == new Size(65, 20));

            if (checkboxPanel == null)
            {
                Console.WriteLine("Lỗi: Không tìm thấy panel chứa checkbox!");
                return;
            }

            List<CheckBox> checkBoxes = checkboxPanel.Controls
                .OfType<CheckBox>()
                .ToList();

            if (checkBoxes.Count == 0)
            {
                Console.WriteLine("Lỗi: Không có checkbox nào để chọn!");
                return;
            }

            // Nếu index lớn hơn số checkbox hiện có, bỏ qua
            if (index >= checkBoxes.Count)
            {
                Console.WriteLine($"Lỗi: Chỉ có {checkBoxes.Count} checkbox, nhưng yêu cầu chọn index {index}!");
                return;
            }

            // Nếu checkbox đã được chọn trước đó, bỏ chọn nó
            if (checkBoxes[index].Checked)
            {
                checkBoxes[index].Checked = false;
            }
            else
            {
                // Bỏ chọn tất cả checkbox khác trước khi chọn checkbox mới
                foreach (var cb in checkBoxes) cb.Checked = false;
                checkBoxes[index].Checked = true;
            }
        }





        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private int LayThoiGianThi(int boDeId)
        {
            int thoiGianThi = 0;

            string connectionString = "Data Source=ThiThuLaiXe.db;Version=3;"; // Thay bằng chuỗi kết nối CSDL
            string query = "SELECT thoi_gian_thi FROM BoDe WHERE id = @boDeId";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@boDeId", boDeId);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        thoiGianThi = Convert.ToInt32(result);
                    }
                }
            }

            return thoiGianThi;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (remainingTime > 0)
            {
                remainingTime--; 
                int minutes = remainingTime / 60;
                int seconds = remainingTime % 60;

                lblTimerThi.Text = $"{minutes:D2}:{seconds:D2}"; 
            }
            else
            {
                timerThi.Stop();
                //MessageBox.Show("Hết thời gian!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                int checkCauDung = HienThiDapAn();

                // Mở Form5 và truyền giá trị checkCauDung
                Form5 form5 = new Form5(SBD, hangXe, checkCauDung, boDe, cauLiet);
                form5.Show();

                // Đóng Form4
                this.Close();
            }

        }
       


        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
        private void HienThiDapAnDung()
        {
            if (danhSachDapAn == null || danhSachDapAn.Count == 0 || danhSachCauHoiPanel == null)
                return;

            for (int i = 0; i < danhSachDapAn.Count; i++)
            {
                int dapAnDung = danhSachDapAn[i]; // Lấy đáp án đúng (1,2,3,4)

                if (i >= danhSachCauHoiPanel.Count) continue;
                Panel containerPanel = danhSachCauHoiPanel[i]; // Lấy panel của câu hỏi i

                // Lấy answerPanel (chứa checkboxPanel)
                Panel answerPanel = containerPanel.Controls.OfType<Panel>()
                                            .FirstOrDefault(p => p.Size == new Size(70, 50));
                if (answerPanel == null) continue;

                // Lấy checkboxPanel (chứa danh sách CheckBox)
                Panel checkboxPanel = answerPanel.Controls.OfType<Panel>()
                                            .FirstOrDefault(p => p.Size == new Size(65, 20));
                if (checkboxPanel == null) continue;

                // Lấy danh sách checkbox
                List<CheckBox> checkBoxes = checkboxPanel.Controls.OfType<CheckBox>().ToList();
                if (checkBoxes.Count == 0) continue;

                // Lấy danh sách đáp án người dùng đã chọn
                List<int> dapAnNguoiDung = checkBoxes
                    .Select((cb, index) => new { cb, index })
                    .Where(x => x.cb.Checked)
                    .Select(x => x.index + 1) // Chuyển về vị trí (1-4)
                    .ToList();

                // Nếu người dùng không chọn đáp án nào => Coi là sai (câu trống)
                bool cauTrong = dapAnNguoiDung.Count == 0;
                bool dungHet = dapAnNguoiDung.Count == 1 && dapAnNguoiDung.Contains(dapAnDung); // Đúng hoàn toàn

                for (int j = 0; j < checkBoxes.Count; j++)
                {
                    checkBoxes[j].FlatStyle = FlatStyle.Popup;

                    if (j + 1 == dapAnDung) // Đáp án đúng
                    {
                        checkBoxes[j].BackColor = Color.Blue;
                        checkBoxes[j].Checked = true;
                    }
                    else if (dapAnNguoiDung.Contains(j + 1)) // Người dùng chọn sai
                    {
                        checkBoxes[j].BackColor = Color.Red;
                        checkBoxes[j].Checked = true;
                    }

                    checkBoxes[j].Font = new Font(checkBoxes[j].Font, FontStyle.Regular);
                    checkBoxes[j].Enabled = false;
                    checkBoxes[j].Refresh();
                }

                // Đổi màu nền Panel câu hỏi
                if (cauTrong)
                {
                    containerPanel.BackColor = Color.LightBlue; // Màu mặc định của Panel
                                                                     // Câu trống (chưa chọn đáp án)
                }
                else if (dungHet)
                {
                    containerPanel.BackColor = Color.LightGreen; // Câu đúng (xanh nhạt)
                }
                else
                {
                    containerPanel.BackColor = Color.LightCoral; // Câu sai (đỏ nhạt)
                }
            }
        }



        private int checkSukien;
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "THOÁT")
            {
                this.Close();
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có muốn kết thúc bài thi?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                int checkCauDung = HienThiDapAn();
                timerThi.Stop();
                Form5 form5 = new Form5(SBD, hangXe, checkCauDung, boDe, cauLiet);
                form5.FormClosed += (s, args) =>
                {
                    checkSukien = 1;
                 // MessageBox.Show("Giá trị của checkSukien: " + checkSukien.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    HienThiDapAnDung();
                    button1.Text = "THOÁT"; 
                    labelKetQua.Visible = true;
                    labelCauDung.Visible = true;
                    labelCauSai.Visible = true;
                    labelSoCauSai.Visible = true;
                    labelSoCauDung.Visible = true;
                    labelHienThiKQ.Visible = true;
                    labelCauLiet.Visible = true;
                    //
                    int cauDatToiThieu = 0;
                    int soCauThi = 0;
            
                    string connectionString = "Data Source=ThiThuLaiXe.db;Version=3;";
                    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();

                    
                        string query = "SELECT so_cau_dat_toi_thieu, so_cau_thi FROM BoDe WHERE id = @boDe";

                        using (SQLiteCommand command = new SQLiteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@boDe", boDe);

                            using (SQLiteDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read()) 
                                {
                                    cauDatToiThieu = reader.GetInt32(0); 
                                    soCauThi = reader.GetInt32(1);
                                }
                            }
                        }
                    }
                    if (cauLiet == 1)
                    {
                        labelHienThiKQ.Text = "TRƯỢT";
                        int soCauSai = soCauThi - checkCauDung;
                        labelSoCauDung.Text = checkCauDung.ToString();
                        labelSoCauSai.Text = soCauSai.ToString();
                        labelCauLiet.Text = "⚠️ Chú ý sai câu điểm liệt nhé 😱";
                    }
                    else
                    {
                        labelSoCauDung.Text = checkCauDung.ToString();
                        int soCauSai = soCauThi - checkCauDung;
                        labelSoCauSai.Text = soCauSai.ToString();

                        if (checkCauDung < cauDatToiThieu)
                        {
                            labelHienThiKQ.Text = "TRƯỢT";
                        }
                        else
                        {
                            labelHienThiKQ.Text = "ĐẠT";
                        }
                    }
                };
                form5.Show();
            }
        }

        private List<string> LayDanhSachHinhAnh(int idBoDe)
        {
            List<string> danhSachHinhAnh = new List<string>();

            string query = @"
               SELECT dsc.hinh_anh 
               FROM BoDe_CauHoi bch
               JOIN DanhSachCauHoi dsc ON bch.id_cau_hoi = dsc.id
               WHERE bch.id_bo_de = @idBoDe";

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idBoDe", idBoDe);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            danhSachHinhAnh.Add(reader["hinh_anh"].ToString());
                        }
                    }
                }
            }

            return danhSachHinhAnh;
        }
        private List<int> LayDanhSachDapAn(int idBoDe)
        {
            List<int> danhSachDapAn = new List<int>();

            string query = @"
               SELECT dsc.dap_an 
               FROM BoDe_CauHoi bch
               JOIN DanhSachCauHoi dsc ON bch.id_cau_hoi = dsc.id
               WHERE bch.id_bo_de = @idBoDe";

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idBoDe", idBoDe);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            danhSachDapAn.Add(Convert.ToInt32(reader["dap_an"])); // Chuyển sang kiểu số nguyên
                        }
                    }
                }
            }

            return danhSachDapAn;
        }
        private List<int> LayDanhSachSoDapAn(int idBoDe)
        {
            List<int> danhSachSoDapAn = new List<int>();

            string query = @"
               SELECT dsc.so_dap_an 
               FROM BoDe_CauHoi bch
               JOIN DanhSachCauHoi dsc ON bch.id_cau_hoi = dsc.id
               WHERE bch.id_bo_de = @idBoDe";

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idBoDe", idBoDe);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            danhSachSoDapAn.Add(Convert.ToInt32(reader["so_dap_an"])); // Chuyển sang kiểu số nguyên
                        }
                    }
                }
            }

            return danhSachSoDapAn;
        }
        private List<int> LayDanhSachCauLiet(int idBoDe)
        {
            List<int> danhSachCauLiet = new List<int>();

            string query = @"
              SELECT dsc.cau_liet 
              FROM BoDe_CauHoi bch
              JOIN DanhSachCauHoi dsc ON bch.id_cau_hoi = dsc.id
              WHERE bch.id_bo_de = @idBoDe";

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idBoDe", idBoDe);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Kiểm tra nếu giá trị NULL thì thay bằng 0
                            int cauLiet = reader["cau_liet"] != DBNull.Value ? Convert.ToInt32(reader["cau_liet"]) : 0;
                            danhSachCauLiet.Add(cauLiet);
                        }
                    }
                }
            }

            return danhSachCauLiet;
        }



        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void ptbCauHoiThi_Click(object sender, EventArgs e)
        {

        }

        private void labelSoCauSai_Click(object sender, EventArgs e)
        {

        }
    }
}
