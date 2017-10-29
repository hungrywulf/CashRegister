using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

class CashRegister {

    static void Main(string[] args) {

        bool finished = false;
        string input;
        double totalCost = 0; // amount customer cwes

        List<string> alcoholItems = CSVListS(0, "alcohol.csv");
        List<double> alcoholPrice = CSVListD(1, "alcohol.csv");
        List<int> alcoholStock = CSVListI(2, "alcohol.csv");

        List<string> beveragesItems = CSVListS(0, "beverages.csv");
        List<double> beveragesPrice = CSVListD(1, "beverages.csv");
        List<int> beveragesStock = CSVListI(2, "beverages.csv");

        List<string> foodItems = CSVListS(0, "food.csv");
        List<double> foodPrice = CSVListD(1, "food.csv");
        List<int> foodStock = CSVListI(2, "food.csv");

        List<string> snacksItems = CSVListS(0, "snacks.csv");
        List<double> snacksPrice = CSVListD(1, "snacks.csv");
        List<int> snacksStock = CSVListI(2, "snacks.csv");


        //prompt user for input 1-4
        Console.WriteLine("Enter 1-4 for the types of items to purchase: ");
        Console.WriteLine("Aftwards enter the number of the item and the amount of items you want to buy\n");  
        while (finished == false) {
            Console.WriteLine("1) Alcohol\n2) Beverages\n3) Food\n4) Snacks\n5) Exit and Checkout");
            input = Console.ReadLine();

            if (input == "1") {
                
                Catalog(ref totalCost, ref alcoholItems, ref alcoholPrice, ref alcoholStock);
            }

            if (input == "2") {

                Catalog(ref totalCost, ref beveragesItems, ref beveragesPrice, ref beveragesStock);
            }

            if (input == "3") {

                Catalog(ref totalCost, ref foodItems, ref foodPrice, ref foodStock);
            }

            if (input == "4") {

                Catalog(ref totalCost, ref snacksItems, ref snacksPrice, ref snacksStock);
            }
            if (input == "5") {

                Console.WriteLine("\nThe total Cost is: {0}\n", totalCost);
                finished = true;
            }
            if (input != "1" && input != "2" && input != "3" && input != "4" && input != "5") {
                
                Console.WriteLine("\nError: enter an integer 1 - 5.\n");
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

    public static List<int> CSVListI (int column, string fileLocation) {
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

    public static List<double> CSVListD (int column, string fileLocation) {
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

    public static List<string> CSVListS (int column, string fileLocation) {
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
    // references 3 lists and a cost value
    public static void Catalog(ref double cost,
                               ref List<string> items,
                               ref List<double> price,
                               ref List<int> stock) {
        bool success1 = true;
        bool success2 = true;
        int count = 1;
        int item;
        int amountOfItems;
        string input;
        string [] option = new string[2];

        foreach (var i in items) {
            Console.WriteLine("{0}) {1}", count, i);
            count++;
        }
        Console.WriteLine();
        input = Console.ReadLine();

        // option[0] is the item
        // option[1] is the amount of items bought
        option = input.Split(' ');

        if (option.Length == 2) {
            success1 = Int32.TryParse(option[0], out item);
            success2 = Int32.TryParse(option[1], out amountOfItems);
        } else if (option.Length == 1) {
            success1 = Int32.TryParse(option[0], out item);
            amountOfItems = 1;
        } else {
            Console.WriteLine("\nError: Enter 1 or 2 integers\n");
            return;
        }

        if (success1 == false || success2 == false) {
            Console.WriteLine("\nError: Enter 1 or 2 integers\n");
            return;
        }

        for (count = 1; count <= items.Count(); count++) {

            if (item == count) {

                cost += Cost(amountOfItems, price[count - 1]);
                stock[item - 1] -= amountOfItems;

                if (stock[item - 1] <= 0) {
                    stock[item - 1] = 0;
                }
            }
        }
    }
}
