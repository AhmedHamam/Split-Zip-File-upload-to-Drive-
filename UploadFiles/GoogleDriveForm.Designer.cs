namespace UploadFiles
{
    partial class GoogleDriveForm
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
            LblUploadSpeed = new Label();
            label6 = new Label();
            label5 = new Label();
            PrgUpload = new ProgressBar();
            BtnUpload = new Button();
            button1 = new Button();
            button2 = new Button();
            CBListOfFolders = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            CBListOfFiles = new ComboBox();
            BtnSelectSourcePath = new Button();
            label3 = new Label();
            TxtDireoctoryPath = new TextBox();
            SuspendLayout();
            // 
            // LblUploadSpeed
            // 
            LblUploadSpeed.AutoSize = true;
            LblUploadSpeed.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            LblUploadSpeed.ForeColor = Color.Red;
            LblUploadSpeed.Location = new Point(161, 216);
            LblUploadSpeed.Name = "LblUploadSpeed";
            LblUploadSpeed.Size = new Size(117, 21);
            LblUploadSpeed.TabIndex = 26;
            LblUploadSpeed.Text = "Upload Speed";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ForeColor = Color.Red;
            label6.Location = new Point(6, 216);
            label6.Name = "label6";
            label6.Size = new Size(117, 21);
            label6.TabIndex = 25;
            label6.Text = "Upload Speed";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ForeColor = Color.Red;
            label5.Location = new Point(6, 178);
            label5.Name = "label5";
            label5.Size = new Size(128, 21);
            label5.TabIndex = 24;
            label5.Text = "Upload Precent";
            // 
            // PrgUpload
            // 
            PrgUpload.Location = new Point(161, 176);
            PrgUpload.Name = "PrgUpload";
            PrgUpload.Size = new Size(427, 23);
            PrgUpload.TabIndex = 23;
            // 
            // BtnUpload
            // 
            BtnUpload.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            BtnUpload.Location = new Point(9, 246);
            BtnUpload.Name = "BtnUpload";
            BtnUpload.Size = new Size(239, 64);
            BtnUpload.TabIndex = 27;
            BtnUpload.Text = "Upload";
            BtnUpload.UseVisualStyleBackColor = true;
            BtnUpload.Click += BtnUpload_Click;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            button1.Location = new Point(282, 246);
            button1.Name = "button1";
            button1.Size = new Size(239, 64);
            button1.TabIndex = 28;
            button1.Text = "Download";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            button2.Location = new Point(527, 246);
            button2.Name = "button2";
            button2.Size = new Size(239, 64);
            button2.TabIndex = 29;
            button2.Text = "Delete";
            button2.UseVisualStyleBackColor = true;
            // 
            // CBListOfFolders
            // 
            CBListOfFolders.FormattingEnabled = true;
            CBListOfFolders.Location = new Point(150, 11);
            CBListOfFolders.Name = "CBListOfFolders";
            CBListOfFolders.Size = new Size(438, 23);
            CBListOfFolders.TabIndex = 30;
            CBListOfFolders.SelectedIndexChanged += CBListOfFolders_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.Red;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(113, 21);
            label1.TabIndex = 31;
            label1.Text = "List of folders";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.Red;
            label2.Location = new Point(12, 71);
            label2.Name = "label2";
            label2.Size = new Size(94, 21);
            label2.TabIndex = 32;
            label2.Text = "List of Files";
            // 
            // CBListOfFiles
            // 
            CBListOfFiles.FormattingEnabled = true;
            CBListOfFiles.Location = new Point(150, 70);
            CBListOfFiles.Name = "CBListOfFiles";
            CBListOfFiles.Size = new Size(438, 23);
            CBListOfFiles.TabIndex = 33;
            // 
            // BtnSelectSourcePath
            // 
            BtnSelectSourcePath.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnSelectSourcePath.Location = new Point(671, 117);
            BtnSelectSourcePath.Name = "BtnSelectSourcePath";
            BtnSelectSourcePath.Size = new Size(119, 29);
            BtnSelectSourcePath.TabIndex = 36;
            BtnSelectSourcePath.Text = "Open";
            BtnSelectSourcePath.UseVisualStyleBackColor = true;
            BtnSelectSourcePath.Click += BtnSelectSourcePath_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.Red;
            label3.Location = new Point(6, 122);
            label3.Name = "label3";
            label3.Size = new Size(197, 21);
            label3.TabIndex = 35;
            label3.Text = "Directory of Source Path";
            // 
            // TxtDireoctoryPath
            // 
            TxtDireoctoryPath.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TxtDireoctoryPath.Location = new Point(211, 118);
            TxtDireoctoryPath.Name = "TxtDireoctoryPath";
            TxtDireoctoryPath.ReadOnly = true;
            TxtDireoctoryPath.Size = new Size(427, 29);
            TxtDireoctoryPath.TabIndex = 34;
            // 
            // GoogleDriveForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(789, 327);
            Controls.Add(BtnSelectSourcePath);
            Controls.Add(label3);
            Controls.Add(TxtDireoctoryPath);
            Controls.Add(CBListOfFiles);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(CBListOfFolders);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(BtnUpload);
            Controls.Add(LblUploadSpeed);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(PrgUpload);
            Name = "GoogleDriveForm";
            Text = "GoogleDriveForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LblUploadSpeed;
        private Label label6;
        private Label label5;
        private ProgressBar PrgUpload;
        private Button BtnUpload;
        private Button button1;
        private Button button2;
        private ComboBox CBListOfFolders;
        private Label label1;
        private Label label2;
        private ComboBox CBListOfFiles;
        private Button BtnSelectSourcePath;
        private Label label3;
        private TextBox TxtDireoctoryPath;
    }
}