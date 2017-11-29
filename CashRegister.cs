using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

class CashRegister {

    static void Main(string[] args) {

        bool finished = false;
        bool finishedM = false;
        string input1;
        string input2;
        double totalCost = 0;

        List<string> alcoholItems = CSVListS(0, "alcohol.csv");
        List<double> alcoholPrice = CSVListD(1, "alcohol.csv");
        List<int> alcoholStock = CSVListI(2, "alcohol.csv");
        List<int> alcoholStockCopy = new List<int>(alcoholStock);

        List<string> beveragesItems = CSVListS(0, "beverages.csv");
        List<double> beveragesPrice = CSVListD(1, "beverages.csv");
        List<int> beveragesStock = CSVListI(2, "beverages.csv");
        List<int> beveragesStockCopy = new List<int>(beveragesStock);

        List<string> foodItems = CSVListS(0, "food.csv");
        List<double> foodPrice = CSVListD(1, "food.csv");
        List<int> foodStock = CSVListI(2, "food.csv");
        List<int> foodStockCopy = new List<int>(foodStock);

        List<string> snacksItems = CSVListS(0, "snacks.csv");
        List<double> snacksPrice = CSVListD(1, "snacks.csv");
        List<int> snacksStock = CSVListI(2, "snacks.csv");
        List<int> snacksStockCopy = new List<int>(snacksStock);


        //prompt user for input 1-4
        Console.WriteLine("Enter 1-4 for the types of items to purchase: ");
        Console.WriteLine("Aftwards enter the number of the item and the amount of items you want to buy\n");  
        while (finished == false) {
            Console.WriteLine("1) Alcohol\n2) Beverages\n3) Food\n4) Snacks\n5) Manager Functions\n6) Exit and Checkout");
            input1 = Console.ReadLine();

            if (input1 == "1") {
                
                Catalog(ref totalCost, ref alcoholItems, ref alcoholPrice, ref alcoholStock);
            }

            if (input1 == "2") {

                Catalog(ref totalCost, ref beveragesItems, ref beveragesPrice, ref beveragesStock);
            }

            if (input1 == "3") {

                Catalog(ref totalCost, ref foodItems, ref foodPrice, ref foodStock);
            }

            if (input1 == "4") {

                Catalog(ref totalCost, ref snacksItems, ref snacksPrice, ref snacksStock);
            }
            if (input1 == "5") {

                while (finishedM == false) {
                    Console.WriteLine("1) Empty Checkout\n2) Modify the Stock of an item\n3) Modify the price of an item\n4) Add an item\n5) Exit Manager Functions");
                    input2 = Console.ReadLine();
                    if (input2 != "1" && input2 != "2" && input2 != "3" && input2 != "4" && input2 != "5") {
                        Console.WriteLine("\nError: enter an integer 1 - 5.\n");
                        continue;
                    }
                    if (input2 == "1") {
                        alcoholStock = alcoholStockCopy;
                        beveragesStock = beveragesStockCopy;
                        foodStock = foodStockCopy;
                        snacksStock = snacksStockCopy;
                        totalCost = 0;
                        continue;
                    }
                    if (input2 == "5") {
                        finishedM = true;
                        continue;
                    }
                    Console.WriteLine("Enter the Catalog you want to modify");
                    Console.WriteLine("1) Alcohol\n2) Beverages\n3) Food\n4) Snacks");
                    input1 = Console.ReadLine();
                    if (input1 == "1") {
                    
                        Manager(ref totalCost, ref alcoholItems, ref alcoholPrice, ref alcoholStock, input2);
                    }

                    if (input1 == "2") {

                        Manager(ref totalCost, ref beveragesItems, ref beveragesPrice, ref beveragesStock, input2);
                    }

                    if (input1 == "3") {

                        Manager(ref totalCost, ref foodItems, ref foodPrice, ref foodStock, input2);
                    }

                    if (input1 == "4") {

                        Manager(ref totalCost, ref snacksItems, ref snacksPrice, ref snacksStock, input2);
                    }
                    if (input1 != "1" && input1 != "2" && input1 != "3" && input1 != "4") {

                        Console.WriteLine("\nError: enter an integer 1 - 4.\n");
                    }
                }
            }
            if (input1 == "6") {

                Console.WriteLine("\nThe total Cost is: {0}\n", totalCost);
                SaveFile(alcoholItems, alcoholPrice, alcoholStock, "alcohol.csv");
				SaveFile(beveragesItems, beveragesPrice, beveragesStock, "beverages.csv");
				SaveFile(foodItems, foodPrice, foodStock, "food.csv");
				SaveFile(snacksItems, snacksPrice, snacksStock, "snacks.csv");
                finished = true;
            }
            if (input1 != "1" && input1 != "2" && input1 != "3" && input1 != "4" && input1 != "5" && input1 != "6") {
                
                Console.WriteLine("\nError: enter an integer 1 - 5.\n");
            }
        }
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

                cost += amountOfItems * price[count - 1];
                stock[item - 1] -= amountOfItems;

                if (stock[item - 1] <= 0) {
                    stock[item - 1] = 0;
                }
            }
        }
    }
    public static void Manager(ref double cost,
                               ref List<string> items,
                               ref List<double> price,
                               ref List<int> stock,
                               string inputC) {
        bool success1 = true;
        bool success2 = true;
        bool success3 = true;
        string input;
        string input1;
        string input2;
        string input3;
        int item;
        int count = 1;

        double inputPrice;
        int inputStock;

        if (inputC == "1") {
            return;
        }
        if (inputC == "2") {
            Console.WriteLine("Which item's stock do you want to modify?");
            foreach (var i in items) {
                Console.WriteLine("{0}) {1}", count, i);
                count++;
            }
            Console.WriteLine();
            input = Console.ReadLine();
            success3 = Int32.TryParse(input, out item);
            if (success3 == false) {
                Console.WriteLine("Error: Enter an integer");
                return;
            }
            for (count = 1; count <= items.Count(); count++) {
                if (item == count) {
                    Console.WriteLine("Enter the amount of stock you want to item to have.\n");
                    input3 = Console.ReadLine();
                    input3 = input3.Trim();
                    success2 = Int32.TryParse(input3, out inputStock);

                    if (success1 == true) {
                        stock[item - 1] = inputStock; 
                    }
                    else {
                        Console.WriteLine("Error: Incorrect Input.");
                    }

                }
            }
            return;
        }
        if (inputC == "3") {
            Console.WriteLine("Which item's price do you want to modify?");
            foreach (var i in items) {
                Console.WriteLine("{0}) {1}", count, i);
                count++;
            }
            Console.WriteLine();
            input = Console.ReadLine();
            success3 = Int32.TryParse(input, out item);
            if (success3 == false) {
                Console.WriteLine("Error: Enter an integer");
                return;
            }
            for (count = 1; count <= items.Count(); count++) {
                if (item == count) {
                    Console.WriteLine("Enter the price you want to change the item to.\n");
                    input2 = Console.ReadLine();
                    input2 = input2.Trim();
                    success1 = Double.TryParse(input2, out inputPrice);

                    if (success1 == true) {
                        inputPrice = Math.Round(inputPrice, 2, MidpointRounding.AwayFromZero);
                        price[item - 1] = inputPrice; 
                    }
                    else {
                        Console.WriteLine("Error: Incorrect Input.");
                    }
                }
            }
            return;
        }
        if (inputC == "4") {

            Console.WriteLine("Enter the name of the item");
            input1 = Console.ReadLine();

            Console.WriteLine("Enter the price of the item");
            input2 = Console.ReadLine();

            Console.WriteLine("Enter the amount of stock the item has");
            input3 = Console.ReadLine();

            input1 = input1.Trim();
            input2 = input2.Trim();
            input3 = input3.Trim();
            success1 = Double.TryParse(input2, out inputPrice);
            success2 = Int32.TryParse(input3, out inputStock);

            if (success1 == true && success2 == true) {
                inputPrice = Math.Round(inputPrice, 2, MidpointRounding.AwayFromZero);
                items.Add(input1);
                price.Add(inputPrice);
                stock.Add(inputStock);
                return;
            }
            else {
                Console.WriteLine("Incorrect Input");
            }
        }
    }
    public static void SaveFile(List<string> item, List<double> price, List<int> stock, String fileLocation) {
		using (System.IO.StreamWriter file = new System.IO.StreamWriter(@fileLocation))
		{
			file.WriteLine("Item,Price,Stock");
			for(int i = 0; i < item.Count; i++) {
				file.WriteLine(item[i] + "," + price[i] + "," + stock[i]);
			}
		}
	}
}
