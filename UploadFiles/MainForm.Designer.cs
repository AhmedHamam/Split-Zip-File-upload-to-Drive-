namespace UploadFiles
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            BtnUploadToDrive = new Button();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            textBox1 = new TextBox();
            label1 = new Label();
            NudSize = new NumericUpDown();
            CbSize = new ComboBox();
            BtnSelectSourcePath = new Button();
            label3 = new Label();
            BtnSelectOutPath = new Button();
            label2 = new Label();
            textBox2 = new TextBox();
            label4 = new Label();
            button2 = new Button();
            button3 = new Button();
            label8 = new Label();
            BtnGetCredential = new Button();
            TxtCredential = new TextBox();
            ((System.ComponentModel.ISupportInitialize)NudSize).BeginInit();
            SuspendLayout();
            // 
            // BtnUploadToDrive
            // 
            BtnUploadToDrive.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnUploadToDrive.Location = new Point(28, 497);
            BtnUploadToDrive.Name = "BtnUploadToDrive";
            BtnUploadToDrive.Size = new Size(203, 48);
            BtnUploadToDrive.TabIndex = 0;
            BtnUploadToDrive.Text = "Upload To Google Drive";
            BtnUploadToDrive.UseVisualStyleBackColor = true;
            BtnUploadToDrive.Click += BtnUploadToDrive_Click;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            radioButton1.Location = new Point(217, 212);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(148, 25);
            radioButton1.TabIndex = 3;
            radioButton1.TabStop = true;
            radioButton1.Text = "Use Temp OutPut";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            radioButton2.Location = new Point(217, 168);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(249, 25);
            radioButton2.TabIndex = 4;
            radioButton2.TabStop = true;
            radioButton2.Text = "Save Spliced File to Output path";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBox1.Location = new Point(217, 17);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(427, 29);
            textBox1.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.Red;
            label1.Location = new Point(12, 21);
            label1.Name = "label1";
            label1.Size = new Size(197, 21);
            label1.TabIndex = 7;
            label1.Text = "Directory of Source Path";
            // 
            // NudSize
            // 
            NudSize.Location = new Point(217, 108);
            NudSize.Name = "NudSize";
            NudSize.Size = new Size(117, 23);
            NudSize.TabIndex = 9;
            NudSize.TextAlign = HorizontalAlignment.Center;
            // 
            // CbSize
            // 
            CbSize.DropDownStyle = ComboBoxStyle.DropDownList;
            CbSize.FormattingEnabled = true;
            CbSize.ImeMode = ImeMode.NoControl;
            CbSize.Items.AddRange(new object[] { "KB", "MB", "GB" });
            CbSize.Location = new Point(347, 108);
            CbSize.Name = "CbSize";
            CbSize.Size = new Size(68, 23);
            CbSize.TabIndex = 10;
            // 
            // BtnSelectSourcePath
            // 
            BtnSelectSourcePath.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnSelectSourcePath.Location = new Point(677, 16);
            BtnSelectSourcePath.Name = "BtnSelectSourcePath";
            BtnSelectSourcePath.Size = new Size(119, 29);
            BtnSelectSourcePath.TabIndex = 11;
            BtnSelectSourcePath.Text = "Open";
            BtnSelectSourcePath.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.Red;
            label3.Location = new Point(12, 106);
            label3.Name = "label3";
            label3.Size = new Size(132, 21);
            label3.TabIndex = 12;
            label3.Text = "Spliced File Size";
            // 
            // BtnSelectOutPath
            // 
            BtnSelectOutPath.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnSelectOutPath.Location = new Point(677, 65);
            BtnSelectOutPath.Name = "BtnSelectOutPath";
            BtnSelectOutPath.Size = new Size(119, 29);
            BtnSelectOutPath.TabIndex = 15;
            BtnSelectOutPath.Text = "Open";
            BtnSelectOutPath.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.Red;
            label2.Location = new Point(12, 65);
            label2.Name = "label2";
            label2.Size = new Size(199, 21);
            label2.TabIndex = 14;
            label2.Text = "Directory of Output Path";
            // 
            // textBox2
            // 
            textBox2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBox2.Location = new Point(217, 61);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(427, 29);
            textBox2.TabIndex = 13;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = Color.Red;
            label4.Location = new Point(12, 187);
            label4.Name = "label4";
            label4.Size = new Size(63, 21);
            label4.TabIndex = 16;
            label4.Text = "Option";
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button2.Location = new Point(282, 497);
            button2.Name = "button2";
            button2.Size = new Size(203, 48);
            button2.TabIndex = 17;
            button2.Text = "Upload To Ftp Sever";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button3.Location = new Point(521, 497);
            button3.Name = "button3";
            button3.Size = new Size(203, 48);
            button3.TabIndex = 18;
            button3.Text = "Upload To Api";
            button3.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label8.ForeColor = Color.Red;
            label8.Location = new Point(12, 256);
            label8.Name = "label8";
            label8.Size = new Size(89, 21);
            label8.TabIndex = 23;
            label8.Text = "Credential";
            // 
            // BtnGetCredential
            // 
            BtnGetCredential.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnGetCredential.Location = new Point(677, 260);
            BtnGetCredential.Name = "BtnGetCredential";
            BtnGetCredential.Size = new Size(119, 29);
            BtnGetCredential.TabIndex = 25;
            BtnGetCredential.Text = "Open";
            BtnGetCredential.UseVisualStyleBackColor = true;
            BtnGetCredential.Click += BtnGetCredential_Click;
            // 
            // TxtCredential
            // 
            TxtCredential.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TxtCredential.Location = new Point(217, 256);
            TxtCredential.Name = "TxtCredential";
            TxtCredential.ReadOnly = true;
            TxtCredential.Size = new Size(427, 29);
            TxtCredential.TabIndex = 24;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 577);
            Controls.Add(BtnGetCredential);
            Controls.Add(TxtCredential);
            Controls.Add(label8);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(label4);
            Controls.Add(BtnSelectOutPath);
            Controls.Add(label2);
            Controls.Add(textBox2);
            Controls.Add(label3);
            Controls.Add(BtnSelectSourcePath);
            Controls.Add(CbSize);
            Controls.Add(NudSize);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(radioButton2);
            Controls.Add(radioButton1);
            Controls.Add(BtnUploadToDrive);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Uploder";
            ((System.ComponentModel.ISupportInitialize)NudSize).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnUploadToDrive;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private TextBox textBox1;
        private Label label1;
        private NumericUpDown NudSize;
        private ComboBox CbSize;
        private Button BtnSelectSourcePath;
        private Label label3;
        private Button BtnSelectOutPath;
        private Label label2;
        private TextBox textBox2;
        private Label label4;
        private Button button2;
        private Button button3;
        private Label label8;
        private Button BtnGetCredential;
        private TextBox TxtCredential;
    }
}