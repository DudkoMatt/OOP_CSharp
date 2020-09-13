using System;
using System.Collections.Generic;
using System.IO;

namespace Lab_1
{
    class Program
    {
        public static void Main()
        {
            try
            {
                var data = IniParser.Parse("file.ini");

                // Ok
                /*
                Console.WriteLine(data.TryGet<int>("COMMON", "StatisterTimeMs", out _));
                Console.WriteLine(data.TryGet<double>("COMMON", "StatisterTimeMs", out _));
                Console.WriteLine(data.TryGet<string>("COMMON", "StatisterTimeMs", out _));
                Console.WriteLine(data.TryGetInt("COMMON", "StatisterTimeMs", out _));
                Console.WriteLine(data.TryGetDouble("COMMON", "StatisterTimeMs", out _));
                Console.WriteLine(data.TryGetString("COMMON", "StatisterTimeMs", out _));
                
                Console.WriteLine(data.TryGet<string>("COMMON", "DiskCachePath", out _));
                */
                
                Console.WriteLine(data.TryGet<string>("NCMD", "SampleRate", out var v));
                Console.WriteLine(v);
                Console.WriteLine(data.TryGet<string>("COMMON", "DiskCachePath", out var v1));
                Console.WriteLine(v1);
                
                // Error
                Console.WriteLine(data.TryGet<double>("COMMON", "SampleRate", out var v2));
                Console.WriteLine(v2);
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