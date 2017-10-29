using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

class CashRegister {

    static void Main(string[] args) {

        bool finished = false;
        string input;
        double totalCost = 0; // amount customer cwes

        List<string> alcoholItems = csvToArrayS(0, "alcohol.csv");
        List<double> alcoholPrice = csvToArrayD(1, "alcohol.csv");
        List<int> alcoholStock = csvToArrayI(2, "alcohol.csv");

        List<string> beveragesItems = csvToArrayS(0, "beverages.csv");
        List<double> beveragesPrice = csvToArrayD(1, "beverages.csv");
        List<int> beveragesStock = csvToArrayI(2, "beverages.csv");

        List<string> foodItems = csvToArrayS(0, "food.csv");
        List<double> foodPrice = csvToArrayD(1, "food.csv");
        List<int> foodStock = csvToArrayI(2, "food.csv");

        List<string> snacksItems = csvToArrayS(0, "snacks.csv");
        List<double> snacksPrice = csvToArrayD(1, "snacks.csv");
        List<int> snacksStock = csvToArrayI(2, "snacks.csv");


        //prompt user for input 1-4
        Console.WriteLine("Enter 1-4 for the types of items to purchase: ");
        Console.WriteLine("Aftwards enter the number of the item and the amount of items you want to buy\n");  
        while (finished == false) {
            Console.WriteLine("1) Alcohol");
            Console.WriteLine("2) Beverages");
            Console.WriteLine("3) Food");
            Console.WriteLine("4) Snacks");
            Console.WriteLine("5) Exit and Checkout");
            input = Console.ReadLine();

            if (input == "1") {
                Console.WriteLine(alcoholStock[0]);
                catalog(ref totalCost, ref alcoholItems, ref alcoholPrice, ref alcoholStock);
                Console.WriteLine(alcoholStock[0]);
            }

            if (input == "2") {

                catalog(ref totalCost, ref beveragesItems, ref beveragesPrice, ref beveragesStock);
            }

            if (input == "3") {

                catalog(ref totalCost, ref foodItems, ref foodPrice, ref foodStock);
            }

            if (input == "4") {

                catalog(ref totalCost, ref snacksItems, ref snacksPrice, ref snacksStock);
            }
            if (input == "5") {
                Console.WriteLine("The total Cost is: {0}", totalCost);
                finished = true;
            }
            if (input != "1" && input != "2" && input != "3" && input != "4" && input != "5") {
                Console.WriteLine("\nError incorrect input\n");
            }
        }
    }

    public static double Cost(double price)
    {
        return price;
    }

    public static double Cost(int amountOfItems, double price)
    {
        return amountOfItems * price;
    }

    public static List<int> csvToArrayI (int column, string fileLocation) {
        string line;
        int parsing;
        List<int> items = new List<int>();

        using (StreamReader file = new StreamReader(fileLocation)) {
            while ((line = file.ReadLine()) != null) {

                string[] words = line.Split(',');
                Int32.TryParse(words[column], out parsing);
                items.Add(parsing);
            }
            items.RemoveAt(0);
            return items;
        }
    }

    public static List<double> csvToArrayD (int column, string fileLocation) {
        string line;
        double parsing;
        List<double> items = new List<double>();

        using (StreamReader file = new StreamReader(fileLocation)) {
            while ((line = file.ReadLine()) != null) {

                string[] words = line.Split(',');
                Double.TryParse(words[column], out parsing);
                items.Add(parsing);
            }
            items.RemoveAt(0);
            return items;
        }
    }

    public static List<string> csvToArrayS (int column, string fileLocation) {
        string line;
        List<string> items = new List<string>();

        using (StreamReader file = new StreamReader(fileLocation)) {
            while ((line = file.ReadLine()) != null) {

                string[] words = line.Split(',');
                items.Add(words[column]);
            }
            items.RemoveAt(0);
            return items;
        }
    }

    public static void catalog(ref double cost,
                               ref List<string> items,
                               ref List<double> price,
                               ref List<int> stock) {
        int count = 1;
        string input2;
        string [] option = new string[2];

        foreach (var i in items) {
            Console.WriteLine("{0}) {1}", count, i);
            count++;
        }
        Console.WriteLine();
        input2 = Console.ReadLine();
        option = input2.Split(' ');

        for (count = 1; count <= items.Count(); count++) {

            if (option[0] == count.ToString()) {

                if (Int32.Parse(option[0]) == count) {
                                
                    if (option.Length == 2) {
                        int amountOfItems;
                        Int32.TryParse(option[1], out amountOfItems);
                        cost += Cost(amountOfItems, price[count - 1]);
                        stock[Int32.Parse(option[0]) - 1] -= amountOfItems;
                    } else if (option.Length == 1) {
                        cost += Cost(price[count - 1]);
                        stock[Int32.Parse(option[0]) - 1]--;
                    } else {
                        Console.WriteLine("\nError in input is too long or short\n");
                    }
                }
            }
        }
    }
}
