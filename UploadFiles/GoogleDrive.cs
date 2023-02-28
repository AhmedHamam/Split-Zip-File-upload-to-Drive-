using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace UploadFiles
{
    public class GoogleDrive
    {
        private readonly string _clientSecretJsonPath;
        private readonly string _applicationName;
        private readonly string _folderId;
        private readonly string _fileName;
        private readonly string _filePath;

        public GoogleDrive(string clientSecretJsonPath, string applicationName, string folderId, string fileName, string filePath)
        {
            _clientSecretJsonPath = clientSecretJsonPath;
            _applicationName = applicationName;
            _folderId = folderId;
            _fileName = fileName;
            _filePath = filePath;
        }
        #region Get Credential
        private UserCredential GetCredential()
        {
            string[] Scopes = { DriveService.Scope.Drive };
            //get user credential  from client secret json file
            var secrets = GetClientSecrets(_clientSecretJsonPath);
            string credPath = GetCredentialPath();

            return GoogleWebAuthorizationBroker.AuthorizeAsync(
                                           secrets,
                                           Scopes,
                                           "user",
                                           CancellationToken.None,
                                           new FileDataStore(credPath, true)).Result;
        }

        private ClientSecrets GetClientSecrets(string path)
        {
            FileStream stream = new(path, FileMode.Open, FileAccess.Read);
            return GoogleClientSecrets.FromStream(stream).Secrets;
        }

        private string GetCredentialPath()
        {
            string credPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(credPath, ".credentials/drive-dotnet-quickstart.json");
        }
        #endregion Get Credential

        #region Upload 

        #endregion Upload

        #region Download

        #endregion Download

        #region Delete

        #endregion Delete



        #region Folders

        /// <summary>
        /// get list of folders under parent folder
        /// </summary>
        /// <param name="parentId">pa</param>
        /// <returns></returns>
        public IEnumerable<Google.Apis.Drive.v3.Data.File>? GetFolders(string parentId)
        {
            var service = CreateDriveService();
            var request = service.Files.List();
            request.Q = $"'{parentId}' in parents";
            request.Fields = "nextPageToken, files(id, name)";
            var result = request.Execute();
            var folders = result.Files;

            return folders;
        }

        /// <summary>
        /// Get List Of folders from google drive root folder
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Google.Apis.Drive.v3.Data.File>? GetFolders()
        {
            var service = CreateDriveService();
            var request = service.Files.List();
            request.Q = "mimeType='application/vnd.google-apps.folder'";
            request.Fields = "nextPageToken, files(id, name)";
            var result = request.Execute();
            var folders = result.Files;
            return folders;
        }

        /// <summary>
        /// create folder in google drive in root folder
        /// </summary>
        /// <param name="folderName">Folder Name </param>
        /// <returns></returns>
        public Google.Apis.Drive.v3.Data.File CreateFolder(string folderName)
        {
            var service = CreateDriveService();
            var fileMetadata = new Google.Apis.Drive.v3.Data.File();
            fileMetadata.Name = folderName;
            fileMetadata.MimeType = "application/vnd.google-apps.folder";
            var request = service.Files.Create(fileMetadata);
            request.Fields = "id";
            var file = request.Execute();
            return file;
        }

        /// <summary>
        /// create folder in google drive under parent folder
        /// </summary>
        /// <param name="folderName"> Folder Name </param>
        /// <param name="parentFolderId">Id of Parent Folder </param>
        /// <returns> Google Drive Folder</returns>
        public Google.Apis.Drive.v3.Data.File CreateFolder(string folderName, string parentFolderId)
        {
            var service = CreateDriveService();
            var fileMetadata = new Google.Apis.Drive.v3.Data.File();
            fileMetadata.Name = folderName;
            fileMetadata.MimeType = "application/vnd.google-apps.folder";
            fileMetadata.Parents = new List<string>() { parentFolderId };
            var request = service.Files.Create(fileMetadata);
            request.Fields = "id";
            var file = request.Execute();
            return file;
        }

        /// <summary>
        /// create folder in google drive under parent folders
        /// </summary>
        /// <param name="folderName">Folder Name </param>
        /// <param name="parentFolderIds">List Of Id for Parents folders </param>
        /// <returns> Google Drive Folder </returns>
        public Google.Apis.Drive.v3.Data.File CreateFolder(string folderName, List<string> parentFolderIds)
        {
            var service = CreateDriveService();
            var fileMetadata = new Google.Apis.Drive.v3.Data.File();
            fileMetadata.Name = folderName;
            fileMetadata.MimeType = "application/vnd.google-apps.folder";
            fileMetadata.Parents = parentFolderIds;
            var request = service.Files.Create(fileMetadata);
            request.Fields = "id";
            var file = request.Execute();
            return file;
        }

        #endregion Folders

        #region Files
        #endregion Files








        public void Upload()
        {
            var service = CreateDriveService();
            var fileMetadata = CreateFileMetadata();
            var file = UploadFile(service, fileMetadata);
            MessageBox.Show("File ID: " + file.Id);
        }

        private DriveService CreateDriveService()
        {
            var credential = GetCredential();
            return new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = _applicationName,
            });
        }

        private Google.Apis.Drive.v3.Data.File CreateFileMetadata()
        {
            var folder = GetFolders(_folderId);
            var file = new Google.Apis.Drive.v3.Data.File();
            file.Name = _fileName;
            if (folder != null)
            {
                file.Parents = new List<string>() { _folderId };
            }
            return file;
        }


        //get list of files from google drive folder
        public IEnumerable<Google.Apis.Drive.v3.Data.File>? GetFiles(string folderId)
        {
            var service = CreateDriveService();
            var request = service.Files.List();
            request.Q = $"'{folderId}' in parents";
            request.Fields = "nextPageToken, files(id, name)";
            var result = request.Execute();
            var files = result.Files;
            return files;
        }




        private Google.Apis.Drive.v3.Data.File UploadFile(DriveService service, Google.Apis.Drive.v3.Data.File fileMetadata)
        {
            FilesResource.CreateMediaUpload request;
            using (var stream = new FileStream(_filePath, FileMode.Open))
            {
                request = service.Files.Create(
                                       fileMetadata, stream, "");
                request.Fields = "id";
                request.Upload();
            }

            return request.ResponseBody;
        }


    }
}
