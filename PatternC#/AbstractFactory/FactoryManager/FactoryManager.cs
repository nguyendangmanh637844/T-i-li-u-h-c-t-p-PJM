using AbstractFactory.Enum;
using AbstractFactory.Factory;

namespace AbstractFactory.FactoryManager
{
    internal sealed class FactoryManager
    {
        private static readonly FactoryManager _factoryManager = new FactoryManager();

        private FactoryManager()
        { }

        public static FactoryManager Instance
        { get { return _factoryManager; } }

        public IFactory getFactory(EnumFactoryType type)
        {
            switch (type)
            {
                case EnumFactoryType.TYPE1: return new Factory1();
                case EnumFactoryType.TYPE2: return new Factory2();
                default: return null;
            }
        }
    }
}