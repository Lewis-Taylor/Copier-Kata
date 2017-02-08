using KataCopier;

namespace KataCopierTest
{
    public class Destination : IDestination
    {
        private char _givenChar = ' ';

        public void SetChar(char c)
        {
            _givenChar = c;
        }

        public bool WasSetCharCalledWithCharParameter(char c)
        {
            return c == _givenChar;
        }
    }
}