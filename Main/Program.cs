using System;

namespace Main
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Início *****");

            // Pre-processamento
            var processing = new Processing();
            processing.LoadStopwords();
            processing.LoadTexts(Folder.Originais);
            var bag = processing.Preprocessing();

            // Criação do .ARFF
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
            //Console.ReadKey();
        }
    }
}