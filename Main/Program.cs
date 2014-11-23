using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace tf_ia_geradorarquivoweka
{
    class Program
    {

        static string root = @"C:\Users\Giovanni_2\Dropbox\PUCRS\Inteligência Artificial\t2\Arquivos";

        static void Main(string[] args)
        {
            string stopWord = File.ReadAllText(root + @"\lista de stopwords Portugues.txt");

            Processing processing = new Processing(stopWord);

            processing.FillSubjects(root);

            processing.Preprocessing();
        }

       
    }
}
