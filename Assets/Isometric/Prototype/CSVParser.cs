using System.IO;
using System.Collections.Generic;

namespace Gnome.Template
{
    public static class CSVParser
    {
        public static string[][] Parse(string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;

            List<string[]> list = new List<string[]>();

            string[] lines = s.Split('\n');

            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;

                string[] fields = line.Split(',');

                list.Add(fields);
            }

            return list.ToArray();
        }
    }
}