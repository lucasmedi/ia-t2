using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Main
{
    public class Subject
    {
        public string Name { get; set; }

        static string StopWords;

        string TrainingText { get; set; }
        string TestText { get; set; }

        public List<Word> Words { get; set; }//TROCAR O NOME, ESTA EH A LISTA DOS COM MAIOR FREQUENCIA
        public List<Text> Texts { get; set; }

        List<Text> TrainingTexts { get; set; }
        List<Text> TestTexts { get; set; }

        public Subject(string name, List<Text> texts)
        {
            Name = name;
            Texts = texts;
        }

        public void GenerateOwnBagOfWords()
        {
            TrainingTexts.ForEach(t =>
                {
                    Words = t.Words.GroupBy(x => x.ToUpper())
                    .Select(x => new Word(x.Key, x.Count()))
                    .OrderByDescending(x => x.Frequency)
                    .ToList<Word>()
                    .Take(AppConfig.FeatureRanking).ToList();
                }
            );
        }

        public void RemoveStopWords(string stopWord)
        {
            var stringSeparators = new string[] { "\n", "\r" };
            var stopWords = stopWord.Split(stringSeparators, StringSplitOptions.None).ToList<string>();
            
            stopWords.ForEach(x =>
                {
                    Texts.ForEach(t => t.Words.RemoveAll(w => x.Equals(w, StringComparison.InvariantCultureIgnoreCase)));
                }
            );
        }

        public void RemoveNumbers()
        {
            Texts.ForEach(t => t.Words.RemoveAll(w => Regex.IsMatch(w, @"[\d-]")));
        }

        public void RemoveSpecialCharacters()
        {
            Texts.ForEach(t => t.Words.RemoveAll(w => Regex.IsMatch(w, @"[^0-9a-zA-Z]+")));
        }

        public void RemoveOther()
        {
            Texts.ForEach(t => t.Words.RemoveAll(w => w.Length < 2));
        }

        public void RemoveEmpty()
        {
            Texts.ForEach(t => t.Words.RemoveAll(w => w == String.Empty));
        }

        public void CalculateTraningAndTest()
        {
            TrainingTexts = new List<Text>();
            TestTexts = new List<Text>();
            int amountFilesTraining = CalculatePercentage(Texts.Count, Sets.TRAINING);
            for (int i = 0; i < amountFilesTraining; i++)
            {
                TrainingTexts.Add(Texts[i]);
            }

            for (int i = amountFilesTraining; i < Texts.Count; i++)
            {
                TestTexts.Add(Texts[i]);
            }
        }

        public void SaveFiles()
        {
            var directory = FolderHelper.GetDirectory(Folder.Preprocessados, Name);
            foreach (var text in Texts)
            {
                FolderHelper.CreateFile(directory, text.FileName, text.Words);
            }
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