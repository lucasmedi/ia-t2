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

            Arff arff = null;
            var date = DateTime.Now;

            Console.Write("Gerar arquivo de treino .arff: ");
            arff = new Arff(processing.Subjects, bag);
            arff.CreateFile(date, Set.TRAINING);
            Console.WriteLine("OK");

            Console.Write("Gerar arquivo .arff: ");
            arff = new Arff(processing.Subjects, bag);
            arff.CreateFile(date, Set.TEST);
            Console.WriteLine("OK");

            Console.WriteLine("***** Fim *****");
            Console.ReadKey();
        }
    }
}