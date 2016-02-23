using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LaLigaEstupenda
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"Log files\", @"Excepciones.log");
            Console.WriteLine("Exception log can be found at: ");
            Console.WriteLine(path);
            Console.ReadLine();
        }

    }
}
