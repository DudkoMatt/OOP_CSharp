using System;
using System.Collections.Generic;
using System.IO;

namespace Lab_1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var data = IniParser.Parse("file.ini");

                // Ok
                /*
                Console.WriteLine(data.TryGet<int>("COMMON", "StatisterTimeMs"));
                Console.WriteLine(data.TryGet<double>("COMMON", "StatisterTimeMs"));
                Console.WriteLine(data.TryGet<string>("COMMON", "StatisterTimeMs"));
                Console.WriteLine(data.TryGetInt("COMMON", "StatisterTimeMs"));
                Console.WriteLine(data.TryGetDouble("COMMON", "StatisterTimeMs"));
                Console.WriteLine(data.TryGetString("COMMON", "StatisterTimeMs"));
                
                Console.WriteLine(data.TryGet<string>("COMMON", "DiskCachePath"));
                */

                Console.WriteLine(data.TryGet<string>("NCMD", "SampleRate"));
                
                // Error
                // Console.WriteLine(data.TryGet<int>("COMMON", "DiskCachePath"));
                // Console.WriteLine(data.TryGet<double>("COMMON", "SampleRate"));

            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}