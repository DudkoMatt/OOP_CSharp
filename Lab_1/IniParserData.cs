using System;
using System.Collections.Generic;

namespace Lab_1
{
    public class IniParserData
    {
        private Dictionary<string, Dictionary<string, string>> _data;
        private string _lastSectionName;

        public IniParserData()
        {
            _data = new Dictionary<string, Dictionary<string, string>>();
            _lastSectionName = "";
        }
        
        public void AddField(string sectionName, string fieldName, string fieldValue)
        {
            if (_data.ContainsKey(sectionName) && sectionName != _lastSectionName)
                throw new FormatException($"File has wrong format: '{sectionName}' section was already declared");

            _lastSectionName = sectionName;

            if (!_data.ContainsKey(sectionName))
                _data.Add(sectionName, new Dictionary<string, string>());
            
            if (!_data[sectionName].ContainsKey(fieldName))
                _data[sectionName].Add(fieldName, fieldValue);
            else
                throw new FormatException($"File has wrong format: '{fieldName}' field was already declared in '{sectionName}' section");
        }

        public void AssertNewSection (string sectionName)
        {
            if (_data.ContainsKey(sectionName))
                throw new FormatException($"File has wrong format: '{sectionName}' section was already declared");
        }

        public T TryGet<T>(string sectionName, string fieldName)
        {
            try
            {
                var valueString = _data[sectionName][fieldName];

                if (typeof(T) == typeof(double) || typeof(T) == typeof(decimal) || typeof(T) == typeof(float))
                    valueString = valueString.Replace('.', ',');

                return (T) Convert.ChangeType(valueString, typeof(T));
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException($"The given field '{fieldName}' was not present in the section '{sectionName}'");
            }
            catch (FormatException)
            {
                throw new FormatException($"The field '{fieldName}' value in the section '{sectionName}' cannot be converted to {typeof(T)}");
            }
        }

        public int TryGetInt(string sectionName, string fieldName) => TryGet<int>(sectionName, fieldName);
        public double TryGetDouble(string sectionName, string fieldName) => TryGet<double>(sectionName, fieldName);
        public string TryGetString(string sectionName, string fieldName) => _data[sectionName][fieldName];
    }
}