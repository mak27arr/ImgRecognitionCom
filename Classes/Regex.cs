using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TextRecognitionComObject.Classes
{
    public class Regex
    {
        public string Replace(string str,string pattern,string symbol = "")
        {
            Regex r = new Regex();
            return r.Replace(str, pattern, symbol);
        }
        public string Match(string str, string pattern, string symbol = "")
        {
            Regex r = new Regex();
            return r.Match(str, pattern, symbol);

        }
    }
}
