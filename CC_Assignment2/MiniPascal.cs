using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Data.Common;

namespace CC_Assignment2
{
    class MiniPascal
    {
        private readonly List<Token> tokenList;
        private readonly Dictionary<string, string> symbolTable;

        private readonly string[] keywords = {

                "program", "var", "real", "integer", "begin", "end",

                "if", "then", "else", "while", "do", "repeat", "until",

                "write", "writeln", "or", "div", "mod", "and", "true",

                "false", "not", "trunc", "real", "readln"

        };
        private readonly string[] sym = {

                ".", ";", ":=", ":", "(", ")",

                "+", "-", "*", "/", "=", ">",

                "<=", ">=", "<>", "<"

        };

        public MiniPascal()
        {
            tokenList = new List<Token>();
            symbolTable = new Dictionary<string, string>();
        }

        private void AddToken(string type, string lexeme, int lineNumber, int position)
        {
            tokenList.Add(new Token(type, lexeme, lineNumber, position));
        }

        public string ReadCode(string filePath)
        {
            string code = File.ReadAllText(filePath);
            return code;
        }

        



        public string GetTokenType(string token) => keywords.Contains(token) ? "keyword" : token.IsDouble() ? "realConst" : token.IsInteger() ? "intConst" : token.IsStringLiteral() ? "stringConst" : "id" ;
        



        public string[] GetLines(string code)
        {
            return code.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        }


        public void DisplayTokenList()
        {
            foreach (Token token in tokenList)
            {
                Console.WriteLine(token.lexeme + "\t" + token.type + "\t" + token.lineNumber + "\t" + token.position);
            }
            Console.WriteLine("print");
            foreach(string str in symbolTable.Keys)
            {
                Console.WriteLine(str + "\t" + symbolTable[str]);
            }
        }

        public void WriteTokensToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Token Type,Lexeme,Line Number,Position");
                foreach (Token token in tokenList)
                {
                    writer.WriteLine(token.lexeme + "," + token.type + "," + token.lineNumber + "," + token.position);
                }
            }
        }


        public void WriteSymbolTableToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Identifier, Type");
                foreach (string key in symbolTable.Keys)
                {
                    writer.WriteLine(key + "," + symbolTable[key]);
                }
            }
        }



        public void ScanCode(String code)
        {
            bool found = false;
            string[] lines = GetLines(code);
            int lineNumber = 0;


            foreach (string line in lines)
            {

                lineNumber++;
                int index = -1;
                while (++index < line.Length)
                {

                    if (found && index > 0) index--;

                    found = false;
                    string token = "";
                    bool stringLiteral = false;
                    bool isNumber;

                    while (index < line.Length && line[index] != ' ' && !sym.Contains(line[index].ToString()))
                    {

                        if (line[index] == '"') stringLiteral = !stringLiteral;

                        isNumber = false;

                        while (index < line.Length - 1 && Char.IsDigit(line[index]) || line[index] == '.')
                        {
                            isNumber = true;
                            token += line[index];
                            index++;
                        }
                        if (!isNumber)
                        {
                            token += line[index];
                            index++;
                        }
                        while (index<line.Length && stringLiteral && (line[index] == ' ' || sym.Contains(line[index].ToString())))
                        {
                            token += line[index];
                            index++;
                        }
                    }

                    if (!String.IsNullOrEmpty(token))
                    {

                        tokenList.Add(new Token(GetTokenType(token) , token,
                            lineNumber, index + 1 - token.Length));

                        if (token.isDataType() && tokenList[tokenList.Count - 2].lexeme == ":" ) symbolTable[tokenList[tokenList.Count - 3].lexeme] = token;

                    }


                    if (index<line.Length && sym.Contains(line[index].ToString()))
                    {
                            token = "";

                            while (index < line.Length && sym.Contains(line[index].ToString()))
                            {
                                token += line[index].ToString();
                                index++;
                            }
                            if (!sym.Contains(token))
                                foreach(char str in token)
                                    tokenList.Add(new Token("symbol", token,
                                        lineNumber, index + 1 - token.Length));
                            
                            else
                                tokenList.Add(new Token("symbol", token,
                                    lineNumber, index + 1 - token.Length));
                            found = true;
                    }
                }
            }
        }
    }
}
