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

        public bool TryGet<T>(string sectionName, string fieldName, out T result)
        {
            result = default;
            
            try
            {
                var valueString = _data[sectionName][fieldName];

                // ToDO: Локализация
                if (typeof(T) == typeof(double) || typeof(T) == typeof(decimal) || typeof(T) == typeof(float))
                    valueString = valueString.Replace('.', ',');

                result = (T) Convert.ChangeType(valueString, typeof(T));

                // Conversion examples
                
                // object v = "Hello";
                // object d = "12.1";
                // var s = v as string;
                // var z = d as string;
                //
                // switch (d)
                // {
                //     case double y:
                //         Console.WriteLine("");
                //         break;
                // }
                //
                // if (d is double x)
                // {
                //     
                // }
                // else
                // {
                //     
                // }
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
            catch (FormatException)
            {
                return false;
            }
            catch (InvalidCastException)
            {
                return false;
            }
            
            return true;
        }

        public bool TryGetInt(string sectionName, string fieldName, out int result) => TryGet(sectionName, fieldName, out result);
        public bool TryGetDouble(string sectionName, string fieldName, out double result) => TryGet(sectionName, fieldName, out result);
        public bool TryGetString(string sectionName, string fieldName, out string result)
        {
            result = "";
            if (!_data[sectionName].ContainsKey(fieldName)) return false;
            result = _data[sectionName][fieldName];
            return true;

        }

        /*
        public T Get<T>(string sectionName, string fieldName)
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
        
        public int GetInt(string sectionName, string fieldName) => Get<int>(sectionName, fieldName);
        public double GetDouble(string sectionName, string fieldName) => Get<double>(sectionName, fieldName);
        public string GetString(string sectionName, string fieldName) => _data[sectionName][fieldName];
        */
    }
}