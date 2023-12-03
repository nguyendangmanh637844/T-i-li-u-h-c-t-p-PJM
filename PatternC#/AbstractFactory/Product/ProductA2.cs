using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory.Product
{
    internal class ProductA2 : IProductA
    {
        public void getName()
        {
            Console.WriteLine("ProductA2");
        }
    }
}
