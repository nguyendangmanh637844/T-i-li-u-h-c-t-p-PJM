using Factorry.Object;

namespace Factorry
{
    internal sealed class Factory
    {
        private static readonly Factorry.Factory _instance = new Factory();

        private Factory()
        { }

        public static Factorry.Factory Instance
        { get { return _instance; } }

        public IObject GetObject(EnumObject enumObject)
        {
            switch (enumObject)
            {
                case EnumObject.OBJECT1: return new Object1();
                case EnumObject.OBJECT2: return new Object2();
                default: return null;
            }
        }
    }
}