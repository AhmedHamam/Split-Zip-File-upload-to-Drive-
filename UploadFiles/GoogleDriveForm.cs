namespace UploadFiles
{
    public partial class GoogleDriveForm : Form
    {
        private readonly GoogleDrive googleDrive;
        public GoogleDriveForm()
        {
            InitializeComponent();
        }
        //constructor to get google drive object
        public GoogleDriveForm(GoogleDrive googleDrive)
        {
            InitializeComponent();
            this.googleDrive = googleDrive;
            var folders = googleDrive.GetFolders();
            CBListOfFolders.SelectedIndex = -1;
            CBListOfFolders.DataSource = folders;
            CBListOfFolders.ValueMember = "Id";
            CBListOfFolders.DisplayMember = "Name";
            CBListOfFolders.SelectedIndex = -1;

        }
        //get all files name from google drive

        //method to calculate calculate upload speed
        public void CalculateUploadSpeed(long bytesUploaded, long totalBytes, TimeSpan timeSpan)
        {

            double uploadSpeed = bytesUploaded / timeSpan.TotalSeconds;
            string uploadSpeedText = string.Format("{0} kb/s", (uploadSpeed / 1024).ToString("0.00"));
            LblUploadSpeed.Text = uploadSpeedText;
        }

        //change progress bar value with percentage
        public void ChangeProgressBarValue(long bytesUploaded, long totalBytes)
        {
            double percentage = (double)bytesUploaded / totalBytes * 100;
            PrgUpload.Value = int.Parse(Math.Truncate(percentage).ToString());
        }

        private void CBListOfFolders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBListOfFolders.SelectedIndex == -1)
            {
                var files = googleDrive.GetFilesFromGoogleDrive();
                CBListOfFiles.DataSource = files;
                CBListOfFiles.ValueMember = "Id";
                CBListOfFiles.DisplayMember = "Name";
                CBListOfFiles.SelectedIndex = -1;
                return;
            }
            Google.Apis.Drive.v3.Data.File parent = (Google.Apis.Drive.v3.Data.File)CBListOfFolders.SelectedItem;
            var files2 = googleDrive.GetFilesFromGoogleDrive(parent.Id)?.ToList();

            CBListOfFiles.DataSource = files2;
            CBListOfFiles.ValueMember = "Id";
            CBListOfFiles.DisplayMember = "Name";
            CBListOfFiles.SelectedIndex = -1;
        }

        private void BtnUpload_Click(object sender, EventArgs e)
        {
            if (TxtDireoctoryPath.Text == "")
            {
                MessageBox.Show("Please Select File");
                return;
            }

            if (CBListOfFolders.SelectedIndex > -1)
                googleDrive.Upload(TxtDireoctoryPath.Text, CBListOfFolders.SelectedValue.ToString());
            else
                googleDrive.Upload(TxtDireoctoryPath.Text, null);

            //get upoad file speed
        }

        private void BtnSelectSourcePath_Click(object sender, EventArgs e)
        {
            TxtDireoctoryPath.Text = GetPath.GetFilePath("Get File Path", null, "Please Selct one file");
        }
    }
}
