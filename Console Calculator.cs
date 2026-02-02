using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== MENU ===");
        Console.WriteLine("1. Add (+)");
        Console.WriteLine("2. Subtract (-)");
        Console.WriteLine("3. Divide (/)");
        Console.WriteLine("4. Average");
        Console.WriteLine("5. Tax");
        Console.WriteLine("6. Exit");
        Console.Write("Enter your choice: ");

        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                Console.Write("Enter first number: ");
                double a1 = double.Parse(Console.ReadLine());

                Console.Write("Enter second number: ");
                double b1 = double.Parse(Console.ReadLine());

                Console.WriteLine("Result: " + (a1 + b1));
                break;

            case 2:
                Console.Write("Enter first number: ");
                double a2 = double.Parse(Console.ReadLine());

                Console.Write("Enter second number: ");
                double b2 = double.Parse(Console.ReadLine());

                Console.WriteLine("Result: " + (a2 - b2));
                break;

            case 3:
                Console.Write("Enter first number: ");
                double a3 = double.Parse(Console.ReadLine());

                Console.Write("Enter second number: ");
                double b3 = double.Parse(Console.ReadLine());

                if (b3 != 0)
                    Console.WriteLine("Result: " + (a3 / b3));
                else
                    Console.WriteLine("Cannot divide by zero.");
                break;

            case 4:
                Console.Write("Enter first number: ");
                double a4 = double.Parse(Console.ReadLine());

                Console.Write("Enter second number: ");
                double b4 = double.Parse(Console.ReadLine());

                Console.WriteLine("Average: " + ((a4 + b4) / 2));
                break;

            case 5:
                const decimal TAX_RATE = 0.055m; // 5.5%

                Console.Write("Enter amount: ");
                decimal amount = decimal.Parse(Console.ReadLine());

                decimal tax = amount * TAX_RATE;

                Console.WriteLine("Tax: $" + tax.ToString("0.00"));
                break;


            case 6:
                Console.WriteLine("Goodbye!");
                break;

            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }
}

