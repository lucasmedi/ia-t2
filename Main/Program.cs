using System;
using System.IO;

namespace Main
{
    public class Program
    {
        static void Main(string[] args)
        {
            var stopWords = File.ReadAllText(AppConfig.FilesPath + @"\lista de stopwords Portugues.txt");

            var processing = new Processing(stopWords);
            processing.getTexts(AppConfig.FilesPath);
            processing.Preprocessing();

            Console.ReadKey();
        }
    }
}