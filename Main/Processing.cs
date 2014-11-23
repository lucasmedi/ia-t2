using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tf_ia_geradorarquivoweka
{
    class Processing
    {
        List<Subject> Subjects = new List<Subject>();
        string StopWords;


        public Processing() { }

        public Processing(string stopWords)
        {
            StopWords = stopWords;
        }

        public void FillSubjects(string root)
        {
            DirectoryInfo directoryRoot = new DirectoryInfo(root);
            Debug.Print("" + directoryRoot.GetDirectories().Length);
            directoryRoot.GetDirectories()
                .ToList()
                .ForEach(x => Subjects.Add(Subject.CreateSubject(x)));
        }

        public BagOfWord Preprocessing()
        {
            List<Word> words = new List<Word>();
            Subjects.ForEach(x =>
            {
                x.RemoveStopWords(StopWords);
                x.RemoveSpecialCharacters();
                x.RemoveNumbers();
                x.RemoveOther();

                x.GenerateOwnBagOfWords();

                words.AddRange(x.Words);
            });




            
            return null;
        }

        //public void removeRepeat


    }
}
