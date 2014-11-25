using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Main
{
    public class Arff
    {
        private List<Subject> subjects;
        private BagOfWord bag;

        public Arff(List<Subject> subjects, BagOfWord bag)
        {
            this.subjects = subjects;
            this.bag = bag;
        }

        public void CreateFile(DateTime date, Set op)
        {
            var directory = FolderHelper.GetDirectory(Folder.Gerados);
            var fileName = (op == Set.TRAINING ? "1-treino" : "2-teste");
            var file = FolderHelper.CreateFile(directory, fileName +"-" + AppConfig.FeatureRanking +  ".arff");

            // @relation <NomeDoArquivo>
            file.WriteLine("@relation <{0}> \n", fileName);

            foreach (var word in bag.Words)
            {
                // @attribute <palavraDaBagOfWord_N> integer
                file.WriteLine("@attribute <{0}> integer", word.Name);
            }

            // @attribute classes {assunto1,assunto2,assunto3,assunto4,assunto5}
            file.WriteLine("@attribute classes {" + string.Join(",", subjects.Select(o => o.Name).ToList()) + "}");

            // Linha em branco
            file.WriteLine();

            // @data
            file.WriteLine("@data");

            // 0, 1, 0, 0, 0, 1, …, assunto1
            // 0, 0, 1, 0, 0, 0, …, assunto1
            // 1, 0, 1, 0, 0, 0, …, assunto5
            foreach (var subject in subjects)
            {
                List<Text> texts = null;
                switch (op)
                {
                    case Set.TRAINING:
                        texts = subject.TrainingTexts;
                        break;
                    case Set.TEST:
                        texts = subject.TestTexts;
                        break;
                }

                foreach (var text in texts)
                {
                    var str = new StringBuilder();

                    foreach (var word in bag.Words)
                    {
                        str.Append((text.Words.Any(o => o.ToUpper() == word.Name.ToUpper()) ? 1 : 0) + ",");
                    }

                    if (str.Length != 0)
                    {
                        file.Write(str.ToString());
                        file.WriteLine(subject.Name);
                    }
                }
            }

            file.Flush();
            file.Close();
        }
    }
}