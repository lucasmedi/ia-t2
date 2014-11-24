using System;
using System.Collections.Generic;
using System.Linq;

namespace Main
{
    public class Processing
    {
        private List<string> stopWords;

        public List<Subject> Subjects { get; set; }

        public Processing()
        {
            this.Subjects = new List<Subject>();
            this.stopWords = new List<string>();
        }

        /// <summary>
        /// Loads stopwords file
        /// </summary>
        public void LoadStopwords()
        {
            using (var stream = FolderHelper.ReadFile(Folder.Arquivos, "stopwords.txt"))
            {
                while (!stream.EndOfStream)
                {
                    stopWords.Add(stream.ReadLine());
                }

                stream.Close();
            }
        }

        /// <summary>
        /// Loads subjects and each text
        /// </summary>
        /// <param name="op">Wich directory to search for subjects and texts</param>
        public void LoadTexts(Folder op)
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

        /// <summary>
        /// Executes de preprocessing step
        /// </summary>
        /// <returns></returns>
        public BagOfWord Preprocessing()
        {
            var words = new List<Word>();

            Subjects.ForEach(x =>
            {
                Console.WriteLine("> {0} - Pre-processando arquivos:", x.Name);
                Console.Write("    Limpar arquivos: ");
                // Limpa arquivos
                x.RemoveStopWords(stopWords);
                x.RemoveSpecialCharacters();
                x.RemoveNumbers();
                x.RemoveOther();
                x.RemoveEmpty();
                Console.WriteLine("OK");

                Console.Write("    {0} - Salvar arquivos limpos: ", x.Name);
                x.SaveFiles();
                Console.WriteLine("OK");

                Console.Write("    {0} - Dividir arquivos e gerar Bag of Words: ", x.Name);
                // Separa arquivos de treino e teste
                x.CalculateTraningAndTest();
                // Cria bag of words
                x.GenerateOwnBagOfWords();
                Console.WriteLine("OK");

                words.AddRange(x.Ranking);
            });

            return GenerateBagOfWords(words);
        }

        /// <summary>
        /// Creates the bag of words with the most found letters
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
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