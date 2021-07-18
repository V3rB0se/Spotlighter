using System;
using System.IO;
using System.Diagnostics;
namespace Spotlighter_V1
{
    class Program
    {
        static void Main(string[] args)
        {
            string AccountName = System.Environment.UserName;
            string destination = $@"C:\Users\{AccountName}\AppData\Local\Packages\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\LocalState\Assets";
            string targetFolder = $@"C:\Users\{AccountName}\Desktop\Spotlighter";
            string Env = Environment.GetEnvironmentVariable("WINDIR");
            string explorerDest = @"\explorer.exe";
            int totalFiles = 0;
            
            DirectoryInfo DirectofWall;
            FileInfo[] filesArr;
            Console.WriteLine("-----------------------Spotlight V1---------------------------");
            Console.WriteLine("");
            Console.WriteLine("Warning! It will Remove the Spotlight Folder From Desktop");
            Console.WriteLine("");
            Console.WriteLine("------------------Press Any Key To Continue------------------");
            Console.ReadKey();

            
            //if There's a Directory Named Spotlight Remove it
            if (Directory.Exists(targetFolder))
            {
                Console.WriteLine("Folder Already Exists! Removing...");
                Directory.Delete(targetFolder, true);
            }

            //Create A new Directory with the same name
            Directory.CreateDirectory(targetFolder);

            //Copy all the files in the original temporary spotlight folder to Targeted Folder
            foreach (var file in Directory.GetFiles(destination))
            {
                File.Copy(file, file.Replace(destination, targetFolder), true);
            }
            //Get All The Files & Rename them with jpg Extension
            foreach (var fileInTarget in Directory.GetFiles(targetFolder))
            { 
                File.Move(fileInTarget, Path.ChangeExtension(fileInTarget, ".jpg"));
            }
            // To Delete all the files below the size of 200000 Bytes = 200kb;
            DirectofWall = new DirectoryInfo(targetFolder);
            filesArr = DirectofWall.GetFiles();
            foreach (FileInfo file in filesArr)
            {
                if(file.Length < 200000)
                {
                    file.Delete();
                }
            }

            // If Directory is there
            totalFiles = Directory.GetFiles(targetFolder).Length;
            Console.WriteLine($"Total Number of Files in Spotlight Directory: {totalFiles}");

            // To Open the Folder in Explorer Process;
            Console.WriteLine("Press Any Key to Open Spotlights Folder...");
            Console.ReadKey();
            Process.Start($"{Env}{explorerDest}", targetFolder);
    
               
        }
    }
}
