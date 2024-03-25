using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Timers;
using System.Threading;

namespace Syncronization_folders
{
    internal class Program
    {
        static string ChooseSourceFolder()
        {
            Console.WriteLine("Choose the folder you want to backup..");
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
                return fbd.SelectedPath;

            return "Operation Canceled..";
        }

        static string ChooseReplicaFolder()
        {
            Console.WriteLine("Choose a folder you want to store your backup..");
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
                return fbd.SelectedPath;

            return "Operation Canceled..";
        }

        static string[] GetSourceFiles(string source)
        { 
            return Directory.GetFiles(source);
        }

        static string[] GetBackupFiles(string backup)
        {
            return Directory.GetFiles(backup);
        }

        static int ConfigureTimerTime()
        {
            Console.WriteLine("What interval of time you want the program to look for changes on the source folder? (seconds [ default: 300 seconds { 5 minutes } ] )");
            int interval = Convert.ToInt32(Console.ReadLine());

            return interval;
        }

        static bool VerifyLastModification(string source, string backup)
        {

            DateTime lastModifiedSource = Directory.GetLastWriteTime(source);
            DateTime lastModifiedBackup = Directory.GetLastWriteTime(backup);

            if (lastModifiedSource > lastModifiedBackup)
            {
                return true;
            }

            return false;
        }

        static bool VerifyFolderToSave()
        {
            Console.WriteLine("Are you sure this is the folder you want to create a backup of? (1 - YES; 0 - NO)");
            int saveSource = Console.Read();

            Console.WriteLine("Are you sure this is where you want to keep your backup? (1 - YES; 0 - NO)");
            int saveBackup = Console.Read();

            if (saveSource == 1 && saveBackup == 1)
            {
                ConfigureTimerTime();
                return true;

            }
            else if (saveSource == 1 && saveBackup != 1)
            {
                ChooseReplicaFolder();
                VerifyFolderToSave();
                return false;
            }
            else if (saveSource != 1 && saveBackup == 1)
            {
                ChooseSourceFolder();
                VerifyFolderToSave();
                return false;
            }
            else
            {
                ChooseSourceFolder();
                ChooseReplicaFolder();
                VerifyFolderToSave();
                return false;
            }
        }

        static bool saveLogFile()
        {
            return false;
        }


        [STAThread]
        static void Main(string[] args)
        {
            // VARIABLES
            #region
            string sourcePath, backupPath;
            string[] sourceFiles, backupFiles;

            TimeSpan coulddown = TimeSpan.FromSeconds(ConfigureTimerTime());
            System.Timers.Timer timer = new System.Timers.Timer(coulddown.TotalMilliseconds);

            #endregion

            timer.AutoReset = true;
            timer.Stop();

            // GET SOURCE FOLDER
            #region
            sourcePath = ChooseSourceFolder();
            Console.WriteLine(sourcePath);
            #endregion

            // CHOOSE BACKUP FOLDER
            #region
            backupPath = ChooseReplicaFolder();
            Console.WriteLine(backupPath);
            #endregion

            // READ ALL SOURCE FILES
            #region
            sourceFiles = GetSourceFiles(sourcePath);

            foreach(string file in sourceFiles)
            {
                Console.WriteLine(file);
            }
            #endregion

            // READ ALL BACKUP FILES
            #region
            backupFiles = GetBackupFiles(backupPath);

            foreach(string file in backupFiles)
            {
                Console.WriteLine(file);
            }
            #endregion


            if (VerifyFolderToSave())
            {
                timer.Start();


            }



            // PRINT THERE ARE MODIFICATIONS ON THE FOLDER
            #region
            if (VerifyLastModification(sourcePath, backupPath))
            {
                Console.WriteLine("Existem modificações no ficheiro raiz");

            }

            #endregion

            Console.ReadLine();
            

            
        }
    }
}
