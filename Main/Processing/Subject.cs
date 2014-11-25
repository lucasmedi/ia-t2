using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Main
{
    public class Subject
    {
        public string Name { get; set; }

        public List<Word> Ranking { get; set; }

        public List<Text> Texts { get; set; }

        public List<Text> TrainingTexts { get; set; }
        public List<Text> TestTexts { get; set; }

        public Subject(string name, List<Text> texts)
        {
            Name = name;
            Texts = texts;
        }

        public void GenerateOwnBagOfWords()
        {
            TrainingTexts.ForEach(t =>
                {
                    Ranking = t.Words.GroupBy(x => x.ToLower())
                        .Select(x => new Word(x.Key, x.Count()))
                        .OrderByDescending(x => x.Frequency)
                        .ToList<Word>()
                        .Take(AppConfig.FeatureRanking).ToList();
                }
            );
        }

        public void RemoveStopWords(List<string> stopWords)
        {
            stopWords.ForEach(x =>
                {
                    Texts.ForEach(t => t.Words.RemoveAll(w => x.Equals(w, StringComparison.InvariantCultureIgnoreCase)));
                }
            );
        }

        public void RemoveNumbers()
        {
            foreach (var text in Texts)
            {
                text.Words = text.Words.Select(w => Regex.Replace(w, @"[\d-]", "")).ToList();
            }
        }

        public void RemoveSpecialCharacters()
        {
            foreach (var text in Texts)
            {
                text.Words = text.Words.Select(w => new string(w.ToCharArray().Where(c => Char.IsLetter(c)).ToArray())).ToList();
            }
        }

        public void RemoveEmptySpaces()
        {
            foreach (var text in Texts)
            {
                text.Words = text.Words.Select(w => Regex.Replace(w, @"[\s+]", "")).ToList();
            }
        }

        public void RemoveOther()
        {
            Texts.ForEach(t => t.Words.RemoveAll(w => w.Length < 2));
        }

        public void RemoveEmpty()
        {
            Texts.ForEach(t => t.Words.RemoveAll(w => string.IsNullOrEmpty(w)));
        }

        public void CalculateTraningAndTest()
        {
            TrainingTexts = new List<Text>();
            TestTexts = new List<Text>();
            int amountFilesTraining = CalculatePercentage(Texts.Count, Set.TRAINING);
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

        private static int CalculatePercentage(int value, Set sets)
        {
            return (value * (int)sets) / 100;
        }

        #endregion
    }
}