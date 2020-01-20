using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CrackingTheCodingInterviewProblems
{
    public static class Sec1_v2
    {
        /// <summary>
        ///  Implement an algorithm to determine if a string has all unique
        ///  characters. What if you cannot use additional data structures?
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool s1_1_IsUnique(string s)
        {
            //If ASCII & greater than 128, there is a double character
            if (s.Length > 128) return false;

            var hash = new Hashtable();

            foreach(char c in s)
            {
                if (hash.ContainsKey(c))
                    return false;

                hash.Add(c, 0);
            }

            return true;
        }

        public static bool s1_2_CheckPermutation(string s1, string s2)
        {
            if (s1.Length != s2.Length) return false;

            var charCounter = new int[256];

            foreach (char c in s1)
                charCounter[c]++;

            foreach(char c in s2)
            {
                charCounter[c]--;
                if (charCounter[c] < 0)
                    return false;
            }

            return true;
        }

        public static void s1_2_CheckPermutation_Test()
        {
            var s1 = "qwerty";
            var s2 = "ytrewq";

            var result = Sec1_v2.s1_2_CheckPermutation(s1, s2);
            Console.WriteLine($"Result: {result}");
        }

        public static string s1_3_URLify(char[] s, int sLength)
        {

            return "";
        }

        public static void Test()
        {
            var result = s1_4_IsPalindromePermutation_v2("tactc oapapa");
            Console.WriteLine($"Result: {result}");
        }

        public static bool s1_4_IsPalindromePermutation(string s)
        {
            var updatedS = s.ToLower().Replace(" ", "");
            var oddNumsAllowed = updatedS.Length % 2 == 0
                ? 0
                : 1;

            var charCounter = new int[256];
            foreach (var c in updatedS)
                charCounter[c]++;

            foreach (int count in charCounter)
            {
                if (count % 2 != 0)
                {
                    oddNumsAllowed--;
                    if (oddNumsAllowed < 0)
                        return false;
                }
            }

            return true;
        }

        public static bool s1_4_IsPalindromePermutation_v2(string s)
        {
            var updatedS = s.ToLower().Replace(" ", "");
            var oddNumsAllowed = updatedS.Length % 2 == 0
                ? 0
                : 1;

            var oddCount = 0;
            var charCounter = new int[256];

            foreach (var c in updatedS)
            {
                if (++charCounter[c] % 2 == 0)
                    oddCount--;
                else
                    oddCount++;
            }
            return oddCount <= oddNumsAllowed;
        }

        public static void s1_5_OneWay_Test()
        {
            var inputs = new string[,]
            {
                {"pale", "ple" },
                {"pales", "pale" },
                {"pale", "bale" },
                {"pale", "bae" }
            };

            int length = inputs.Length / 2;
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine($"Current: {i} of {length} ");
                var result = s1_5_OneWay(inputs[i, 0], inputs[i, 1]);
                Console.WriteLine($"Result for {inputs[i, 0]} & {inputs[i, 1]}: {result}");
            }
        }

        public static bool s1_5_OneWay(string orig, string edit)
        {
            var lengthDiff = Math.Abs(orig.Length - edit.Length);
            if (lengthDiff > 1) return false;

            int numOfDiffs = 0;

            //If same lenght compare compare items one by one
            if(lengthDiff == 0)
            {
                for (int i = 0; i < orig.Length; i++)
                {
                    //Add to numOfDiffs if diff
                    if(orig[i] != edit[i])
                    {
                        //If numOfDiffs is greater than 1 return false
                        if (++numOfDiffs > 1)
                            return false;
                    }
                }
            }
            else
            {
                //If not same length, compare using shortest
                var longest = orig.Length > edit.Length ? orig : edit;
                var shortest = orig.Length > edit.Length ? edit : orig;
                var foundOffset = false;
                int lIdx = 0;

                for(int sIdx = 0; sIdx < shortest.Length; sIdx++)
                {
                    if (shortest[sIdx] != longest[lIdx])
                    {
                        if (foundOffset) return false;

                        foundOffset = true;
                        lIdx++;
                    }
                    
                    lIdx++; 
                }
            }

            return true;
        }

        public static void S1_6_StringCompressions_Test()
        {
            var input = "aabcccccaaa";
            var result = S1_6_StringCompressions(input);
            Console.WriteLine($"Input: {input}. Result: {result}");
        }

        public static string S1_6_StringCompressions(string s)
        {
            if (string.IsNullOrWhiteSpace(s) || s.Length < 2)
                return s;

            //keep track of all chars are > 1
            bool countPassed = false;
            //keep track of last char
            char lastChar = s[0];
            var charCount = 1;

            var results = new StringBuilder();

            //iterate through string
            for(int i = 1; i < s.Length; i++)
            {
                //if current char equal to last, add to count
                if (s[i] == lastChar)
                    charCount++;
                //else add last char and count
                else
                {
                    results.Append($"{lastChar}{charCount}");
                    if (!countPassed && charCount > 1)
                        countPassed = true;

                    //reset count back to 1
                    lastChar = s[i];
                    charCount = 1;
                }
            }

            //else add last char and count to String Builder
            return !countPassed || charCount < 2
                ? s
                : results.Append($"{lastChar}{charCount}").ToString();
        }

        // TODO
        public static void S1_7_RotateMatrix_Test()
        {
            var input = new int[,] {
                { 1, 2, 3},
                { 4, 5, 6},
                { 7, 8, 9}
            };
            input.GetLength(0);
            var length = input.GetLength(0);// input.Length / 3;
            Console.WriteLine($"Length of array is {length}");
            for (int r = 0; r < length; r++)
            {
                for (int c = 0; c <= input.GetUpperBound(0); c++)
                {
                    Console.Write($" {input[r, c]}");
                }
                Console.WriteLine("");
            }
        }

        public static void S1_8_ZeroMatrix(int [,] matrix)
        {
            var zeroRows = new HashSet<int>();
            var zeroCols = new HashSet<int>();

            //iterate through each row
            //if val is 0, add to list of columns to set to 
            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                //iterate through each column
                //if column is 0, add to list to set to 0
                for (int c = 0; c <= matrix.GetUpperBound(0); r++)
                {
                    if (matrix[r, c] == 0)
                    {
                        zeroRows.Add(r);
                        zeroCols.Add(c);
                    }
                }
            }

            //for each column in list set to 0

            //for each row in list set to 0
        }

        public static bool IsSubstring(string s1, string s2)
        {
            return false;
        }
    }
}
