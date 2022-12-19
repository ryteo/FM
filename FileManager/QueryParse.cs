using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    public class QueryParse
    {
        public string ToShortPath(string path)
        {
            path = path.Trim(new char[] { '"', '"' });

            return path;
        }
        public string _ToShortPath(string path)
        {
            string[] pathStr = path.Split('\\');           
            if (pathStr.Length > 3)
            {
                return @$"{pathStr[0]}\...\{pathStr[pathStr.Length - 2]}\{pathStr[pathStr.Length - 1]}";
            }
            else return path;
        }
    }
}
