using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SkyMovie.Model
{
    class Log
    {
        public static void Write(string text)
        {
            using (StreamWriter writer = new StreamWriter("debug.txt", true))
            {
                writer.WriteLine(text);
            }
        }
    }
}
