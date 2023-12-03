using AbstractFactory.Product;

namespace AbstractFactory.Factory
{
    internal interface IFactory
    {
        IProductA createProductA();

        IProductB createProductB();
    }
}