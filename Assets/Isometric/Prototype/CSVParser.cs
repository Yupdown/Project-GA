using System.Collections.Generic;
using System.Text;

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

                string trimmedLine = line.TrimEnd();
                string[] fields = trimmedLine.Split(',');

                list.Add(fields);
            }

            return list.ToArray();
        }
    }
}