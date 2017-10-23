using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace CashRegister
{
    class CashRegister {

        static void Main(string[] args) {

            List<string> items = csvToArrayS(0, "test.csv");
            List<double> price = csvToArrayD(1, "test.csv");
            List<int> stock = csvToArrayI(2, "test.csv");

            foreach (var i in items) {
                Console.WriteLine(i);
            }
            Console.WriteLine();
            foreach (var i in price) {
                Console.WriteLine(i);
            }
            Console.WriteLine();
            foreach (var i in stock) {
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