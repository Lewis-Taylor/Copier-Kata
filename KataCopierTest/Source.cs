using KataCopier;

namespace KataCopierTest
{
    public class Source : ISource
    {
        private char _sourceChar;

        public Source(char c)
        {
            _sourceChar = c;
        }

        public char GetChar()
        {
            return _sourceChar;
        }

    }
}