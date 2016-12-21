using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TgaLib;

namespace TgaInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var fs = new FileStream(args[0], FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var reader = new BinaryReader(fs))
            {
                var tga = new TgaImage(reader);
                Console.WriteLine("{0}", tga);
            }
        }
    }
}
