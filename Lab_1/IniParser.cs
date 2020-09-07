using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Lab_1
{
    public static class IniParser
    {
        public static IniParserData Parse(string path)
        {
            var data = new IniParserData();
            using var f = File.OpenText(path);
            var sectionName = "";
            
            while (!f.EndOfStream)
            {
                var line = f.ReadLine();
                if (line == null) break;

                // Remove all comments
                if (line.Contains(';'))
                    line = line.Substring(0, line.IndexOf(';'));
                
                line = line.Trim();
                
                if (line.Equals(""))
                    continue;

                var sectionMatch = Regex.Match(line, @"^\[[A-Za-z0-9_]+\]$");
                var fieldMatch = Regex.Match(line, @"^[A-Za-z0-9_]+\s*=\s*\S*$");
                
                if (sectionMatch.Success)
                {
                    sectionName = sectionMatch.Value;
                    sectionName = sectionName.Remove(sectionName.Length - 1, 1).Remove(0, 1);
                    data.AssertNewSection(sectionName);
                }
                else if (fieldMatch.Success && !sectionName.Equals(""))
                {
                    var values = Regex.Split(fieldMatch.Value, @"\s*=\s*");
                    if (values.Length != 2)
                        throw new FormatException("File has wrong format");
                    
                    data.AddField(sectionName, values[0], values[1]);
                }
                else
                {
                    throw new FormatException("File has wrong format");
                }
            }

            return data;
        }
    }
}