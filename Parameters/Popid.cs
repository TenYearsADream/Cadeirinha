using System.Text.RegularExpressions;

namespace NewCadeirinhaIoT.Parameters
{
    public class Popid
    {       
        public static string GetFromString(string source)
        {            
            string popidPattern = @"(\d{6})";            
            foreach(Match m in Regex.Matches(source, popidPattern))
            {
                return m.Groups[1].ToString();
            }
            return "";
        }
    }
}
