using SplitZipFile;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            //var sourceDirectory = @"D:\Test"; // Change this to the directory you want to split into zip files.
            //var outputDirectory = @"D:\ZipFiles"; // Change this to the directory where you want to save the zip files.
            //var maxZipFileSize = 50 * 1024 * 1024; // Change this to the maximum size (in bytes) of each zip file.
            //SplitDirectoryToZipFiles(sourceDirectory, outputDirectory, maxZipFileSize);
            //JoinSplitFiles(outputDirectory);

            //CreateSplitArchive(outputDirectory, sourceDirectory, "Test", 100000);
            //upload();

            string sourceDirectory = @"C:\\FTP";
            string targetDirectory = "/public_html";
            string host = "ftp://houseofarabic.com";
            string username = "u885723119.akeedtech.com";
            string password = "ASDasd@123";

            FTPUploader.UploadDirectory(sourceDirectory, targetDirectory, host, username, password);

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    static void JoinSplitFiles(string directoryPath)
    {
        var zipFilePaths = Directory.GetFiles(directoryPath, "*.zip");

        var outputDirectoryPath = Path.Combine(directoryPath, "output");
        Directory.CreateDirectory(outputDirectoryPath);

        var processedEntryNames = new HashSet<string>();

        foreach (var zipFilePath in zipFilePaths)
        {
            using (var zipStream = new ZipArchive(new FileStream(zipFilePath, FileMode.Open, FileAccess.Read)))
            {
                foreach (var entry in zipStream.Entries)
                {
                    var outputFilePath = Path.Combine(outputDirectoryPath, entry.Name);

                    if (processedEntryNames.Contains(entry.Name))
                    {
                        using (var outputExeStream = new FileStream(outputFilePath, FileMode.Append, FileAccess.Write))
                        {
                            foreach (var splitEntry in zipStream.Entries)
                            {
                                if (splitEntry.Name == entry.Name)
                                {
                                    using (var inputExeStream = splitEntry.Open())
                                    {
                                        inputExeStream.CopyTo(outputExeStream);
                                    }
                                }
                            }
                        }

                        continue;
                    }

                    processedEntryNames.Add(entry.Name);

                    using (var outputExeStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                    {
                        foreach (var splitEntry in zipStream.Entries)
                        {
                            if (splitEntry.Name == entry.Name)
                            {
                                using (var inputExeStream = splitEntry.Open())
                                {
                                    inputExeStream.CopyTo(outputExeStream);
                                }
                            }
                        }
                    }
                }
            }
        }

        Console.WriteLine("Join complete.");
    }
    static void SplitExeFile(string exeFilePath, string outputDirectory, long chunkSize, int numChunks)
    {
        using (var exeStream = new FileStream(exeFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            byte[] buffer = new byte[chunkSize];
            int bytesRead;
            int i = 0;

            for (; i < numChunks - 1; i++)
            {
                string outputFilePath = Path.Combine(outputDirectory, $"{i}.zip");
                using (var outputZipFile = ZipFile.Open(outputFilePath, ZipArchiveMode.Create))
                using (var outputZipStream = outputZipFile.CreateEntry(Path.GetFileName(exeFilePath), CompressionLevel.Optimal).Open())
                {
                    long bytesRemaining = chunkSize;
                    while (bytesRemaining > 0 && (bytesRead = exeStream.Read(buffer, 0, (int)Math.Min(bytesRemaining, buffer.Length))) > 0)
                    {
                        outputZipStream.Write(buffer, 0, bytesRead);
                        bytesRemaining -= bytesRead;
                    }
                }
            }

            // Last chunk may be smaller
            string lastOutputFilePath = Path.Combine(outputDirectory, $"{i}.zip");
            using (var lastOutputZipFile = ZipFile.Open(lastOutputFilePath, ZipArchiveMode.Create))
            using (var lastOutputZipStream = lastOutputZipFile.CreateEntry(Path.GetFileName(exeFilePath), CompressionLevel.Optimal).Open())
            {
                while ((bytesRead = exeStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    lastOutputZipStream.Write(buffer, 0, bytesRead);
                }
            }
        }
    }
    static void SplitDirectoryToZipFiles(string sourceDirectoryPath, string outputDirectoryPath, long maxZipFileSize)
    {
        if (!Directory.Exists(sourceDirectoryPath))
        {
            Console.WriteLine($"Error: Source directory does not exist: {sourceDirectoryPath}");
            return;
        }

        if (!Directory.Exists(outputDirectoryPath))
        {
            Directory.CreateDirectory(outputDirectoryPath);
        }

        // Get all files in the source directory and subdirectories.
        var files = Directory.GetFiles(sourceDirectoryPath, "*", SearchOption.AllDirectories);

        // Sort the files by size (descending) so we can add the largest files to the zip files first.
        var sortedFiles = files.OrderByDescending(f => new FileInfo(f).Length).ToArray();

        var currentZipIndex = 1;
        var currentZipFileSize = 0L;
        var currentZipFilePath = Path.Combine(outputDirectoryPath, $"part{currentZipIndex}.zip");
        var zipStream = new ZipArchive(new FileStream(currentZipFilePath, FileMode.Create, FileAccess.Write), ZipArchiveMode.Create);

        foreach (var file in sortedFiles)
        {
            var fileInfo = new FileInfo(file);

            // If the current file would cause the current zip file to exceed the maximum size, close the current zip file and create a new one.
            if (currentZipFileSize > 0 && currentZipFileSize + fileInfo.Length > maxZipFileSize)
            {
                Console.WriteLine($"Creating new zip file: {currentZipFilePath}");

                zipStream.Dispose();
                currentZipIndex++;
                currentZipFileSize = 0L;
                currentZipFilePath = Path.Combine(outputDirectoryPath, $"part{currentZipIndex}.zip");
                zipStream = new ZipArchive(new FileStream(currentZipFilePath, FileMode.Create, FileAccess.Write), ZipArchiveMode.Create);
            }

            Console.WriteLine($"Adding {fileInfo.FullName} to {currentZipFilePath}");

            // Add the file to the current zip file.
            var entryName = GetRelativePath(fileInfo.FullName, sourceDirectoryPath);
            var entry = zipStream.CreateEntry(entryName, CompressionLevel.Optimal);
            using (var input = new FileStream(file, FileMode.Open, FileAccess.Read))
            using (var output = entry.Open())
            {
                input.CopyTo(output);
            }

            currentZipFileSize += fileInfo.Length;
        }

        Console.WriteLine("Compression complete.");
    }
    static string GetRelativePath(string fullPath, string basePath)
    {
        var absolutePath = Path.GetFullPath(fullPath);
        var absoluteBasePath = Path.GetFullPath(basePath);

        if (!absolutePath.StartsWith(absoluteBasePath))
        {
            throw new ArgumentException("The fullPath must be within the basePath.", nameof(fullPath));
        }

        var relativePath = absolutePath.Substring(absoluteBasePath.Length).TrimStart(Path.DirectorySeparatorChar);

        return relativePath;
    }
    public static void CreateSplitArchive(string outputFilePath, string sourceFolderPath, string folderToArchiveName, int volumeSizeInKB)
    {
        // Define the command-line arguments
        string arguments = string.Format("a -v{0} \"{1}\\\" \"{2}\\\"", volumeSizeInKB, outputFilePath, sourceFolderPath);
        // string arguments = string.Format("a -v{0} \"{1}\\{2}\" \"{3}\\\"", volumeSizeInKB, outputFilePath, folderToArchiveName, sourceFolderPath);

        // Output the command to the console or a log file
        Console.WriteLine("WinRAR.exe {0}", arguments);

        // Create a new process to execute the command
        Process process = new Process();
        process.StartInfo.FileName = "WinRAR.exe";
        process.StartInfo.Arguments = arguments;
        process.StartInfo.WorkingDirectory = sourceFolderPath;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;

        // Start the process and wait for it to finish
        process.Start();
        process.WaitForExit();
    }
    public static void upload()
    {
        string ftpServerUrl = "ftp://houseofarabic.com";
        string ftpUsername = "u885723119.akeedtech.com";
        string ftpPassword = "ASDasd@123";

        string localFilePath = "C:\\FTP\\";
        string remoteFilePath = "/public_html";

        // Create a FTP request object
        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(ftpServerUrl + remoteFilePath));
        request.Method = WebRequestMethods.Ftp.UploadFile;
        request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);

        // Read the local file into a byte array
        byte[] fileContents;
        using (StreamReader sourceStream = new StreamReader(localFilePath))
        {
            fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
        }

        // Upload the file to the FTP server
        request.ContentLength = fileContents.Length;
        using (Stream requestStream = request.GetRequestStream())
        {
            requestStream.Write(fileContents, 0, fileContents.Length);
        }

        // Get the FTP server response
        using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
        {
            Console.WriteLine($"Upload complete. Response: {response.StatusDescription}");
        }

    }
}
