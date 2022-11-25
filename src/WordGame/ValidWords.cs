namespace WordGame
{
    using System.Collections;
    using System.IO;
    using System.Reflection;

    public class ValidWords : IValidWords
    {
        // As all word are different it's better to use HashSet because it's faster (O(1))
        ArrayList a = new ArrayList();

        public ValidWords()
        {
            Stream stream = null;
            StreamReader reader = null;
            try
            {
                // not sure if using reflection here is a good idea. it would be much cleaner to put path in configuration file
                // and use FileStream
                stream = Assembly.GetAssembly(typeof(ValidWords)).GetManifestResourceStream("WordGame.wordlist.txt");
                reader = new StreamReader(stream);

                while (!reader.EndOfStream)
                {
                    a.Add(reader.ReadLine());
                }
            }
            // I guess it would be better to add 'catch' block as well, because right now all exceptions are being ignored
            finally
            {
                // please use 'using' constructions instead
                reader.Dispose();
                stream.Dispose();
            }
        }

        // it's totally ok to write this thing like that, 
        // but there is one-liner that do exactly the same (just for you tho know):
        // public int Size => a.Count;
        public int Size
        {
            get { return a.Count; }
        }

        public bool Contains(string word)
        {
            return a.Contains(word);
        }
    }
}
