using System;
using System.Collections.Generic;
using System.Linq;

namespace Main
{
    public class BagOfWord
    {
        public List<Word> Words { get; set; }

        public BagOfWord(List<Word> words)
        {
            this.Words = words;
        }

        public void GetWords(string value)
        {
            string[] stringSeparators = new string[] { "\n", "\r" };
            value.Split(stringSeparators, StringSplitOptions.None)
                .ToList<String>()
                .ForEach(x => Words.Add(new Word(x)));
        }
    }
}