using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.RegularExpressions;


namespace tf_ia_geradorarquivoweka
{
    class Subject
    {
        string TrainingText { get; set; }
        string TestText { get; set; }
        List<string> Texts { get; set; }
        List<string> TrainingTexts { get; set; }
        List<string> TestTexts { get; set; }
        public List<Word> Words { get; set; }
        static string StopWords;

        public Subject() { }

        public Subject(string stopWords)
        {
            StopWords = stopWords;
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

        public static Subject CreateSubject(DirectoryInfo dir)
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


        //ACHAR UM NOME MELHOR
        public void GenerateOwnBagOfWords()
        {
            Words = TrainingTexts.GroupBy(x => x.ToUpper()).Select(x => new Word(x.Key, x.Count())).OrderByDescending(x => x.Frequency).ToList<Word>().Take((int)Sets.FEATURESRANKING).ToList();
        }

        public void RemoveStopWords(string stopWord)
        {
            string[] stringSeparators = new string[] { "\n", "\r" };
            List<string> stopWords = stopWord.Split(stringSeparators, StringSplitOptions.None).ToList<string>();
            Debug.Print("TRAIN: " + TrainingTexts.ToArray().Length);
            Debug.Print("TEST: " + TestTexts.ToArray().Length);


            stopWords.ForEach(x =>
                                {
                                    TrainingTexts
                                        .RemoveAll(tr => x.Equals(tr, StringComparison.InvariantCultureIgnoreCase));
                                    TestTexts
                                        .RemoveAll(te => x.Equals(te, StringComparison.InvariantCultureIgnoreCase));
                                });

            Debug.Print("TRAIN2: " + TrainingTexts.ToArray().Length);
            Debug.Print("TEST2: " + TestTexts.ToArray().Length);
        }

        public void RemoveNumbers()
        {
            Debug.Print("TRAIN3: " + TrainingTexts.ToArray().Length);
            Debug.Print("TEST3: " + TestTexts.ToArray().Length);

            TrainingTexts.RemoveAll(tr => Regex.IsMatch(tr, @"[\d-]"));
            TestTexts.RemoveAll(te => Regex.IsMatch(te, @"[\d-]"));

            Debug.Print("TRAIN4: " + TrainingTexts.ToArray().Length);
            Debug.Print("TEST4: " + TestTexts.ToArray().Length);
        }

        public void RemoveSpecialCharacters()
        {
            Debug.Print("TRAIN5: " + TrainingTexts.ToArray().Length);
            Debug.Print("TEST5: " + TestTexts.ToArray().Length);

            TrainingTexts.RemoveAll(tr => Regex.IsMatch(tr, @"[^0-9a-zA-Z]+"));
            TestTexts.RemoveAll(te => Regex.IsMatch(te, @"[^0-9a-zA-Z]+"));

            Debug.Print("TRAIN6: " + TrainingTexts.ToArray().Length);
            Debug.Print("TEST6: " + TestTexts.ToArray().Length);
        }

        public void RemoveOther()
        {
            Debug.Print("TRAIN7: " + TrainingTexts.ToArray().Length);
            Debug.Print("TEST7: " + TestTexts.ToArray().Length);

            TrainingTexts.RemoveAll(tr => tr.Length < 2);
            TestTexts.RemoveAll(te => te.Length < 2);

            Debug.Print("TRAIN8: " + TrainingTexts.ToArray().Length);
            Debug.Print("TEST8: " + TestTexts.ToArray().Length);
        }

        #region privates methods

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
