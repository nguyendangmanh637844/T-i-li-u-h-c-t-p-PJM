namespace Singleton
{
    public sealed class Singleton
    {
        private static readonly Singleton _singleton = new Singleton();

        private Singleton()
        { }

        public static Singleton Instance
        { get { return _singleton; } }
    }
}