using System;
using System.IO;
using System.IO.Compression;

class Program
{
    static void Main(string[] args)
    {
        // Specify the path to the source ZIP archive
        string sourceZipFilePath = @"D:\ZipFiles\Test.zip";

        // Specify the maximum size for each volume (in bytes)
        long maxVolumeSize = 50 * 1024 * 1024; // 50 MB

        // Specify the path for the output volumes
        string outputDirectoryPath = @"D:\ZipFiles\tt\";

        // Create the output directory if it does not exist
        if (!Directory.Exists(outputDirectoryPath))
        {
            Directory.CreateDirectory(outputDirectoryPath);
        }

        // Open the source ZIP archive
        var sourceZip = ZipFile.Open(sourceZipFilePath, ZipArchiveMode.Read);
        // Get the name of the source ZIP archive (without the file extension)
        string archiveName = Path.GetFileNameWithoutExtension(sourceZipFilePath);

        // Create the first volume
        int volumeNumber = 1;
        long volumeSize = 0;
        string volumeFilePath = Path.Combine(outputDirectoryPath, $"{archiveName}.part{volumeNumber:000}.zip");
        var volumeZip = new ZipArchive(new FileStream(volumeFilePath, FileMode.Create, FileAccess.Write), ZipArchiveMode.Create);
        // Add each entry in the source ZIP archive to the current volume
        foreach (var entry in sourceZip.Entries)
        {
            // If the current entry would cause the current volume to exceed the maximum size, close the current volume and create a new one
            if (volumeSize + entry.Length > maxVolumeSize)
            {
                Console.WriteLine($"Creating new zip file");

                volumeZip.Dispose();
                volumeNumber++;
                volumeSize = 0;
                volumeFilePath = Path.Combine(outputDirectoryPath, $"{archiveName}.part{volumeNumber:000}.zip");
                volumeZip = new ZipArchive(new FileStream(volumeFilePath, FileMode.Create, FileAccess.Write), ZipArchiveMode.Create);
            }

            // Add the entry to the current volume
            using (var input = entry.Open())
            {
                var output = volumeZip.CreateEntry(entry.FullName, CompressionLevel.Optimal).Open();
                input.CopyTo(output);
            }

            volumeSize += entry.Length;
        }

        Console.WriteLine("Splitting complete.");
    }
}
