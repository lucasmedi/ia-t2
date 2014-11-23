using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public class Arff
    {
        private List<string> subjects;
        private BagOfWord bag;

        public Arff(List<string> subjects, BagOfWord bag)
        {
            this.subjects = subjects;
            this.bag = bag;
        }

        public void CreateFile()
        {
            var date = DateTime.Now;
            var directory = FolderHelper.GetDirectory(Folder.Arquivos);
            var fileName = string.Format("treino_{0}{1}{2}{3}{4}{5}", date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
            var file = FolderHelper.CreateFile(directory, fileName + ".arff");

            // @relation <NomeDoArquivo>
            file.WriteLine("@relation {0}", fileName);

            foreach (var word in bag.Words)
            {
                // @attribute <palavraDaBagOfWord_N> integer
                file.WriteLine("@attribute {0} string", word.Name);
            }

            // @attribute classes {assunto1,assunto2,assunto3,assunto4,assunto5}
            file.WriteLine("@attributes classes {" + string.Join(",", subjects) + "}");

            // @data
            file.WriteLine("@data");

            // 0, 1, 0, 0, 0, 1, …, assunto1
            // 0, 0, 1, 0, 0, 0, …, assunto1
            // 1, 0, 1, 0, 0, 0, …, assunto5
            var directories = FolderHelper.GetDirectories(Folder.Preprocessados);
            foreach (var d in directories)
            {
                foreach (var f in d.GetFiles())
                {
                    using (var t = f.OpenText())
                    {
                        while (!t.EndOfStream)
                        {
                            var text = t.ReadLine();
                            if (string.IsNullOrEmpty(text))
                            {
                                continue;
                            }

                            file.Write((bag.Words.Any(o => o.Name.ToUpper() == text.ToUpper()) ? 1 : 0) + ", ");
                        }
                    }

                    file.WriteLine(d.Name);
                }
            }

            file.Flush();
            file.Close();
        }
    }
}