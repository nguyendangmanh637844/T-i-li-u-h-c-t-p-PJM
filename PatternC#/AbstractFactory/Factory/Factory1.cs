using AbstractFactory.Enum;
using AbstractFactory.Product;

namespace AbstractFactory.Factory
{
    internal class Factory1 : IFactory
    {
        public IProductA createProductA()
        {
            return new ProductA1();
        }

        public IProductB createProductB()
        {
            return new ProductB1();
        }
    }
}