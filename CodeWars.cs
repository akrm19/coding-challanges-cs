using System;
using System.Collections;
using System.Linq;

namespace CrackingTheCodingInterviewProblems
{
    public class CodeWars
    {
        public CodeWars()
        {
        }


        public static int FindShortestWord(string wordInput)
        {
            var shortestWord = wordInput
                .Split(" ")
                .ToList()
                .OrderBy((word) => word.Length)
                .FirstOrDefault();

            return shortestWord.Length;
        }

        public static int FindShortestWord2(string wordInput)
        {
            return wordInput.Split(' ').Min(x => x.Length);
        }

        /// <summary>
        /// Finds the unique number from list.
        /// Problem:
        /// There is an array with some numbers. All numbers are equal except for one. Try to find it!
        /// It’s guaranteed that array contains more than 3 numbers.
        /// The tests contain some very huge arrays, so think about performance.
        /// </summary>
        /// <returns>The unique number from list.</returns>
        /// <param name="numbers">Numbers.</param>
        public static int findUniqueNumberFromList(int[] numbers)
        {
            var numberOccuranceCount = new Hashtable(); 

            foreach (var number in numbers)
            {
                numberOccuranceCount.ContainsKey(number);
            }

            return 0;// numbers.Distinct().First();
            //return numbers.Distinct().FirstOrDefault();
            //return 0;
            var index = 0;
            do
            {

                index++;

            } while (index < numbers.Length);
        }
    }
}
