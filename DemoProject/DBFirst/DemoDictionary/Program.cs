// See https://aka.ms/new-console-template for more information
var a = 9;
Dictionary<int, string> fruits = new Dictionary<int, string> { { 1, "apple" }, { 2, "banana" }, { 3, "mongo" } };

Console.WriteLine(fruits.ContainsKey(a) ? fruits[a] : "default");