using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Main
{
    public class Processing
    {
        public List<Subject> Subjects { get; set; }

        private string stopWords;

        public Processing()
        {
            this.Subjects = new List<Subject>();
        }

        public Processing(string stopWords)
        {
            this.Subjects = new List<Subject>();
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
                    Subjects.Add(new Subject(x.Name, texts));
                });
        }

        public BagOfWord Preprocessing()
        {
            var words = new List<Word>();
            Subjects.ForEach(x =>
            {
                Console.WriteLine("Preprocessando arquivo {0}:", x.Name);
                Console.Write("    Limpar arquivos: ");
                // Limpa arquivos
                x.RemoveStopWords(stopWords);
                x.RemoveSpecialCharacters();
                x.RemoveNumbers();
                x.RemoveOther();
                x.RemoveEmpty();
                Console.WriteLine("OK");

                Console.Write("    Salvar arquivos limpos: ");
                // Salva arquivos limpos
                x.SaveFiles();
                Console.WriteLine("OK");

                // Separa arquivos de treino e teste
                x.CalculateTraningAndTest();

                Console.Write("    Organizar e contar palavras: ");
                // Cria bag of words
                x.GenerateOwnBagOfWords();
                Console.WriteLine("OK");

                words.AddRange(x.Words);
            });

            return GenerateBagOfWords(words);
        }

        public BagOfWord GenerateBagOfWords(List<Word> words)
        {
            Console.Write("Gerar bag of words geral: ");
            var res = words.GroupBy(x => x.Name)
                .Select(x => new Word(x.Key, (int)x.Sum(w => w.Frequency)))
                .OrderByDescending(x => x.Frequency)
                .ToList<Word>();
            Console.WriteLine("OK");

            return new BagOfWord(res);
        }
    }
}