using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Syncronization_folders
{
    internal class Program
    {
        static string ChooseSourceFolder()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
                return fbd.SelectedPath;

            return "Operation Canceled..";
        }

        static string ChooseReplicaFolder()
        {
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

        static bool VerifyBackupFolder(string[] sourcefiles, string[] backupfiles)
        {
            if (sourcefiles.Length != backupfiles.Length)
            {
                return false;
            }

            return true;
        }


        [STAThread]
        static void Main(string[] args)
        {
            // VARIABLES
            string sourcePath, backupPath;
            string[] sourceFiles, backupFiles;

            // GET SOURCE FOLDER
            #region
            Console.WriteLine("Choose the folder you want to backup..");
            sourcePath = ChooseSourceFolder();
            Console.WriteLine(sourcePath);
            #endregion

            // CHOOSE BACKUP FOLDER
            #region
            Console.WriteLine("Choose a folder you want to store your backup..");
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
            

            Console.ReadLine();
            

            
        }
    }
}
