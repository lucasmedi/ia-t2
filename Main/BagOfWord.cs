using System;
using System.Collections.Generic;
using System.Linq;

namespace Main
{
    public class BagOfWord
    {
        public List<Word> words;

        public BagOfWord()
        {
            words = new List<Word>();
        }

        public BagOfWord(List<Word> words)
        {
            this.words = words;
        }

        public void GetWords(string value)
        {
            string[] stringSeparators = new string[] { "\n", "\r" };
            value.Split(stringSeparators, StringSplitOptions.None)
                .ToList<String>()
                .ForEach(x => words.Add(new Word(x)));
        }
    }
}