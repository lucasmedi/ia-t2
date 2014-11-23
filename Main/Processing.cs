using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Main
{
    public class Processing
    {
        List<Subject> subjects = new List<Subject>();
        string StopWords;

        public Processing()
        {
            subjects = new List<Subject>();
        }

        public Processing(string stopWords)
        {
            subjects = new List<Subject>();
            StopWords = stopWords;
        }

        public void FillSubjects(string root)
        {
            DirectoryInfo directoryRoot = new DirectoryInfo(root);
            Console.WriteLine("" + directoryRoot.GetDirectories().Length);
            directoryRoot.GetDirectories()
                .ToList()
                .ForEach(x => subjects.Add(Subject.CreateSubject(x)));
        }

        public BagOfWord Preprocessing()
        {
            List<Word> words = new List<Word>();
            subjects.ForEach(x =>
            {
                x.RemoveStopWords(StopWords);
                x.RemoveSpecialCharacters();
                x.RemoveNumbers();
                x.RemoveOther();

                x.GenerateOwnBagOfWords();

                words.AddRange(x.Words);
            });

            return null;
        }

        //public void removeRepeat
    }
}