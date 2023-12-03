using Factorry.Object;

Factorry.Factory factory = Factorry.Factory.Instance;
IObject obj1 = factory.GetObject(Factorry.EnumObject.OBJECT1);
obj1.sampleMethod();
IObject obj2 = factory.GetObject(Factorry.EnumObject.OBJECT2);
obj2.sampleMethod();