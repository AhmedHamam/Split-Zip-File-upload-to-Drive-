using System;
using System.IO;
using System.Net;
namespace SplitZipFile
{
    public class FTPUploader
    {
        public static void UploadDirectory(string sourceDirectory, string targetDirectory, string host, string username, string password)
        {
            // Create a WebClient with credentials.
            WebClient client = new WebClient();
            client.Credentials = new NetworkCredential(username, password);

            // Get a list of all files and directories in the source directory and its subdirectories.
            string[] files = Directory.GetFiles(sourceDirectory, "*", SearchOption.AllDirectories);

            // Loop through each file and upload it to the FTP server.
            foreach (string file in files)
            {
                // Get the relative path of the file.
                string relativePath = file.Substring(sourceDirectory.Length + 1).Replace("\\", "/");

                // Combine the target directory and relative path to get the full target path.
                string targetPath = targetDirectory + "/" + relativePath;

                // Create the directory on the FTP server if it doesn't already exist.
                string directory = Path.GetDirectoryName(targetPath);
                if (!string.IsNullOrEmpty(directory))
                {
                    client.UploadData(host + "/" + directory, WebRequestMethods.Ftp.MakeDirectory, new byte[] { });
                }

                // Upload the file to the FTP server.
                client.UploadFile(host + "/" + targetPath, WebRequestMethods.Ftp.UploadFile, file);
            }
        }
    }

}
