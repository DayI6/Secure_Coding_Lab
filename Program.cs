using System;

namespace SampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sample C# app for CodeQL scanning");

            var db = new DatabaseHelper();

            Console.Write("Enter a username to search: ");
            string username = Console.ReadLine();

            // Vulnerable method call
            string result = db.GetUserByName_Insecure(username);
            Console.WriteLine("Insecure query result: " + result);

            // Safe method call
            string safeResult = db.GetUserByName_Secure(username);
            Console.WriteLine("Secure query result: " + safeResult);
        }
    }
}
