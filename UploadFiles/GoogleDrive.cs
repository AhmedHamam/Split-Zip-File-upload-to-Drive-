using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO.Compression;

namespace UploadFiles
{
    public class GoogleDrive
    {
        private readonly string _clientSecretJsonPath;
        private readonly string _applicationName;
        private  string? _folderId;
        private readonly string _fileName;
        private  string _filePath;

        //private constructor
        private GoogleDrive()
        {
        }
        public GoogleDrive(string clientSecretJsonPath,string applicationName)
        {
            _clientSecretJsonPath = clientSecretJsonPath;
            _applicationName = applicationName;
        }
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
            return Path.Combine(credPath, ".credentials/drive-dotnet-quickstart2.json");
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
        /// create folder in google drive under parent folder with folder name
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
        /// create folder in google drive under parent folders with folder name
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

        // delete folder from google drive
        public void DeleteFolder(string folderId)
        {
            var service = CreateDriveService();
            service.Files.Delete(folderId).Execute();
        }
        //download folder from google drive
        public void DownloadFolder(string folderId)
        {
            var service = CreateDriveService();
            var request = service.Files.Get(folderId);
            var file = request.Execute();
            var folderName = file.Name;
            var folderPath = Path.Combine(_filePath, folderName);
            Directory.CreateDirectory(folderPath);
            var files = GetFiles(folderId);
            foreach (var item in files)
            {
                DownloadFile(item.Id, folderPath);
            }
            var folders = GetFolders(folderId);
            foreach (var item in folders)
            {
                DownloadFolder(item.Id);
            }
        }
        //download folder from google drive as zip file
        public void DownloadFolderAsZip(string folderId)
        {
            var service = CreateDriveService();
            var request = service.Files.Get(folderId);
            var file = request.Execute();
            var folderName = file.Name;
            var folderPath = Path.Combine(_filePath, folderName);
            Directory.CreateDirectory(folderPath);
            var files = GetFiles(folderId);
            foreach (var item in files)
            {
                DownloadFile(item.Id, folderPath);
            }
            var folders = GetFolders(folderId);
            foreach (var item in folders)
            {
                DownloadFolder(item.Id);
            }
            ZipFile.CreateFromDirectory(folderPath, folderPath + ".zip");
            Directory.Delete(folderPath, true);
        }
     


        private void DownloadFile(string id, string folderPath)
        {
           //downlod file from google drive
           var service = CreateDriveService();
            var request = service.Files.Get(id);
            var file = request.Execute();
            var fileName = file.Name;
            var filePath = Path.Combine(folderPath, fileName);
            using (var stream = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
            {
                request.MediaDownloader.ProgressChanged += (IDownloadProgress progress) =>
                {
                    switch (progress.Status)
                    {
                        case DownloadStatus.Downloading:
                            {
                                Console.WriteLine(progress.BytesDownloaded);
                                break;
                            }
                        case DownloadStatus.Completed:
                            {
                                Console.WriteLine("Download complete.");
                                break;
                            }
                        case DownloadStatus.Failed:
                            {
                                Console.WriteLine("Download failed.");
                                break;
                            }
                    }
                };
                request.Download(stream);
            }




        }
        #endregion Folders

        #region Files
        //change file permission
        public void ChangeFilePermission(string fileId, string email, string role)
        {
            var service = CreateDriveService();
            var newPermission = new Google.Apis.Drive.v3.Data.Permission();
            newPermission.Type = "user";
            newPermission.Role = role;
            newPermission.EmailAddress = email;
            var request = service.Permissions.Create(newPermission, fileId);
            request.Execute();
        }
        public void ChangeFilePermission(string fileId,string role)
        {
            var service = CreateDriveService();
            var newPermission = new Google.Apis.Drive.v3.Data.Permission();
            newPermission.Type = "anyone";
            newPermission.Role = role;
            var request = service.Permissions.Create(newPermission, fileId);
            request.Execute();
        }
       

      
        //allow list of users to access file
        public void ChangeFilePermission(string fileId, List<string> emails, string role)
        {
            var service = CreateDriveService();
            foreach (var email in emails)
            {
                var newPermission = new Google.Apis.Drive.v3.Data.Permission();
                newPermission.Type = "user";
                newPermission.Role = role;
                newPermission.EmailAddress = email;
                var request = service.Permissions.Create(newPermission, fileId);
                request.Execute();
            }
        }


        //get file from google drive
        public Google.Apis.Drive.v3.Data.File GetFile(string fileId)
        {
            var service = CreateDriveService();
            var request = service.Files.Get(fileId);
            var file = request.Execute();
            return file;
        }
        //get list of files under parent folder
        public IEnumerable<Google.Apis.Drive.v3.Data.File>? GetFilesFromGoogleDrive(string parentId)
        {
            var service = CreateDriveService();
            var request = service.Files.List();
            request.Q = $"'{parentId}' in parents";
            request.Fields = "nextPageToken, files(id, name)";
            var result = request.Execute();
            var files = result.Files;
            return files;
        }
        //get list of files from google drive root folder
        public IEnumerable<Google.Apis.Drive.v3.Data.File>? GetFilesFromGoogleDrive()
        {
            var service = CreateDriveService();
            var request = service.Files.List();
            request.Q = "mimeType!='application/vnd.google-apps.folder'";
            request.Fields = "nextPageToken, files(id, name)";
            var result = request.Execute();
            var files = result.Files;
            return files;
        }
        // create file in google drive in root folder
        public Google.Apis.Drive.v3.Data.File CreateFile(string fileName, string mimeType, string description, string folderId)
        {
            var service = CreateDriveService();
            var fileMetadata = new Google.Apis.Drive.v3.Data.File();
            fileMetadata.Name = fileName;
            fileMetadata.MimeType = mimeType;
            fileMetadata.Description = description;
            fileMetadata.Parents = new List<string>() { folderId };
            var request = service.Files.Create(fileMetadata);
            request.Fields = "id";
            var file = request.Execute();
            return file;
        }   
        // create file in google drive under parent folder
        public Google.Apis.Drive.v3.Data.File CreateFile(string fileName, string mimeType, string description, string folderId, string parentFolderId)
        {
            var service = CreateDriveService();
            var fileMetadata = new Google.Apis.Drive.v3.Data.File();
            fileMetadata.Name = fileName;
            fileMetadata.MimeType = mimeType;
            fileMetadata.Description = description;
            fileMetadata.Parents = new List<string>() { folderId, parentFolderId };
            var request = service.Files.Create(fileMetadata);
            request.Fields = "id";
            var file = request.Execute();
            return file;
        }

        //delete file from google drive
        public void DeleteFile(string fileId)
        {
            var service = CreateDriveService();
            var request = service.Files.Delete(fileId);
            request.Execute();
        }


        #endregion Files








        public void Upload()
        {
            try
            {
                var service = CreateDriveService();
                var fileMetadata = CreateFileMetadata();

                var file = UploadFile(service, fileMetadata);
                MessageBox.Show("File ID: " + file?.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
           
        }
        public void Upload(string filePath,string? folderId)
        {
            try
            {
                var service = CreateDriveService();
                _filePath= filePath;
                _folderId = folderId;
                var fileMetadata = CreateFileMetadata();
                var file = UploadFile(service, fileMetadata);
                MessageBox.Show("File ID: " + file?.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }


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
                //get uploaded bytes

                var uploadedBytes = request.ResponseBody.Size;
                //get total bytes
                var totalBytes = stream.Length;
                //get percentage
                var percentage = (uploadedBytes * 100) / totalBytes;
                Console.WriteLine(percentage);

                
            }

            return request.ResponseBody;
        }


    }
}
