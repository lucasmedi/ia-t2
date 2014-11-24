
namespace Main
{
    public class Word
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