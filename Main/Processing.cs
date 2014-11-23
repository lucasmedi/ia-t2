using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Main
{
    public class Processing
    {
        private List<Subject> subjects;
        private string stopWords;

        public Processing()
        {
            this.subjects = new List<Subject>();
        }

        public Processing(string stopWords)
        {
            this.subjects = new List<Subject>();
            this.stopWords = stopWords;
        }

        public void getTexts(Folder op)
        {
            FolderHelper.GetDirectories(op)
                .ToList()
                .ForEach(x =>
                {
                    var texts = new List<Text>();
                    x.GetFiles().ToList().ForEach(f => texts.Add(new Text(f)));
                    subjects.Add(new Subject(x.Name, texts));
                });
        }

        public BagOfWord Preprocessing()
        {
            var words = new List<Word>();
            subjects.ForEach(x =>
            {
                x.RemoveStopWords(stopWords);
                x.RemoveSpecialCharacters();
                x.RemoveNumbers();
                x.RemoveOther();
                x.SaveFiles();

                x.CalculateTraningAndTest();
                x.GenerateOwnBagOfWords();

                words.AddRange(x.Words);
            });

            return GenerateBagOfWords(words);
        }

        public BagOfWord GenerateBagOfWords(List<Word> words)
        {
            var res = words.GroupBy(x => x.Name)
                .Select(x => new Word(x.Key, (int)x.Sum(w => w.Frequency)))
                .OrderByDescending(x => x.Frequency)
                .ToList<Word>();

            return new BagOfWord(res);
        }
    }
}