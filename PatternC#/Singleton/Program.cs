using Singleton;

internal class Program
{
    private static void Main(string[] args)
    {
        Singleton.Singleton singleton1 = Singleton.Singleton.Instance;
        Singleton.Singleton singleton2 = Singleton.Singleton.Instance;
        if (singleton1 == singleton2)
        {
            Console.WriteLine("true");
        }
        else { Console.WriteLine("false"); }
    }
}