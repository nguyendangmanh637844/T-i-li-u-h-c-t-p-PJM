namespace DemoDictionary
{
    public static class Extension
    {
        public static Dictionary<int, string> fruits = new Dictionary<int, string> { { 1, "apple" }, { 2, "banana" }, { 3, "mongo" } };

        public static string GetFruit(int fruit)
        {
            return fruits.ContainsKey(fruit) ? fruits[fruit] : "default";
        }
    }
}