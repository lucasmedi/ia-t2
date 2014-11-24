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

        public static string ArquivosPath
        {
            get
            {
                return AppPath + @"..\..\Arquivos";
            }
        }

        public static string OriginaisPath
        {
            get
            {
                return ArquivosPath + @"\originais";
            }
        }

        public static string PreprocessadosPath
        {
            get
            {
                return ArquivosPath + @"\preprocessados";
            }
        }

        public static string GeradosPath
        {
            get
            {
                return ArquivosPath + @"\gerados";
            }
        }

        #endregion

        public static DirectoryInfo GetDirectory(Folder op, string name = "")
        {
            var directory = new DirectoryInfo(GetPath(op));

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
            var directory = new DirectoryInfo(GetPath(op));
            Console.WriteLine("Diretórios encontrados: {0}", directory.GetDirectories().Length.ToString());

            return directory.GetDirectories();
        }

        public static StreamWriter CreateFile(DirectoryInfo directory, string name)
        {
            return File.CreateText(directory.FullName + @"\" + name);
        }

        public static StreamReader ReadFile(Folder op, string name)
        {
            return File.OpenText(GetPath(op) + @"\" + name);
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

        private static string GetPath(Folder op)
        {
            var path = string.Empty;
            switch (op)
            {
                case Folder.Arquivos:
                    path = ArquivosPath;
                    break;
                case Folder.Originais:
                    path = OriginaisPath;
                    break;
                case Folder.Preprocessados:
                    path = PreprocessadosPath;
                    break;
                case Folder.Gerados:
                    path = GeradosPath;
                    break;
            }

            return path;
        }
    }
}