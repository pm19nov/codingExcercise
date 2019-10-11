using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            PerformIntersectAndSort();          // Question 1
            PerformSortOnLastNames();           // Question 2
        }

        private static void PerformSortOnLastNames()
        {
            // sample input
            var names = new List<string>()
            {
                "John Locke",
                "Thomas Aquinas",
                "David Hume",
                "Rene Descartes"
            };

            var sortedNames = SortNames(names);
            Console.Write($"Program 2 ------ \n");
            foreach (var sortedName in sortedNames)
            {
                Console.WriteLine($"{sortedName}");
            }
        }

        private static void PerformIntersectAndSort()
        {
            var input1 = "board";
            var input2 = "bored";
            var outputAns = IntersectAndSort(input1, input2);
            Console.WriteLine($"Program 1 ------ \n{outputAns}\n\n");
        }

        private static string IntersectAndSort(string inputWord1, string inputWord2)
        {
            string answer = "";
            if (inputWord1.Length < 1 || inputWord2.Length < 1)
                return answer;
            
            var chararr1 = inputWord1.ToCharArray(0, inputWord1.Length);
            var chararr2 = inputWord2.ToCharArray(0, inputWord2.Length);
            answer = new string(chararr1.Intersect(chararr2).OrderBy(x => x).ToArray());
            
            return answer;
        }

        private static List<string> SortNames(List<string> names)
        {
            var sortedNames = new List<string>();
            if(names == null || names.Count < 1)
                return sortedNames;

            var lastNameToFirstNameLookup = new Dictionary<string, HashSet<string>>();
            var lastNames = new List<string>();
            foreach (var name in names)
            {
                var split = name.Split(' ');
                var firstName = split[0];
                var lastName = split[1];

                lastNames.Add(lastName);        // only use the last name to sort so keep track

                if (lastNameToFirstNameLookup.ContainsKey(lastName))
                    lastNameToFirstNameLookup[lastName].Add(firstName); // easy lookup after sorting last names O(1) retrieval
                else
                {
                    lastNameToFirstNameLookup.Add(lastName, new HashSet<string>() { firstName });
                }
            }

            var sortedLastNames = SortUsingPrimitiveLoop(lastNames);     // sorting last names

            foreach (var lastName in sortedLastNames)   
            {
                var firstNames = lastNameToFirstNameLookup[lastName];       // lookup to track first name
                foreach (var firstName in firstNames)
                {
                    var fullName = $"{firstName} {lastName}";
                    sortedNames.Add(fullName);
                }

            }

            return sortedNames;
        }

        // primitive sorting technique O(n^2), comparison, temp space
        private static List<string> SortUsingPrimitiveLoop(List<string> lastNames)
        {
            var lastNameArray = lastNames.ToArray();

            string temp = "";
            for (int i = 0; i < lastNameArray.Length; i++)
            {
                for (int j = 0; j < lastNameArray.Length - 1; j++)
                {
                    if (String.Compare(lastNameArray[j], lastNameArray[j + 1], StringComparison.CurrentCultureIgnoreCase) > 0)
                    {
                        temp = lastNameArray[j];
                        lastNameArray[j] = lastNameArray[j + 1];
                        lastNameArray[j + 1] = temp;
                    }
                }
            }

            return lastNameArray.ToList();
        }
    }
}
