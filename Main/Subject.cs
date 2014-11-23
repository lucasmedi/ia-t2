using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Main
{
    public class Subject
    {
        public string Text { get; set; }
        public string TrainingText { get; set; }
        public string TestText { get; set; }
        public List<string> Texts { get; set; }
        public List<string> TrainingTexts { get; set; }
        public List<string> TestTexts { get; set; }
        public BagOfWord BagOfWord { get; set; }

        public Subject() { }

        public Subject(string text)
        {
            Text = text;
        }

        public Subject(string trainingText, string testText)
        {
            TrainingText = trainingText;
            TestText = testText;
            TrainingTexts = new List<String>();
            TestTexts = new List<String>();
            FillTextList(trainingText, TrainingTexts);
            FillTextList(testText, TestTexts);
        }

        public static Subject GetSubject(DirectoryInfo dir)
        {
            var trainingText = new StringBuilder();
            var testText = new StringBuilder();

            int amountFilesTraining = CalculatePercentage(dir.GetFiles().Length, Sets.TRAINING);
            for (int i = 0; i < amountFilesTraining; i++)
            {
                trainingText.Append(File.ReadAllText(dir.GetFiles()[i].FullName));
            }

            for (int i = amountFilesTraining; i < dir.GetFiles().Length; i++)
            {
                testText.Append(File.ReadAllText(dir.GetFiles()[i].FullName));
            }

            return new Subject(trainingText.ToString(), testText.ToString());
        }

        public void RemoveStopWords(string stopWord)
        {
            var stringSeparators = new string[] { "\n", "\r" };
            var stopWords = stopWord.Split(stringSeparators, StringSplitOptions.None).ToList<string>();
            Console.WriteLine("TRAIN: " + TrainingTexts.ToArray().Length);
            Console.WriteLine("TEST: " + TestTexts.ToArray().Length);

            stopWords.ForEach(x =>
            {
                TrainingTexts
                    .RemoveAll(tr => x.Equals(tr, StringComparison.InvariantCultureIgnoreCase));
                TestTexts
                    .RemoveAll(te => x.Equals(te, StringComparison.InvariantCultureIgnoreCase));
            });

            Console.WriteLine("TRAIN2: " + TrainingTexts.ToArray().Length);
            Console.WriteLine("TEST2: " + TestTexts.ToArray().Length);
        }

        public void RemoveNumbers()
        {
            Console.WriteLine("TRAIN3: " + TrainingTexts.ToArray().Length);
            Console.WriteLine("TEST3: " + TestTexts.ToArray().Length);

            TrainingTexts.RemoveAll(tr => Regex.IsMatch(tr, @"[\d-]"));
            TestTexts.RemoveAll(te => Regex.IsMatch(te, @"[\d-]"));

            Console.WriteLine("TRAIN4: " + TrainingTexts.ToArray().Length);
            Console.WriteLine("TEST4: " + TestTexts.ToArray().Length);
        }

        public void RemoveSpecialCharacters()
        {
            Console.WriteLine("TRAIN5: " + TrainingTexts.ToArray().Length);
            Console.WriteLine("TEST5: " + TestTexts.ToArray().Length);

            TrainingTexts.RemoveAll(tr => Regex.IsMatch(tr, @"[^0-9a-zA-Z]+"));
            TestTexts.RemoveAll(te => Regex.IsMatch(te, @"[^0-9a-zA-Z]+"));

            Console.WriteLine("TRAIN6: " + TrainingTexts.ToArray().Length);
            Console.WriteLine("TEST6: " + TestTexts.ToArray().Length);
        }

        public void RemoveOther()
        {
            Console.WriteLine("TRAIN7: " + TrainingTexts.ToArray().Length);
            Console.WriteLine("TEST7: " + TestTexts.ToArray().Length);

            TrainingTexts.RemoveAll(tr => tr.Length < 2);
            TestTexts.RemoveAll(te => te.Length < 2);

            Console.WriteLine("TRAIN8: " + TrainingTexts.ToArray().Length);
            Console.WriteLine("TEST8: " + TestTexts.ToArray().Length);
        }

        public void GenerateBagOfWords()
        {
            var frequencies = TrainingTexts.GroupBy(x => x.ToUpper()).Select(x => new Word(x.Key, x.Count())).OrderByDescending(x => x.Frequency).ToList<Word>();
            BagOfWord = new BagOfWord(frequencies);
        }

        #region Private Methods

        private static int CalculatePercentage(int value, Sets sets)
        {
            return (value * (int)sets) / 100;
        }

        private void FillTextList(string value, List<string> list)
        {
            string[] stringSeparators = new string[] { "\n", "\r" };
            value.Split(stringSeparators, StringSplitOptions.None)
                .ToList<String>()
                .ForEach(x => list.Add(x));
        }

        #endregion
    }
}