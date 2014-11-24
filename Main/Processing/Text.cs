using System.Collections.Generic;
using System.IO;

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

            FillTextList(file);
        }

        public Text(string label, List<string> words)
        {
            this.FileName = label;
            this.Words = words;
        }

        private void FillTextList(FileInfo file)
        {
            using (var stream = file.OpenText())
            {
                while (!stream.EndOfStream)
                {
                    Words.Add(stream.ReadLine());
                }
            }
        }
    }
}