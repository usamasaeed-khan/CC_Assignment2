using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CC_Assignment2
{
    class Program
    {
        static void Main(string[] args)
        {
            MiniPascal obj = new MiniPascal();
            string code = obj.ReadCode(@"../../../PascalCode.pas");
            obj.ScanCode(code);
            obj.WriteTokensToFile(@"../../../TokensList.csv");
            obj.WriteSymbolTableToFile(@"../../../SymbolTable.csv");

            Console.Read();

        }
    }
}
