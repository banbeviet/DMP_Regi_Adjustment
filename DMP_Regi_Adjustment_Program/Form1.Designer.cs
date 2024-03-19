namespace DMP_Regi_Adjustment_Program
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.DeviceStatus_groupBox = new System.Windows.Forms.GroupBox();
            this.DeviceStatus_label = new System.Windows.Forms.Label();
            this.DBGMon_groupBox = new System.Windows.Forms.GroupBox();
            this.DBGMonStatus_label = new System.Windows.Forms.Label();
            this.NailY_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.NailM_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.NailC_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.PhotoM_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.PhotoC_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.PrintNail_button = new System.Windows.Forms.Button();
            this.PrintPhoto_button = new System.Windows.Forms.Button();
            this.Nail_Regi_write_button = new System.Windows.Forms.Button();
            this.Photo_Regi_write_button = new System.Windows.Forms.Button();
            this.PrinterSN_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.BluetoothStatus_groupBox = new System.Windows.Forms.GroupBox();
            this.BluetoothStatus_label = new System.Windows.Forms.Label();
            this.Setting_groupBox = new System.Windows.Forms.GroupBox();
            this.cbx_Photo = new System.Windows.Forms.CheckBox();
            this.cbx_Nail = new System.Windows.Forms.CheckBox();
            this.gb_Nail = new System.Windows.Forms.GroupBox();
            this.BTPrintNail_button = new System.Windows.Forms.Button();
            this.Photo_groupbox = new System.Windows.Forms.GroupBox();
            this.BTPrintPhoto_button = new System.Windows.Forms.Button();
            this.statusStrip.SuspendLayout();
            this.DeviceStatus_groupBox.SuspendLayout();
            this.DBGMon_groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NailY_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NailM_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NailC_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhotoM_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhotoC_numericUpDown)).BeginInit();
            this.BluetoothStatus_groupBox.SuspendLayout();
            this.Setting_groupBox.SuspendLayout();
            this.gb_Nail.SuspendLayout();
            this.Photo_groupbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip.Location = new System.Drawing.Point(0, 373);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.statusStrip.Size = new System.Drawing.Size(561, 22);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(157, 17);
            this.toolStripStatusLabel1.Text = "Please re-plug the USB cable";
            // 
            // DeviceStatus_groupBox
            // 
            this.DeviceStatus_groupBox.Controls.Add(this.DeviceStatus_label);
            this.DeviceStatus_groupBox.Location = new System.Drawing.Point(13, 14);
            this.DeviceStatus_groupBox.Name = "DeviceStatus_groupBox";
            this.DeviceStatus_groupBox.Size = new System.Drawing.Size(189, 109);
            this.DeviceStatus_groupBox.TabIndex = 1;
            this.DeviceStatus_groupBox.TabStop = false;
            this.DeviceStatus_groupBox.Text = "Device Status";
            // 
            // DeviceStatus_label
            // 
            this.DeviceStatus_label.AutoSize = true;
            this.DeviceStatus_label.Font = new System.Drawing.Font("Gulim", 18F);
            this.DeviceStatus_label.Location = new System.Drawing.Point(15, 44);
            this.DeviceStatus_label.Name = "DeviceStatus_label";
            this.DeviceStatus_label.Size = new System.Drawing.Size(151, 24);
            this.DeviceStatus_label.TabIndex = 0;
            this.DeviceStatus_label.Text = "Disconnected";
            // 
            // DBGMon_groupBox
            // 
            this.DBGMon_groupBox.Controls.Add(this.DBGMonStatus_label);
            this.DBGMon_groupBox.Location = new System.Drawing.Point(207, 14);
            this.DBGMon_groupBox.Name = "DBGMon_groupBox";
            this.DBGMon_groupBox.Size = new System.Drawing.Size(109, 109);
            this.DBGMon_groupBox.TabIndex = 6;
            this.DBGMon_groupBox.TabStop = false;
            this.DBGMon_groupBox.Text = "DBGMon Status";
            // 
            // DBGMonStatus_label
            // 
            this.DBGMonStatus_label.AutoSize = true;
            this.DBGMonStatus_label.Font = new System.Drawing.Font("Gulim", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DBGMonStatus_label.Location = new System.Drawing.Point(27, 44);
            this.DBGMonStatus_label.Name = "DBGMonStatus_label";
            this.DBGMonStatus_label.Size = new System.Drawing.Size(63, 27);
            this.DBGMonStatus_label.TabIndex = 0;
            this.DBGMonStatus_label.Text = "---";
            // 
            // NailY_numericUpDown
            // 
            this.NailY_numericUpDown.DecimalPlaces = 2;
            this.NailY_numericUpDown.Enabled = false;
            this.NailY_numericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.NailY_numericUpDown.Location = new System.Drawing.Point(86, 32);
            this.NailY_numericUpDown.Maximum = new decimal(new int[] {
            125,
            0,
            0,
            131072});
            this.NailY_numericUpDown.Minimum = new decimal(new int[] {
            125,
            0,
            0,
            -2147352576});
            this.NailY_numericUpDown.Name = "NailY_numericUpDown";
            this.NailY_numericUpDown.Size = new System.Drawing.Size(48, 20);
            this.NailY_numericUpDown.TabIndex = 506;
            // 
            // NailM_numericUpDown
            // 
            this.NailM_numericUpDown.DecimalPlaces = 2;
            this.NailM_numericUpDown.Enabled = false;
            this.NailM_numericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.NailM_numericUpDown.Location = new System.Drawing.Point(86, 62);
            this.NailM_numericUpDown.Maximum = new decimal(new int[] {
            125,
            0,
            0,
            131072});
            this.NailM_numericUpDown.Minimum = new decimal(new int[] {
            125,
            0,
            0,
            -2147352576});
            this.NailM_numericUpDown.Name = "NailM_numericUpDown";
            this.NailM_numericUpDown.Size = new System.Drawing.Size(48, 20);
            this.NailM_numericUpDown.TabIndex = 507;
            // 
            // NailC_numericUpDown
            // 
            this.NailC_numericUpDown.DecimalPlaces = 2;
            this.NailC_numericUpDown.Enabled = false;
            this.NailC_numericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.NailC_numericUpDown.Location = new System.Drawing.Point(86, 91);
            this.NailC_numericUpDown.Maximum = new decimal(new int[] {
            125,
            0,
            0,
            131072});
            this.NailC_numericUpDown.Minimum = new decimal(new int[] {
            125,
            0,
            0,
            -2147352576});
            this.NailC_numericUpDown.Name = "NailC_numericUpDown";
            this.NailC_numericUpDown.Size = new System.Drawing.Size(48, 20);
            this.NailC_numericUpDown.TabIndex = 508;
            // 
            // PhotoM_numericUpDown
            // 
            this.PhotoM_numericUpDown.DecimalPlaces = 2;
            this.PhotoM_numericUpDown.Enabled = false;
            this.PhotoM_numericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.PhotoM_numericUpDown.Location = new System.Drawing.Point(65, 29);
            this.PhotoM_numericUpDown.Maximum = new decimal(new int[] {
            125,
            0,
            0,
            131072});
            this.PhotoM_numericUpDown.Minimum = new decimal(new int[] {
            125,
            0,
            0,
            -2147352576});
            this.PhotoM_numericUpDown.Name = "PhotoM_numericUpDown";
            this.PhotoM_numericUpDown.Size = new System.Drawing.Size(48, 20);
            this.PhotoM_numericUpDown.TabIndex = 509;
            // 
            // PhotoC_numericUpDown
            // 
            this.PhotoC_numericUpDown.DecimalPlaces = 2;
            this.PhotoC_numericUpDown.Enabled = false;
            this.PhotoC_numericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.PhotoC_numericUpDown.Location = new System.Drawing.Point(65, 58);
            this.PhotoC_numericUpDown.Maximum = new decimal(new int[] {
            125,
            0,
            0,
            131072});
            this.PhotoC_numericUpDown.Minimum = new decimal(new int[] {
            125,
            0,
            0,
            -2147352576});
            this.PhotoC_numericUpDown.Name = "PhotoC_numericUpDown";
            this.PhotoC_numericUpDown.Size = new System.Drawing.Size(48, 20);
            this.PhotoC_numericUpDown.TabIndex = 510;
            // 
            // PrintNail_button
            // 
            this.PrintNail_button.Enabled = false;
            this.PrintNail_button.Location = new System.Drawing.Point(145, 32);
            this.PrintNail_button.Name = "PrintNail_button";
            this.PrintNail_button.Size = new System.Drawing.Size(110, 42);
            this.PrintNail_button.TabIndex = 511;
            this.PrintNail_button.Text = "Print Nail";
            this.PrintNail_button.UseVisualStyleBackColor = true;
            this.PrintNail_button.Click += new System.EventHandler(this.PrintNail_button_Click);
            // 
            // PrintPhoto_button
            // 
            this.PrintPhoto_button.Enabled = false;
            this.PrintPhoto_button.Location = new System.Drawing.Point(123, 31);
            this.PrintPhoto_button.Name = "PrintPhoto_button";
            this.PrintPhoto_button.Size = new System.Drawing.Size(126, 44);
            this.PrintPhoto_button.TabIndex = 512;
            this.PrintPhoto_button.Text = "Print Photo";
            this.PrintPhoto_button.UseVisualStyleBackColor = true;
            this.PrintPhoto_button.Click += new System.EventHandler(this.PrintPhoto_button_Click);
            // 
            // Nail_Regi_write_button
            // 
            this.Nail_Regi_write_button.Enabled = false;
            this.Nail_Regi_write_button.Location = new System.Drawing.Point(0, 148);
            this.Nail_Regi_write_button.Name = "Nail_Regi_write_button";
            this.Nail_Regi_write_button.Size = new System.Drawing.Size(261, 47);
            this.Nail_Regi_write_button.TabIndex = 513;
            this.Nail_Regi_write_button.Text = "Write Nail Regi Data";
            this.Nail_Regi_write_button.UseVisualStyleBackColor = true;
            this.Nail_Regi_write_button.Click += new System.EventHandler(this.Nail_Regi_write_button_Click);
            // 
            // Photo_Regi_write_button
            // 
            this.Photo_Regi_write_button.Enabled = false;
            this.Photo_Regi_write_button.Location = new System.Drawing.Point(0, 148);
            this.Photo_Regi_write_button.Name = "Photo_Regi_write_button";
            this.Photo_Regi_write_button.Size = new System.Drawing.Size(257, 47);
            this.Photo_Regi_write_button.TabIndex = 514;
            this.Photo_Regi_write_button.Text = "Write Photo Regi Data";
            this.Photo_Regi_write_button.UseVisualStyleBackColor = true;
            this.Photo_Regi_write_button.Click += new System.EventHandler(this.Photo_Regi_write_button_Click);
            // 
            // PrinterSN_textBox
            // 
            this.PrinterSN_textBox.Location = new System.Drawing.Point(113, 133);
            this.PrinterSN_textBox.MaxLength = 18;
            this.PrinterSN_textBox.Name = "PrinterSN_textBox";
            this.PrinterSN_textBox.Size = new System.Drawing.Size(432, 20);
            this.PrinterSN_textBox.TabIndex = 515;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(14, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 516;
            this.label1.Text = "White";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(14, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 517;
            this.label2.Text = "Yellow";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Magenta;
            this.label3.Location = new System.Drawing.Point(14, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 518;
            this.label3.Text = "Magenta";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Cyan;
            this.label4.Location = new System.Drawing.Point(14, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 519;
            this.label4.Text = "Cyan";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Cyan;
            this.label5.Location = new System.Drawing.Point(5, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 522;
            this.label5.Text = "Cyan";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Magenta;
            this.label6.Location = new System.Drawing.Point(5, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 521;
            this.label6.Text = "Magenta";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Yellow;
            this.label7.Location = new System.Drawing.Point(5, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 520;
            this.label7.Text = "Yellow";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(29, 136);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 13);
            this.label8.TabIndex = 523;
            this.label8.Text = "Printer Serial #";
            // 
            // BluetoothStatus_groupBox
            // 
            this.BluetoothStatus_groupBox.Controls.Add(this.BluetoothStatus_label);
            this.BluetoothStatus_groupBox.Location = new System.Drawing.Point(322, 14);
            this.BluetoothStatus_groupBox.Name = "BluetoothStatus_groupBox";
            this.BluetoothStatus_groupBox.Size = new System.Drawing.Size(109, 109);
            this.BluetoothStatus_groupBox.TabIndex = 524;
            this.BluetoothStatus_groupBox.TabStop = false;
            this.BluetoothStatus_groupBox.Text = "BLuetooth Status";
            // 
            // BluetoothStatus_label
            // 
            this.BluetoothStatus_label.AutoSize = true;
            this.BluetoothStatus_label.Font = new System.Drawing.Font("Gulim", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BluetoothStatus_label.Location = new System.Drawing.Point(27, 44);
            this.BluetoothStatus_label.Name = "BluetoothStatus_label";
            this.BluetoothStatus_label.Size = new System.Drawing.Size(63, 27);
            this.BluetoothStatus_label.TabIndex = 0;
            this.BluetoothStatus_label.Text = "---";
            // 
            // Setting_groupBox
            // 
            this.Setting_groupBox.Controls.Add(this.cbx_Photo);
            this.Setting_groupBox.Controls.Add(this.cbx_Nail);
            this.Setting_groupBox.Location = new System.Drawing.Point(437, 14);
            this.Setting_groupBox.Name = "Setting_groupBox";
            this.Setting_groupBox.Size = new System.Drawing.Size(109, 109);
            this.Setting_groupBox.TabIndex = 7;
            this.Setting_groupBox.TabStop = false;
            this.Setting_groupBox.Text = "Settings";
            // 
            // cbx_Photo
            // 
            this.cbx_Photo.AutoSize = true;
            this.cbx_Photo.Location = new System.Drawing.Point(13, 54);
            this.cbx_Photo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbx_Photo.Name = "cbx_Photo";
            this.cbx_Photo.Size = new System.Drawing.Size(54, 17);
            this.cbx_Photo.TabIndex = 1;
            this.cbx_Photo.Text = "Photo";
            this.cbx_Photo.UseVisualStyleBackColor = true;
            this.cbx_Photo.CheckedChanged += new System.EventHandler(this.cbx_Photo_CheckedChanged);
            // 
            // cbx_Nail
            // 
            this.cbx_Nail.AutoSize = true;
            this.cbx_Nail.Location = new System.Drawing.Point(13, 24);
            this.cbx_Nail.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbx_Nail.Name = "cbx_Nail";
            this.cbx_Nail.Size = new System.Drawing.Size(44, 17);
            this.cbx_Nail.TabIndex = 0;
            this.cbx_Nail.Text = "Nail";
            this.cbx_Nail.UseVisualStyleBackColor = true;
            this.cbx_Nail.CheckedChanged += new System.EventHandler(this.Nail_CheckedChanged);
            // 
            // gb_Nail
            // 
            this.gb_Nail.Controls.Add(this.BTPrintNail_button);
            this.gb_Nail.Controls.Add(this.label3);
            this.gb_Nail.Controls.Add(this.NailY_numericUpDown);
            this.gb_Nail.Controls.Add(this.NailM_numericUpDown);
            this.gb_Nail.Controls.Add(this.NailC_numericUpDown);
            this.gb_Nail.Controls.Add(this.PrintNail_button);
            this.gb_Nail.Controls.Add(this.label1);
            this.gb_Nail.Controls.Add(this.Nail_Regi_write_button);
            this.gb_Nail.Controls.Add(this.label2);
            this.gb_Nail.Controls.Add(this.label4);
            this.gb_Nail.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.gb_Nail.Location = new System.Drawing.Point(13, 161);
            this.gb_Nail.Name = "gb_Nail";
            this.gb_Nail.Size = new System.Drawing.Size(261, 196);
            this.gb_Nail.TabIndex = 525;
            this.gb_Nail.TabStop = false;
            this.gb_Nail.Text = "Nail";
            // 
            // BTPrintNail_button
            // 
            this.BTPrintNail_button.Enabled = false;
            this.BTPrintNail_button.Location = new System.Drawing.Point(145, 82);
            this.BTPrintNail_button.Name = "BTPrintNail_button";
            this.BTPrintNail_button.Size = new System.Drawing.Size(110, 37);
            this.BTPrintNail_button.TabIndex = 520;
            this.BTPrintNail_button.Text = "BT Print Nail";
            this.BTPrintNail_button.UseVisualStyleBackColor = true;
            this.BTPrintNail_button.Click += new System.EventHandler(this.BTPrintNail_button_Click);
            // 
            // Photo_groupbox
            // 
            this.Photo_groupbox.Controls.Add(this.BTPrintPhoto_button);
            this.Photo_groupbox.Controls.Add(this.label7);
            this.Photo_groupbox.Controls.Add(this.PhotoM_numericUpDown);
            this.Photo_groupbox.Controls.Add(this.PhotoC_numericUpDown);
            this.Photo_groupbox.Controls.Add(this.PrintPhoto_button);
            this.Photo_groupbox.Controls.Add(this.label5);
            this.Photo_groupbox.Controls.Add(this.Photo_Regi_write_button);
            this.Photo_groupbox.Controls.Add(this.label6);
            this.Photo_groupbox.Location = new System.Drawing.Point(289, 161);
            this.Photo_groupbox.Name = "Photo_groupbox";
            this.Photo_groupbox.Size = new System.Drawing.Size(257, 196);
            this.Photo_groupbox.TabIndex = 8;
            this.Photo_groupbox.TabStop = false;
            this.Photo_groupbox.Text = "Photo";
            // 
            // BTPrintPhoto_button
            // 
            this.BTPrintPhoto_button.Enabled = false;
            this.BTPrintPhoto_button.Location = new System.Drawing.Point(123, 82);
            this.BTPrintPhoto_button.Name = "BTPrintPhoto_button";
            this.BTPrintPhoto_button.Size = new System.Drawing.Size(126, 37);
            this.BTPrintPhoto_button.TabIndex = 521;
            this.BTPrintPhoto_button.Text = "BT Print Photo";
            this.BTPrintPhoto_button.UseVisualStyleBackColor = true;
            this.BTPrintPhoto_button.Click += new System.EventHandler(this.BTPrintPhoto_button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 395);
            this.Controls.Add(this.Photo_groupbox);
            this.Controls.Add(this.gb_Nail);
            this.Controls.Add(this.Setting_groupBox);
            this.Controls.Add(this.BluetoothStatus_groupBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.PrinterSN_textBox);
            this.Controls.Add(this.DBGMon_groupBox);
            this.Controls.Add(this.DeviceStatus_groupBox);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "DMP Regi Adjustment Program v0.1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.DeviceStatus_groupBox.ResumeLayout(false);
            this.DeviceStatus_groupBox.PerformLayout();
            this.DBGMon_groupBox.ResumeLayout(false);
            this.DBGMon_groupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NailY_numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NailM_numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NailC_numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhotoM_numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhotoC_numericUpDown)).EndInit();
            this.BluetoothStatus_groupBox.ResumeLayout(false);
            this.BluetoothStatus_groupBox.PerformLayout();
            this.Setting_groupBox.ResumeLayout(false);
            this.Setting_groupBox.PerformLayout();
            this.gb_Nail.ResumeLayout(false);
            this.gb_Nail.PerformLayout();
            this.Photo_groupbox.ResumeLayout(false);
            this.Photo_groupbox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.GroupBox DeviceStatus_groupBox;
        private System.Windows.Forms.Label DeviceStatus_label;
        private System.Windows.Forms.GroupBox DBGMon_groupBox;
        private System.Windows.Forms.Label DBGMonStatus_label;
        private System.Windows.Forms.NumericUpDown NailY_numericUpDown;
        private System.Windows.Forms.NumericUpDown NailM_numericUpDown;
        private System.Windows.Forms.NumericUpDown NailC_numericUpDown;
        private System.Windows.Forms.NumericUpDown PhotoM_numericUpDown;
        private System.Windows.Forms.NumericUpDown PhotoC_numericUpDown;
        private System.Windows.Forms.Button PrintNail_button;
        private System.Windows.Forms.Button PrintPhoto_button;
        private System.Windows.Forms.Button Nail_Regi_write_button;
        private System.Windows.Forms.Button Photo_Regi_write_button;
        private System.Windows.Forms.TextBox PrinterSN_textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox BluetoothStatus_groupBox;
        private System.Windows.Forms.Label BluetoothStatus_label;
        private System.Windows.Forms.GroupBox Setting_groupBox;
        private System.Windows.Forms.GroupBox gb_Nail;
        private System.Windows.Forms.GroupBox Photo_groupbox;
        private System.Windows.Forms.CheckBox cbx_Photo;
        private System.Windows.Forms.CheckBox cbx_Nail;
        private System.Windows.Forms.Button BTPrintNail_button;
        private System.Windows.Forms.Button BTPrintPhoto_button;
    }
}

