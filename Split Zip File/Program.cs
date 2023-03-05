//using System;
//using System.IO;
//using System.IO.Compression;

//class Program
//{
//    static void Main(string[] args)
//    {
//        try
//        {
//            var sourceDirectory = @"D:\Test"; // Change this to the directory you want to split into zip files.
//            var outputDirectory = @"D:\ZipFiles"; // Change this to the directory where you want to save the zip files.
//            var maxZipFileSize = 50 * 1024 * 1024; // Change this to the maximum size (in bytes) of each zip file.
//            SplitDirectoryToZipFiles(sourceDirectory, outputDirectory, maxZipFileSize);
//            JoinSplitFiles(outputDirectory);
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Error: {ex.Message}");
//        }
//    }
//    static void JoinSplitFiles(string directoryPath)
//    {
//        var zipFilePaths = Directory.GetFiles(directoryPath, "*.zip");

//        var outputDirectoryPath = Path.Combine(directoryPath, "output");
//        Directory.CreateDirectory(outputDirectoryPath);

//        var processedEntryNames = new HashSet<string>();

//        foreach (var zipFilePath in zipFilePaths)
//        {
//            using (var zipStream = new ZipArchive(new FileStream(zipFilePath, FileMode.Open, FileAccess.Read)))
//            {
//                foreach (var entry in zipStream.Entries)
//                {
//                    var outputFilePath = Path.Combine(outputDirectoryPath, entry.Name);

//                    if (processedEntryNames.Contains(entry.Name))
//                    {
//                        using (var outputExeStream = new FileStream(outputFilePath, FileMode.Append, FileAccess.Write))
//                        {
//                            foreach (var splitEntry in zipStream.Entries)
//                            {
//                                if (splitEntry.Name == entry.Name)
//                                {
//                                    using (var inputExeStream = splitEntry.Open())
//                                    {
//                                        inputExeStream.CopyTo(outputExeStream);
//                                    }
//                                }
//                            }
//                        }

//                        continue;
//                    }

//                    processedEntryNames.Add(entry.Name);

//                    using (var outputExeStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
//                    {
//                        foreach (var splitEntry in zipStream.Entries)
//                        {
//                            if (splitEntry.Name == entry.Name)
//                            {
//                                using (var inputExeStream = splitEntry.Open())
//                                {
//                                    inputExeStream.CopyTo(outputExeStream);
//                                }
//                            }
//                        }
//                    }
//                }
//            }
//        }

//        Console.WriteLine("Join complete.");
//    }
//    static void SplitExeFile(string exeFilePath, string outputDirectory, long chunkSize, int numChunks)
//    {
//        using (var exeStream = new FileStream(exeFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
//        {
//            byte[] buffer = new byte[chunkSize];
//            int bytesRead;
//            int i = 0;

//            for (; i < numChunks - 1; i++)
//            {
//                string outputFilePath = Path.Combine(outputDirectory, $"{i}.zip");
//                using (var outputZipFile = ZipFile.Open(outputFilePath, ZipArchiveMode.Create))
//                using (var outputZipStream = outputZipFile.CreateEntry(Path.GetFileName(exeFilePath), CompressionLevel.Optimal).Open())
//                {
//                    long bytesRemaining = chunkSize;
//                    while (bytesRemaining > 0 && (bytesRead = exeStream.Read(buffer, 0, (int)Math.Min(bytesRemaining, buffer.Length))) > 0)
//                    {
//                        outputZipStream.Write(buffer, 0, bytesRead);
//                        bytesRemaining -= bytesRead;
//                    }
//                }
//            }

//            // Last chunk may be smaller
//            string lastOutputFilePath = Path.Combine(outputDirectory, $"{i}.zip");
//            using (var lastOutputZipFile = ZipFile.Open(lastOutputFilePath, ZipArchiveMode.Create))
//            using (var lastOutputZipStream = lastOutputZipFile.CreateEntry(Path.GetFileName(exeFilePath), CompressionLevel.Optimal).Open())
//            {
//                while ((bytesRead = exeStream.Read(buffer, 0, buffer.Length)) > 0)
//                {
//                    lastOutputZipStream.Write(buffer, 0, bytesRead);
//                }
//            }
//        }
//    }
//    static void SplitDirectoryToZipFiles(string sourceDirectoryPath, string outputDirectoryPath, long maxZipFileSize)
//    {
//        if (!Directory.Exists(sourceDirectoryPath))
//        {
//            Console.WriteLine($"Error: Source directory does not exist: {sourceDirectoryPath}");
//            return;
//        }

//        if (!Directory.Exists(outputDirectoryPath))
//        {
//            Directory.CreateDirectory(outputDirectoryPath);
//        }

//        // Get all files in the source directory and subdirectories.
//        var files = Directory.GetFiles(sourceDirectoryPath, "*", SearchOption.AllDirectories);

//        // Sort the files by size (descending) so we can add the largest files to the zip files first.
//        var sortedFiles = files.OrderByDescending(f => new FileInfo(f).Length).ToArray();

//        var currentZipIndex = 1;
//        var currentZipFileSize = 0L;
//        var currentZipFilePath = Path.Combine(outputDirectoryPath, $"part{currentZipIndex}.zip");
//        var zipStream = new ZipArchive(new FileStream(currentZipFilePath, FileMode.Create, FileAccess.Write), ZipArchiveMode.Create);

//        foreach (var file in sortedFiles)
//        {
//            var fileInfo = new FileInfo(file);

//            // If the current file would cause the current zip file to exceed the maximum size, close the current zip file and create a new one.
//            if (currentZipFileSize > 0 && currentZipFileSize + fileInfo.Length > maxZipFileSize)
//            {
//                Console.WriteLine($"Creating new zip file: {currentZipFilePath}");

//                zipStream.Dispose();
//                currentZipIndex++;
//                currentZipFileSize = 0L;
//                currentZipFilePath = Path.Combine(outputDirectoryPath, $"part{currentZipIndex}.zip");
//                zipStream = new ZipArchive(new FileStream(currentZipFilePath, FileMode.Create, FileAccess.Write), ZipArchiveMode.Create);
//            }

//            Console.WriteLine($"Adding {fileInfo.FullName} to {currentZipFilePath}");

//            // Add the file to the current zip file.
//            var entryName = GetRelativePath(fileInfo.FullName, sourceDirectoryPath);
//            var entry = zipStream.CreateEntry(entryName, CompressionLevel.Optimal);
//            using (var input = new FileStream(file, FileMode.Open, FileAccess.Read))
//            using (var output = entry.Open())
//            {
//                input.CopyTo(output);
//            }

//            currentZipFileSize += fileInfo.Length;
//        }

//        Console.WriteLine("Compression complete.");
//    }
//    static string GetRelativePath(string fullPath, string basePath)
//    {
//        var absolutePath = Path.GetFullPath(fullPath);
//        var absoluteBasePath = Path.GetFullPath(basePath);

//        if (!absolutePath.StartsWith(absoluteBasePath))
//        {
//            throw new ArgumentException("The fullPath must be within the basePath.", nameof(fullPath));
//        }

//        var relativePath = absolutePath.Substring(absoluteBasePath.Length).TrimStart(Path.DirectorySeparatorChar);

//        return relativePath;
//    }
//}



using System;
using System.Net;

namespace DownloadFileFromGoogleDrive
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a web client
            var client = new WebClient();

            // Set your credentials if needed
            client.Credentials = new NetworkCredential("testdrive5703@gmail.com", "ASDasd@123");

            // Get the file url from Google Drive
            var url = "https://drive.google.com/uc?export=download&id=1gkZWA-tQJ1imZXJgFQa_LiUVhmUqadKm";
            var url1 = "https://drive.google.com/uc?export=download&id=1dJc0I7e2rMgHe16altXxny2KPoRwpTvV";
            // Download the file to a local path
            //client.DownloadFile(link, "Untitled document");
            // download to specific folder
            client.DownloadFile(url, @"D:\RAR\testtt.pdf");
            client.DownloadFile(url1, @"D:\RAR\testtt.json");
            // Display a message
            Console.WriteLine("File downloaded successfully.");
            //https://drive.google.com/file/d//view?usp=sharing
            //https://drive.google.com/file/d/1gkZWA-tQJ1imZXJgFQa_LiUVhmUqadKm/view?usp=share_link
            //https://drive.google.com/file/d/1gkZWA-tQJ1imZXJgFQa_LiUVhmUqadKm/view?usp=share_link
            //https://drive.google.com/file/d/1dJc0I7e2rMgHe16altXxny2KPoRwpTvV/view?usp=share_link
            //https://drive.google.com/file/d/1dJc0I7e2rMgHe16altXxny2KPoRwpTvV/view?usp=share_link
            //https://drive.google.com/file/d/1dJc0I7e2rMgHe16altXxny2KPoRwpTvV/view?usp=share_link
            //https://drive.google.com/file/d/1dJc0I7e2rMgHe16altXxny2KPoRwpTvV/view
            //https://drive.google.com/file/d/1dJc0I7e2rMgHe16altXxny2KPoRwpTvV/view?usp=share_link
        }
    }
}
