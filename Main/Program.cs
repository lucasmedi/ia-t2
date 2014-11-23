using System;
using System.IO;

namespace Main
{
    public class Program
    {
        static string root = @"C:\Users\Giovanni_2\Dropbox\PUCRS\Inteligência Artificial\t2\Arquivos";

        static void Main(string[] args)
        {
            var stopWord = File.ReadAllText(root + @"\lista de stopwords Portugues.txt");

            var processing = new Processing(stopWord);
            processing.FillSubjects(root);
            processing.Preprocessing();

            Console.ReadKey();
        }
    }
}