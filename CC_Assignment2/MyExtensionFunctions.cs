using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC_Assignment2
{
    public static class MyExtensionFunctions
    {
        public static bool IsDouble(this String str)
        {
            int decimalCount = 0;
            foreach (char c in str)
            {
                if (c == '.')
                {
                    decimalCount++;
                }
                if (!Char.IsDigit(c) && c != '.')
                {
                    return false;
                }
            }
            if (decimalCount == 1) return true;
            else return false;
        }

        public static bool IsInteger(this string str)
        {
            foreach (char c in str)
            {
                if (!Char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsStringLiteral(this string token)
        {
            bool quotationsOpened = false;
            foreach (char c in token)
            {
                if (c == '"')
                {
                    quotationsOpened = !quotationsOpened;
                }
            }
            if (token[0] == '"' && token[token.Length - 1] == '"' && !quotationsOpened) return true;
            else return false;
        }


        public static bool isDataType(this string token) => token == "real" || token == "integer";
    }
}
