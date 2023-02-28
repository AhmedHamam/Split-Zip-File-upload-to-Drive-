namespace UploadFiles
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnUploadToDrive_Click(object sender, EventArgs e)
        {
            string client_sec = GetClientSecretPath();
            string app_name = "UploadFiles";
            string folderId = "171wYEamWhFGqwpwMap0LBquvyeMBcNzz";
            var file = GetUploadFilePath();
            string file_name = file.Split('\\').Last();
            string file_path = file;
            GoogleDrive googleDrive = new(client_sec, app_name, folderId, file_name, file_path);
            googleDrive.Upload();

        }
        private string GetClientSecretPath()
        {
            string filter = "Json Files|*.json";
            string title = "Select Client Secret Json File";
            string message = "Please Select Client Secret Json File";
            return GetFilePath(title, filter, message);
        }
        private string GetUploadFilePath()
        {
            string filter = "";
            string title = "Select  File";
            string message = "Please Select File";
            return GetFilePath(title, filter, message);
        }
        private string GetFilePath(string title,string filters,string message)
        {
            OpenFileDialog ofd=new OpenFileDialog();
            ofd.Filter=filters;
            ofd.Title=title;
            if(ofd.ShowDialog() == DialogResult.OK)
            { return ofd.FileName; }
            else
            {
                MessageBox.Show(message);
                return null;
            }
        }
    }
}