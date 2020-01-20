using System;
using System.Collections.Generic;

namespace CrackingTheCodingInterviewProblems.Common
{
    public static class SearchAlgorithms
    {

        public static string SampleInput = "ABAAABCDBBABCDDEBCABC"; 
        public static string SamplePattern = "ABC";

        #region RabinKarpSearch
        public static void RabinKarpSearchTest()
        {
            RabinKarpSearchTest(SampleInput, SamplePattern);
        }

        public static void RabinKarpSearchTest(string input, string pattern)
        {
            Console.WriteLine($"Running RabinKarpSearchTest! Input:{input} Pattern:{pattern}");

            var matches = RabinKarpSearch(SampleInput, SamplePattern);

            foreach (var matchIndex in matches)
            {
                Console.WriteLine($"Found match at index {matchIndex}");
            }
        }

        // https://riptutorial.com/algorithm/example/24653/introduction-to-rabin-karp-algorithm  
        // https://medium.com/@harsha444/rabin-karp-algorithm-for-pattern-searching-explained-with-example-7dcdfa6b1c64
        public static List<int> RabinKarpSearch(string input, string pattern)
        {
            int primeNumber = 3;
            int patternLength = pattern.Length;
            int patternCombinedHash = GetSectionHash(pattern, 0, patternLength, primeNumber);

            var indexWithMatchesFound = new List<int>();
            int currentSectionHash = 0;

            for(int i = 0; i <= input.Length - patternLength; i++)
            {
                currentSectionHash = i == 0
                    ? GetSectionHash(input, i, patternLength, primeNumber)
                    : GetNextSectionHashUsingRollingHash(input, i, currentSectionHash, patternLength, primeNumber);

                if (currentSectionHash == patternCombinedHash)
                {
                    if(pattern.Equals(input.Substring(i,patternLength)))
                        indexWithMatchesFound.Add(i);
                }
            }

            return indexWithMatchesFound;
        }

        private static int GetSectionHash(string input, int currentIndex, int patternLength, int primeNum)
        {
            var substring = input.Substring(currentIndex, patternLength);
            Console.WriteLine($"Processing substring {substring}");
            var totalSum = 0;

            for(int i = 0; i < patternLength; i++)
            {
                var currentLetter = input[currentIndex + i];
                int currentLetterHashVal = (int)currentLetter * (int)Math.Pow(primeNum, i);
                Console.WriteLine($"HashValue for {currentLetter} ({(int)currentLetter}) is {currentLetterHashVal}");

                totalSum += currentLetterHashVal;
            }

            Console.WriteLine($"TotalSum for {substring} is {totalSum}");
            return totalSum;
        }


        private static int GetNextSectionHashUsingRollingHash(string input, int currentIndex, int previousSectionHashVal, int patternLength, int primeNum)
        {
            var substring = input.Substring(currentIndex, patternLength);
            Console.WriteLine($"Processing substring {substring}");

            var oldFirstLetter = input[currentIndex - 1];
            var lastLetter = input[(currentIndex - 1) + patternLength];
            int newLastLetterValue = lastLetter * (int)Math.Pow(primeNum, patternLength);

            var tempSectionHash = (previousSectionHashVal - oldFirstLetter) + newLastLetterValue;
            var sectionHash = tempSectionHash / primeNum;

            Console.WriteLine($"Old first letter {oldFirstLetter}. New Last letter {lastLetter} and its Hash val ({newLastLetterValue})");

            return sectionHash;
        }
        #endregion RabinKarpSearch

        #region BoyerMoore Type 1

        public static void BoyerMooreSearchTest()
        {
            Console.WriteLine($"Looking for pattern {SamplePattern} in input: {SampleInput}");
            var results = BoyerMooreSearch(SamplePattern, SampleInput);
            Console.WriteLine("Found matches at indexes:");
            results.ToStringConsole();
        }

        public static List<int> BoyerMooreSearch(string pattern, string toSearch)
        {
            if (string.IsNullOrWhiteSpace(pattern) || string.IsNullOrWhiteSpace(toSearch) || pattern.Length > toSearch.Length)
                return null;

            var matchIndexesFound = new List<int>();
            var badMatchTbl = new BadMatchTable(pattern);
            var currentIdx = 0;

            while(currentIdx <= toSearch.Length - pattern.Length)
            {
                int charsLeftToMatch = pattern.Length - 1;//reset & start from right
                while (charsLeftToMatch >= 0 && (pattern[charsLeftToMatch] == toSearch[currentIdx + charsLeftToMatch]))
                    charsLeftToMatch--;

                if (charsLeftToMatch < 0) //match found
                {
                    matchIndexesFound.Add(currentIdx);
                    currentIdx += pattern.Length;
                }
                else
                    currentIdx += badMatchTbl[toSearch[currentIdx + pattern.Length - 1]];
            }

            return matchIndexesFound;
        }

        private class BadMatchTable
        {
            private readonly int _defaultValue;
            private readonly Dictionary<int, int> _distanceToSkip;

            public BadMatchTable(string pattern)
            {
                _defaultValue = pattern.Length;
                _distanceToSkip = new Dictionary<int, int>();

                //last one is treated as defaultVal
                for (int i = 0; i < pattern.Length - 1; i++)
                    _distanceToSkip[pattern[i]] = pattern.Length - i - 1;
            }

            public int this[int index]
            {
                get
                {
                    return _distanceToSkip.ContainsKey(index)
                        ? _distanceToSkip[index]
                        : _defaultValue;
                }
            }
        }

        #endregion

        #region BoyerMoore Unfinished
        private static bool BoyerMooreUnfisnished(string pattern, string input)
        {
            var patternLenght = pattern.Length;
            var inputLenght = input.Length;
            var i = patternLenght - 1;

            while (i < inputLenght)
            {
                switch (input[i])
                {

                }
            }

            return true;
        }


        private static bool BoyerMoore_StaticExample(string value)
        {
            // Searches for 4-letter constant string using Boyer-Moore style algorithm.
            // ... Uses switch as lookup table.
            int i = 3; // First index to check.
            int length = value.Length;
            while (i < length)
            {
                switch (value[i])
                {
                    case 'D':
                        // Last character in pattern found.
                        // ... Check for definite match.
                        if (value[i - 1] == 'C' &&
                            value[i - 2] == 'B' &&
                            value[i - 3] == 'A')
                        {
                            return true;
                        }
                        // Must be at least 4 characters away.
                        i += 4;
                        continue;
                    case 'C':
                        // Must be at least 1 character away.
                        i += 1;
                        continue;
                    case 'B':
                        // Must be at least 2 characters away.
                        i += 2;
                        continue;
                    case 'A':
                        // Must be at least 3 characters away.
                        i += 3;
                        continue;
                    default:
                        // Must be at least 4 characters away.
                        i += 4;
                        continue;
                }
            }
            // Nothing found.
            return false;
        }

        #endregion

        #region Find Gcd
        public static void FindGdcTest()
        {
            Console.WriteLine("Input: \n");
            var input = new List<int> { 15, 9, 12, 3, 30, 21, 2 };
            input.ToStringConsole();
            var gcd = FindGcd(input);
            Console.WriteLine($"\nGCD: {gcd}");
        }


        /// <summary>
        /// Efficient method to find the gcd.
        /// </summary>
        /// <returns>The gcd.</returns>
        /// <param name="n1">N1.</param>
        /// <param name="n2">N2.</param>
        public static int FindGcd(int n1, int n2)
        {
            while(n1 != 0 && n2 != 0)
            {
                if (n1 > n2)
                    n1 %= n2;
                else
                    n2 %= n1;
            }

            return n1 == 0 ? n2 : n1;
        }

        /// <summary>
        /// Finds the gcd.
        /// Inefficient method
        /// </summary>
        /// <returns>The gcd.</returns>
        /// <param name="nums">Nums.</param>
        public static int FindGcd(List<int> nums)
        {
            var min = int.MaxValue;
            foreach (var n in nums)
                if (n < min)
                    min = n;

            var queue = new Queue<int>();
            queue.Enqueue(min);
            for(int i = min/2; i > 0; i--)
            {
                if (min % i == 0)
                    queue.Enqueue(i);
            }

            while(queue.Count > 0)
            {
                var potentialGcd = queue.Dequeue();
                bool isGcd = true;

                foreach(var num in nums)
                {
                    if(num % potentialGcd != 0)
                    {
                        isGcd = false;
                        break;
                    }
                }

                if (isGcd)
                    return potentialGcd;
            }

            return -1;
        }
        #endregion FindGcd
    }
}
