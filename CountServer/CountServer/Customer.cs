using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountServer
{
    internal class Customer
    {
        public string Name { get; }
        public Customer(string name) {
            Name = name;
        }

        public void Read() {
            Console.WriteLine("{0} запрашивает доступ к Count", Name);
        }
    }
}
