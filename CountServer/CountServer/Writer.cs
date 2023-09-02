using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountServer
{
    internal class Writer : Customer
    {
        public Writer(string name) : base(name){}

        public void Write()
        {
            Console.WriteLine("{0} запрашивает возможность записать Count", Name);
        }
    }
}
