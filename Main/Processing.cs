using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Main
{
    public class Processing
    {
        List<Subject> subjects = new List<Subject>();
        List<Text> Texts = new List<Text>();
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

        public void getTexts(string root)
        {
            DirectoryInfo directoryRoot = new DirectoryInfo(root);
            Console.WriteLine("" + directoryRoot.GetDirectories().Length);
            directoryRoot.GetDirectories()
                .ToList()
                .ForEach(x =>
                {
                    x.GetFiles().ToList().ForEach(f => Texts.Add(new Text().CreateText(f)));
                    subjects.Add(new Subject(x.Name, Texts));
                    Texts = new List<Text>();
                });
        }

        //public void FillSubjects(string root)
        //{
        //    DirectoryInfo directoryRoot = new DirectoryInfo(root);
        //    Console.WriteLine("" + directoryRoot.GetDirectories().Length);
        //    directoryRoot.GetDirectories()
        //        .ToList()
        //        .ForEach(x => subjects.Add(Subject.CreateSubject(x)));
        //}

        public BagOfWord Preprocessing()
        {
            List<Word> words = new List<Word>();
            subjects.ForEach(x =>
            {
                x.RemoveStopWords(StopWords);
                x.RemoveSpecialCharacters();
                x.RemoveNumbers();
                x.RemoveOther();

                x.CalculateTraningAndTest();

                x.GenerateOwnBagOfWords();

                words.AddRange(x.Words);
            });

            return GenerateBagOfWords(words);
        }

        public BagOfWord GenerateBagOfWords(List<Word> words)
        {
            var Words = words.GroupBy(x => x.Name)
                .Select(x => new Word(x.Key, (int)x.Sum(w => w.Frequency)))
                .OrderByDescending(x => x.Frequency)
                .ToList<Word>();

            return new BagOfWord(words);
        }
    }
}