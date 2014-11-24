using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var directory = FolderHelper.GetDirectory(Folder.Arquivos);
            var fileName = string.Format("{0}{1}{2}{3}{4}{5}_{6}", date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, (op == Set.TRAINING ? "1-treino" : "2-teste"));
            var file = FolderHelper.CreateFile(directory, fileName + ".arff");

            // @relation <NomeDoArquivo>
            file.WriteLine("@relation {0}", fileName);

            foreach (var word in bag.Words)
            {
                // @attribute <palavraDaBagOfWord_N> integer
                file.WriteLine("@attribute {0} string", word.Name);
            }

            // @attribute classes {assunto1,assunto2,assunto3,assunto4,assunto5}
            file.WriteLine("@attributes classes {" + string.Join(",", subjects.Select(o => o.Name).ToList()) + "}");

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
                    foreach (var word in text.Words)
                    {
                        if (string.IsNullOrEmpty(word))
                        {
                            continue;
                        }

                        file.Write((bag.Words.Any(o => o.Name.ToUpper() == word.ToUpper()) ? 1 : 0) + ", ");
                    }

                    file.WriteLine(subject.Name);
                }
            }

            file.Flush();
            file.Close();
        }
    }
}