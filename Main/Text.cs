using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public class Text
    {
        public List<string> Words { get; set; }
        public string Label { get; set; }

        public Text() { }

        public Text(string label, List<string> words)
        {
            Label = label;
            Words = words;
        }

        public Text CreateText(FileInfo file)
        {
            Words = new List<string>();
            Label = file.Name;
            FillTextList(File.ReadAllText(file.FullName), Words);

            return new Text(Label, Words);
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
