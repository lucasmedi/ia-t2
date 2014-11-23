using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Main
{
    public class FolderHelper
    {
        #region File Paths

        public static string AppPath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        public static string FilesPath
        {
            get
            {
                return AppPath + @"..\..\Arquivos";
            }
        }

        public static string OriginalPath
        {
            get
            {
                return FilesPath + @"\Originais";
            }
        }

        public static string PreprocessadosPath
        {
            get
            {
                return FilesPath + @"\Preprocessados";
            }
        }

        #endregion

        public static DirectoryInfo GetDirectory(Folder op, string name = "")
        {
            var path = string.Empty;
            switch (op)
            {
                case Folder.Originais:
                    path = OriginalPath;
                    break;
                case Folder.Preprocessados:
                    path = PreprocessadosPath;
                    break;
            }

            var directory = new DirectoryInfo(path);

            if (name != string.Empty)
            {
                if (directory.GetDirectories().Any(o => o.Name == name))
                {
                    directory = directory.GetDirectories(name).First();
                }
                else
                {
                    directory = directory.CreateSubdirectory(name);
                }
            }

            return directory;
        }

        public static DirectoryInfo[] GetDirectories(Folder op)
        {
            var path = string.Empty;
            switch (op)
            {
                case Folder.Originais:
                    path = OriginalPath;
                    break;
                case Folder.Preprocessados:
                    path = PreprocessadosPath;
                    break;
            }

            var directory = new DirectoryInfo(path);
            Console.WriteLine("Diretórios encontrados: {0}", directory.GetDirectories().Length.ToString());

            return directory.GetDirectories();
        }

        public static void CreateFile(DirectoryInfo directory, string name, List<string> content)
        {
            using (var sw = File.CreateText(directory.FullName + @"\" + name))
            {
                foreach (var text in content)
                {
                    sw.WriteLine(text);
                }
            }
        }
    }
}