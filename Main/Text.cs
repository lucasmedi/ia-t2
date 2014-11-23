using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Main
{
    public class Text
    {
        public string FileName { get; set; }
        public List<string> Words { get; set; }

        public Text(FileInfo file)
        {
            this.FileName = file.Name;
            this.Words = new List<string>();
            FillTextList(File.ReadAllText(file.FullName), this.Words);
        }

        public Text(string label, List<string> words)
        {
            this.FileName = label;
            this.Words = words;
        }

        private static void FillTextList(string value, List<string> list)
        {
            string[] stringSeparators = new string[] { "\n", "\r" };
            value.Split(stringSeparators, StringSplitOptions.None)
                .ToList<String>()
                .ForEach(x => list.Add(x));
        }
    }
}