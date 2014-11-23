using System;
using System.IO;

namespace Main
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Início *****");

            var stopWords = File.ReadAllText(FolderHelper.ArquivosPath + @"\stopwords.txt");

            var processing = new Processing(stopWords);
            processing.getTexts(Folder.Originais);
            var bag = processing.Preprocessing();

            Console.Write("Gerar arquivo .arff: ");
            var arff = new Arff(processing.GetSubjects(), bag);
            arff.CreateFile();
            Console.WriteLine("OK");

            Console.WriteLine("***** Fim *****");
            Console.ReadKey();
        }
    }
}