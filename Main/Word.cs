using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tf_ia_geradorarquivoweka
{
    class Word
    {
        public string Name { get; set; }
        public int Frequency { get; set; }

        public Word(string name)
        {
            Name = name;
        }
        public Word(string name, int frequency)
        {
            Name = name;
            Frequency = frequency;
        }
    }
}
