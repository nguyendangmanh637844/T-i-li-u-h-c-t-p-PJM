using AbstractFactory.Factory;
using AbstractFactory.FactoryManager;
using AbstractFactory.Enum;
using AbstractFactory.Product;

FactoryManager manager = FactoryManager.Instance;
IFactory factory1 = manager.getFactory(EnumFactoryType.TYPE1);
IProductA productA1 = factory1.createProductA();
IProductB productB1= factory1.createProductB();

IFactory factory2 = manager.getFactory(EnumFactoryType.TYPE2);
IProductA productA2 = factory2.createProductA();
IProductB productB2 = factory2.createProductB();

productA1.getName();
productA2.getName();
productB1.getName();
productB2.getName();