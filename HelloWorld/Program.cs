using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter your name: ");
        string name = Console.ReadLine!() ?? "Stranger";
        Console.WriteLine("Hello, " + name + "!");
    }
}
