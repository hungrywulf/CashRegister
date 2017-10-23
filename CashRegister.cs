using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace CashRegister
{
    class CashRegister {

        static void Main(string[] args) {

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

            // Testings
            foreach (var i in alcoholItems) {
                Console.WriteLine(i);
            }
            Console.WriteLine();
            foreach (var i in beveragesPrice) {
                Console.WriteLine(i);
            }
            Console.WriteLine();
            foreach (var i in foodStock) {
                Console.WriteLine(i);
            }
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
    }
}