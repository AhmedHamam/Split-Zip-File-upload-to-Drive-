namespace UploadFiles
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void BtnUploadToDrive_Click(object sender, EventArgs e)
        {
            string client_sec = TxtCredential.Text;
            string app_name = "UploadFilesToDrive";
            
            if (!string.IsNullOrEmpty(client_sec) )
            {
                GoogleDrive googleDrive = new(client_sec, app_name);
                GoogleDriveForm googleDriveForm = new GoogleDriveForm(googleDrive);
                googleDriveForm.Show();
            }
            else
            {
                MessageBox.Show("Please Select Client Secret Json File");
                return;
            }

        }
        private string? GetCredentialPath()
        {
            string filter = "Json Files|*.json";
            string title = "Select Client Secret Json File";
            string message = "Please Select Client Secret Json File";
            return GetPath.GetFilePath(title, filter, message);
        }


        private void BtnGetCredential_Click(object sender, EventArgs e)
        {
            TxtCredential.Text = GetCredentialPath();
        }
    }
}