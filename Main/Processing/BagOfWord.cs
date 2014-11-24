using System.Collections.Generic;

namespace Main
{
    public class BagOfWord
    {
        public List<Word> Words { get; set; }

        public BagOfWord(List<Word> words)
        {
            this.Words = words;
        }
    }
}