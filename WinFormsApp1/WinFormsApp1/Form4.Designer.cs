namespace WinFormsApp1
{
    partial class Form4
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            this.panelCauTraLoi = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblTimerThi = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ptbCauHoiThi = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timerThi = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.labelHangXe = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.labelKetQua = new System.Windows.Forms.Label();
            this.labelSoBaoDanh = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.labelHienThiKQ = new System.Windows.Forms.Label();
            this.labelCauDung = new System.Windows.Forms.Label();
            this.labelCauSai = new System.Windows.Forms.Label();
            this.labelSoCauDung = new System.Windows.Forms.Label();
            this.labelSoCauSai = new System.Windows.Forms.Label();
            this.labelCauLiet = new System.Windows.Forms.Label();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbCauHoiThi)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelCauTraLoi
            // 
            this.panelCauTraLoi.BackColor = System.Drawing.Color.PaleTurquoise;
            this.panelCauTraLoi.Location = new System.Drawing.Point(1033, 32);
            this.panelCauTraLoi.Name = "panelCauTraLoi";
            this.panelCauTraLoi.Size = new System.Drawing.Size(335, 640);
            this.panelCauTraLoi.TabIndex = 1;
            this.panelCauTraLoi.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Blue;
            this.panel4.Controls.Add(this.lblTimerThi);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Location = new System.Drawing.Point(1033, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(335, 33);
            this.panel4.TabIndex = 0;
            this.panel4.Paint += new System.Windows.Forms.PaintEventHandler(this.panel4_Paint);
            // 
            // lblTimerThi
            // 
            this.lblTimerThi.AutoSize = true;
            this.lblTimerThi.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTimerThi.ForeColor = System.Drawing.Color.Lime;
            this.lblTimerThi.Location = new System.Drawing.Point(214, 4);
            this.lblTimerThi.Name = "lblTimerThi";
            this.lblTimerThi.Size = new System.Drawing.Size(49, 20);
            this.lblTimerThi.TabIndex = 1;
            this.lblTimerThi.Text = "00:00";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.DarkOrange;
            this.label4.Location = new System.Drawing.Point(75, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "THỜI GIAN CÒN LẠI : ";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Magenta;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(1033, 669);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(335, 37);
            this.button1.TabIndex = 2;
            this.button1.Text = "KẾT THÚC BÀI THI ";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.ptbCauHoiThi);
            this.panel1.Location = new System.Drawing.Point(-4, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1036, 543);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // ptbCauHoiThi
            // 
            this.ptbCauHoiThi.Location = new System.Drawing.Point(3, 3);
            this.ptbCauHoiThi.Name = "ptbCauHoiThi";
            this.ptbCauHoiThi.Size = new System.Drawing.Size(1033, 537);
            this.ptbCauHoiThi.TabIndex = 0;
            this.ptbCauHoiThi.TabStop = false;
            this.ptbCauHoiThi.Click += new System.EventHandler(this.ptbCauHoiThi_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Location = new System.Drawing.Point(804, 542);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(228, 164);
            this.panel3.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(35, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "NGUYỄN TRÌNH ";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(23, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "TRUNG TÂM SH - DT LÁI XE ";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(51, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "PHẦN MỀM THUỘC ";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // timerThi
            // 
            this.timerThi.Interval = 1000;
            this.timerThi.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-12, 542);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(142, 164);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(160, 546);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(175, 21);
            this.label5.TabIndex = 5;
            this.label5.Text = "THÔNG TIN THÍ SINH ";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(158, 634);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 20);
            this.label6.TabIndex = 6;
            this.label6.Text = "Họ tên : ";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(232, 634);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(156, 20);
            this.label7.TabIndex = 7;
            this.label7.Text = "Vũ Thị Khánh Huyền ";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(248, 595);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 20);
            this.label8.TabIndex = 8;
            this.label8.Text = "Hạng xe : ";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // labelHangXe
            // 
            this.labelHangXe.AutoSize = true;
            this.labelHangXe.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelHangXe.ForeColor = System.Drawing.Color.Red;
            this.labelHangXe.Location = new System.Drawing.Point(332, 595);
            this.labelHangXe.Name = "labelHangXe";
            this.labelHangXe.Size = new System.Drawing.Size(13, 20);
            this.labelHangXe.TabIndex = 9;
            this.labelHangXe.Text = ".";
            this.labelHangXe.Click += new System.EventHandler(this.label9_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label10.Location = new System.Drawing.Point(160, 595);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 20);
            this.label10.TabIndex = 10;
            this.label10.Text = "SBD : ";
            // 
            // labelKetQua
            // 
            this.labelKetQua.AutoSize = true;
            this.labelKetQua.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelKetQua.ForeColor = System.Drawing.Color.MidnightBlue;
            this.labelKetQua.Location = new System.Drawing.Point(531, 546);
            this.labelKetQua.Name = "labelKetQua";
            this.labelKetQua.Size = new System.Drawing.Size(85, 21);
            this.labelKetQua.TabIndex = 11;
            this.labelKetQua.Text = "KẾT QUẢ :";
            this.labelKetQua.Visible = false;
            // 
            // labelSoBaoDanh
            // 
            this.labelSoBaoDanh.AutoSize = true;
            this.labelSoBaoDanh.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelSoBaoDanh.ForeColor = System.Drawing.Color.Red;
            this.labelSoBaoDanh.Location = new System.Drawing.Point(213, 595);
            this.labelSoBaoDanh.Name = "labelSoBaoDanh";
            this.labelSoBaoDanh.Size = new System.Drawing.Size(13, 20);
            this.labelSoBaoDanh.TabIndex = 12;
            this.labelSoBaoDanh.Text = ".";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label9.Location = new System.Drawing.Point(158, 669);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 20);
            this.label9.TabIndex = 13;
            this.label9.Text = "CCCD : ";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(223, 669);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(99, 20);
            this.label12.TabIndex = 14;
            this.label12.Text = "8401372655";
            // 
            // labelHienThiKQ
            // 
            this.labelHienThiKQ.AutoSize = true;
            this.labelHienThiKQ.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelHienThiKQ.ForeColor = System.Drawing.Color.Crimson;
            this.labelHienThiKQ.Location = new System.Drawing.Point(618, 546);
            this.labelHienThiKQ.Name = "labelHienThiKQ";
            this.labelHienThiKQ.Size = new System.Drawing.Size(66, 21);
            this.labelHienThiKQ.TabIndex = 15;
            this.labelHienThiKQ.Text = "label13";
            this.labelHienThiKQ.Visible = false;
            // 
            // labelCauDung
            // 
            this.labelCauDung.AutoSize = true;
            this.labelCauDung.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelCauDung.Location = new System.Drawing.Point(531, 595);
            this.labelCauDung.Name = "labelCauDung";
            this.labelCauDung.Size = new System.Drawing.Size(151, 20);
            this.labelCauDung.TabIndex = 16;
            this.labelCauDung.Text = "Số câu trả lời đúng : ";
            this.labelCauDung.Visible = false;
            // 
            // labelCauSai
            // 
            this.labelCauSai.AutoSize = true;
            this.labelCauSai.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelCauSai.Location = new System.Drawing.Point(531, 638);
            this.labelCauSai.Name = "labelCauSai";
            this.labelCauSai.Size = new System.Drawing.Size(134, 20);
            this.labelCauSai.TabIndex = 17;
            this.labelCauSai.Text = "Số câu trả lời sai : ";
            this.labelCauSai.Visible = false;
            // 
            // labelSoCauDung
            // 
            this.labelSoCauDung.AutoSize = true;
            this.labelSoCauDung.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelSoCauDung.ForeColor = System.Drawing.Color.Blue;
            this.labelSoCauDung.Location = new System.Drawing.Point(688, 595);
            this.labelSoCauDung.Name = "labelSoCauDung";
            this.labelSoCauDung.Size = new System.Drawing.Size(60, 20);
            this.labelSoCauDung.TabIndex = 18;
            this.labelSoCauDung.Text = "label16";
            this.labelSoCauDung.Visible = false;
            // 
            // labelSoCauSai
            // 
            this.labelSoCauSai.AutoSize = true;
            this.labelSoCauSai.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelSoCauSai.ForeColor = System.Drawing.Color.Red;
            this.labelSoCauSai.Location = new System.Drawing.Point(671, 638);
            this.labelSoCauSai.Name = "labelSoCauSai";
            this.labelSoCauSai.Size = new System.Drawing.Size(60, 20);
            this.labelSoCauSai.TabIndex = 19;
            this.labelSoCauSai.Text = "label17";
            this.labelSoCauSai.Visible = false;
            this.labelSoCauSai.Click += new System.EventHandler(this.labelSoCauSai_Click);
            // 
            // labelCauLiet
            // 
            this.labelCauLiet.AutoSize = true;
            this.labelCauLiet.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelCauLiet.ForeColor = System.Drawing.Color.DarkOrange;
            this.labelCauLiet.Location = new System.Drawing.Point(531, 676);
            this.labelCauLiet.Name = "labelCauLiet";
            this.labelCauLiet.Size = new System.Drawing.Size(0, 20);
            this.labelCauLiet.TabIndex = 20;
            this.labelCauLiet.Visible = false;
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(1370, 710);
            this.Controls.Add(this.labelCauLiet);
            this.Controls.Add(this.labelSoCauSai);
            this.Controls.Add(this.labelSoCauDung);
            this.Controls.Add(this.labelCauSai);
            this.Controls.Add(this.labelCauDung);
            this.Controls.Add(this.labelHienThiKQ);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.labelSoBaoDanh);
            this.Controls.Add(this.labelKetQua);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.labelHangXe);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panelCauTraLoi);
            this.Controls.Add(this.panel1);
            this.Name = "Form4";
            this.Text = "Form4";
            this.Load += new System.EventHandler(this.Form4_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form4_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form4_KeyDown);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ptbCauHoiThi)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Panel panelCauTraLoi;
        private Button button1;
        private Panel panel1;
        private Panel panel3;
        private Panel panel4;
        private System.Windows.Forms.Timer timerThi;
        private Label label2;
        private Label label1;
        private Label label3;
        private PictureBox ptbCauHoiThi;
        private Label label4;
        private Label lblTimerThi;
        private PictureBox pictureBox1;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label labelHangXe;
        private Label label10;
        private Label labelKetQua;
        private Label labelSoBaoDanh;
        private Label label9;
        private Label label12;
        private Label labelHienThiKQ;
        private Label labelCauDung;
        private Label labelCauSai;
        private Label labelSoCauDung;
        private Label labelSoCauSai;
        private Label labelCauLiet;
    }
}