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
            this.statusStrip.SuspendLayout();
            this.DeviceStatus_groupBox.SuspendLayout();
            this.DBGMon_groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NailY_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NailM_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NailC_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhotoM_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhotoC_numericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip.Location = new System.Drawing.Point(0, 342);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(654, 22);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(162, 17);
            this.toolStripStatusLabel1.Text = "Please re-plug the USB cable";
            // 
            // DeviceStatus_groupBox
            // 
            this.DeviceStatus_groupBox.Controls.Add(this.DeviceStatus_label);
            this.DeviceStatus_groupBox.Location = new System.Drawing.Point(15, 13);
            this.DeviceStatus_groupBox.Name = "DeviceStatus_groupBox";
            this.DeviceStatus_groupBox.Size = new System.Drawing.Size(316, 100);
            this.DeviceStatus_groupBox.TabIndex = 1;
            this.DeviceStatus_groupBox.TabStop = false;
            this.DeviceStatus_groupBox.Text = "Device Status";
            // 
            // DeviceStatus_label
            // 
            this.DeviceStatus_label.AutoSize = true;
            this.DeviceStatus_label.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DeviceStatus_label.Location = new System.Drawing.Point(31, 40);
            this.DeviceStatus_label.Name = "DeviceStatus_label";
            this.DeviceStatus_label.Size = new System.Drawing.Size(184, 27);
            this.DeviceStatus_label.TabIndex = 0;
            this.DeviceStatus_label.Text = "Disconnected";
            // 
            // DBGMon_groupBox
            // 
            this.DBGMon_groupBox.Controls.Add(this.DBGMonStatus_label);
            this.DBGMon_groupBox.Location = new System.Drawing.Point(337, 13);
            this.DBGMon_groupBox.Name = "DBGMon_groupBox";
            this.DBGMon_groupBox.Size = new System.Drawing.Size(300, 100);
            this.DBGMon_groupBox.TabIndex = 6;
            this.DBGMon_groupBox.TabStop = false;
            this.DBGMon_groupBox.Text = "DBGMon Status";
            // 
            // DBGMonStatus_label
            // 
            this.DBGMonStatus_label.AutoSize = true;
            this.DBGMonStatus_label.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DBGMonStatus_label.Location = new System.Drawing.Point(31, 40);
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
            this.NailY_numericUpDown.Location = new System.Drawing.Point(118, 172);
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
            this.NailY_numericUpDown.Size = new System.Drawing.Size(56, 21);
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
            this.NailM_numericUpDown.Location = new System.Drawing.Point(118, 199);
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
            this.NailM_numericUpDown.Size = new System.Drawing.Size(56, 21);
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
            this.NailC_numericUpDown.Location = new System.Drawing.Point(118, 226);
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
            this.NailC_numericUpDown.Size = new System.Drawing.Size(56, 21);
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
            this.PhotoM_numericUpDown.Location = new System.Drawing.Point(422, 171);
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
            this.PhotoM_numericUpDown.Size = new System.Drawing.Size(56, 21);
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
            this.PhotoC_numericUpDown.Location = new System.Drawing.Point(422, 198);
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
            this.PhotoC_numericUpDown.Size = new System.Drawing.Size(56, 21);
            this.PhotoC_numericUpDown.TabIndex = 510;
            // 
            // PrintNail_button
            // 
            this.PrintNail_button.Enabled = false;
            this.PrintNail_button.Location = new System.Drawing.Point(191, 160);
            this.PrintNail_button.Name = "PrintNail_button";
            this.PrintNail_button.Size = new System.Drawing.Size(128, 92);
            this.PrintNail_button.TabIndex = 511;
            this.PrintNail_button.Text = "Print Nail";
            this.PrintNail_button.UseVisualStyleBackColor = true;
            this.PrintNail_button.Click += new System.EventHandler(this.PrintNail_button_Click);
            // 
            // PrintPhoto_button
            // 
            this.PrintPhoto_button.Enabled = false;
            this.PrintPhoto_button.Location = new System.Drawing.Point(490, 160);
            this.PrintPhoto_button.Name = "PrintPhoto_button";
            this.PrintPhoto_button.Size = new System.Drawing.Size(147, 92);
            this.PrintPhoto_button.TabIndex = 512;
            this.PrintPhoto_button.Text = "Print Photo";
            this.PrintPhoto_button.UseVisualStyleBackColor = true;
            this.PrintPhoto_button.Click += new System.EventHandler(this.PrintPhoto_button_Click);
            // 
            // Nail_Regi_write_button
            // 
            this.Nail_Regi_write_button.Enabled = false;
            this.Nail_Regi_write_button.Location = new System.Drawing.Point(15, 285);
            this.Nail_Regi_write_button.Name = "Nail_Regi_write_button";
            this.Nail_Regi_write_button.Size = new System.Drawing.Size(304, 44);
            this.Nail_Regi_write_button.TabIndex = 513;
            this.Nail_Regi_write_button.Text = "Write Nail Regi Data";
            this.Nail_Regi_write_button.UseVisualStyleBackColor = true;
            this.Nail_Regi_write_button.Click += new System.EventHandler(this.Nail_Regi_write_button_Click);
            // 
            // Photo_Regi_write_button
            // 
            this.Photo_Regi_write_button.Enabled = false;
            this.Photo_Regi_write_button.Location = new System.Drawing.Point(337, 285);
            this.Photo_Regi_write_button.Name = "Photo_Regi_write_button";
            this.Photo_Regi_write_button.Size = new System.Drawing.Size(300, 44);
            this.Photo_Regi_write_button.TabIndex = 514;
            this.Photo_Regi_write_button.Text = "Write Photo Regi Data";
            this.Photo_Regi_write_button.UseVisualStyleBackColor = true;
            this.Photo_Regi_write_button.Click += new System.EventHandler(this.Photo_Regi_write_button_Click);
            // 
            // PrinterSN_textBox
            // 
            this.PrinterSN_textBox.Location = new System.Drawing.Point(132, 123);
            this.PrinterSN_textBox.MaxLength = 18;
            this.PrinterSN_textBox.Name = "PrinterSN_textBox";
            this.PrinterSN_textBox.Size = new System.Drawing.Size(503, 21);
            this.PrinterSN_textBox.TabIndex = 515;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(34, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 516;
            this.label1.Text = "White";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(34, 187);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 12);
            this.label2.TabIndex = 517;
            this.label2.Text = "Yellow";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Magenta;
            this.label3.Location = new System.Drawing.Point(34, 214);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 12);
            this.label3.TabIndex = 518;
            this.label3.Text = "Magenta";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Cyan;
            this.label4.Location = new System.Drawing.Point(34, 240);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 519;
            this.label4.Text = "Cyan";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Cyan;
            this.label5.Location = new System.Drawing.Point(352, 215);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 522;
            this.label5.Text = "Cyan";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Magenta;
            this.label6.Location = new System.Drawing.Point(352, 189);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 12);
            this.label6.TabIndex = 521;
            this.label6.Text = "Magenta";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Yellow;
            this.label7.Location = new System.Drawing.Point(352, 162);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 12);
            this.label7.TabIndex = 520;
            this.label7.Text = "Yellow";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(34, 126);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 12);
            this.label8.TabIndex = 523;
            this.label8.Text = "Printer Serial #";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 364);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PrinterSN_textBox);
            this.Controls.Add(this.Photo_Regi_write_button);
            this.Controls.Add(this.Nail_Regi_write_button);
            this.Controls.Add(this.PrintPhoto_button);
            this.Controls.Add(this.PrintNail_button);
            this.Controls.Add(this.PhotoC_numericUpDown);
            this.Controls.Add(this.PhotoM_numericUpDown);
            this.Controls.Add(this.NailC_numericUpDown);
            this.Controls.Add(this.NailM_numericUpDown);
            this.Controls.Add(this.NailY_numericUpDown);
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
    }
}

