using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC_Assignment2
{
    class Token
    {
        public string type { get; }
        public string lexeme { get; }
        public int lineNumber { get; }
        public int position { get; }
        public Token(string type, string lexeme, int lineNumber, int position)
        {
            this.type = type;
            this.lexeme = lexeme;
            this.lineNumber = lineNumber;
            this.position = position;
        }
    }
}
