using System;
using System.IO;

namespace Main
{
    public class Program
    {
        static void Main(string[] args)
        {
            var stopWords = File.ReadAllText(FolderHelper.FilesPath + @"\stopwords.txt");

            var processing = new Processing(stopWords);
            processing.getTexts(Folder.Originais);
            var bag = processing.Preprocessing();

            Console.ReadKey();
        }
    }
}