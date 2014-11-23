using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Main
{
    class Program
    {
        static string root = @"\Arquivos";

        static void Main(string[] args)
        {
            string stopWord = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + root + @"\lista de stopwords Portugues.txt");

            var subjects = new List<Subject>();

            var directoryRoot = new DirectoryInfo(root);
            Console.WriteLine(directoryRoot.GetDirectories().Length.ToString());
            directoryRoot.GetDirectories()
                .ToList()
                .ForEach(x => subjects.Add(Subject.GetSubject(x)));

            subjects.First().RemoveStopWords(stopWord);
            subjects.First().RemoveNumbers();
            subjects.First().RemoveSpecialCharacters();
            subjects.First().RemoveOther();

            //subjects.First().test();
        }
    }
}