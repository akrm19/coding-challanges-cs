using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CrackingTheCodingInterviewProblems
{
    public static class Sec1_ArraysStrings
    {
        public static string SampleInput_UniqueCharacter = "qwertysda";
        public static string SampleInput_NonUniqueCharacter = "qwertysdaqmnb";

        #region Prob_1_1
        public static void Prob_1_1_IsUnique_Test()
        {
            var shouldBeTrue = Prob_1_1_IsUnique(SampleInput_UniqueCharacter);
            var shouldBeFalse = Prob_1_1_IsUnique(SampleInput_NonUniqueCharacter);
        }

        /// <summary>
        /// Implement an algorithm to determine if a string 
        /// has all unique characters
        /// </summary>
        /// <returns>The true unique</returns>
        public static bool Prob_1_1_IsUnique(string stringInput)
        {
            var checkIfAscii = true;
            if (checkIfAscii && stringInput.Length > 128)
                return false;

            Hashtable container = new Hashtable();

            foreach(var character in stringInput)
            {
                if (container[character] != null)
                    return false;
                    
                container.Add(character, true);
            }

            return true;
        }

        public static bool Prob_1_1_IsUnique_BookSolution(string stringInput)
        {
            // First ask if string is in ASCII or Unicode. If it is ASCII,
            // you can check for a max lenght of 128 character, since that
            // is the max character for ASCII. 
            if (stringInput.Length > 128)
                return false;

            var charSet = new bool[128];

            foreach (var character in stringInput.ToCharArray())
            {
                //A char can be implicitly converted to int, long, float, double, or decimal. 
                if (charSet[character])
                {
                    return false;
                }

                charSet[character] = true;
            }
            return true;
        }
        #endregion

        #region Prob_1_2
        public static string SampleInput_Anagram_Input = "Debit card";
        public static string SampleInput_Anagram_Output = "Bad credit";

        public static void Prob_1_2_CheckPermutation_Test()
        {
            Prob_1_2_CheckPermutation_Test(SampleInput_Anagram_Input, SampleInput_Anagram_Output);
        }

        public static void Prob_1_2_CheckPermutation_Test(string firstInput, string secondInput)
        {
            var result = Prob_1_2_IsAnagram(firstInput, secondInput);

            Console.WriteLine($"Is {secondInput} an anagram of {firstInput}: {result}");
        }

        /// <summary>
        /// Given two strings, write a method to decide if one is a permutation
        /// of the other
        /// An anagram is a word or phrase formed by rearranging the letters of 
        /// a different word or phrase, typically using all the original 
        /// letters exactly once. 
        /// </summary>
        public static bool Prob_1_2_IsAnagram_v2(string input, string anagram)
        {
            if (input.Length != anagram.Length)
                return false;

            var hashOfInputChars = new Hashtable();

            foreach(char currentLetter in input)
            {
                if (hashOfInputChars[currentLetter] == null)
                    hashOfInputChars[currentLetter] = 0;
                else
                    hashOfInputChars[currentLetter] =+ 1;
            }

            foreach(char letter in anagram)
            {
                if (hashOfInputChars[letter] == null || (int)hashOfInputChars[letter] < 1)
                    return false;

                hashOfInputChars[letter] = -1;
            }

            return true;
        }

        public static bool Prob_1_2_IsAnagram(string baseString, string secondString)
        {
            //Double check if whitespace counts and if it is case sensitive
            var baseStringArray = baseString.Trim().ToLower().ToCharArray();
            var secondStringArray = secondString.Trim().ToLower().ToCharArray();

            if (baseStringArray.Length != secondStringArray.Length)
                return false;

            Array.Sort(baseStringArray);
            Array.Sort(secondStringArray);

            for(int i = 0; i < baseStringArray.Length; i++)
            {
                if (baseStringArray[i] != secondStringArray[i])
                    return false;
            }

            return true;
        }

        public static bool Prob_1_2_IsAnagram_TempTest(string s1, string anagram)
        {
            if (s1.Length != anagram.Length) return false;

            var container = new Dictionary<char, int>();
            foreach(var c in s1.ToLower())
            {
                if (container.ContainsKey(c))
                    container[c] = container[c] + 1;
                else
                    container.Add(c, 1);
            }

            foreach(var c in anagram.ToLower())
            {
                if (!container.ContainsKey(c)) return false;
                container[c] -= 1;
                if (container[c] < 0) return false;
            }

            return true;
        }

        public static bool Prob_1_2_IsAnagram_TempTest2(string s1, string anagram)
        {
            if (s1.Length != anagram.Length) return false;

            var container = new int[128];
            foreach (var c in s1.ToLower())
                container[c]++;

            foreach (var c in anagram.ToLower())
            {
                container[c]--;
                if (container[c] < 0) return false;
            }

            return true;
        }

        public static bool Prob_1_2_IsAnagram_BookSolution1(string baseString, string secondString)
        {
            if (baseString.Length != secondString.Length)
                return false;

            return ToSortedCharArray(baseString) == ToSortedCharArray(secondString);
        }

        private static char[] ToSortedCharArray(string s)
        {
            var array = s.ToCharArray();
            Array.Sort(array);

            return array;
        }

        public static bool Prob_1_2_IsAnagram_BookSolution2(string baseString, string secondString)
        {
            if (baseString.Length != secondString.Length)
                return false;

            var letters = new int[128];  //Assuming ASCII
            for(int i = 0; i < baseString.Length; i++)
            {
                letters[i]++;
            }

            for (int i = 0; i < secondString.Length; i++)
            {
                letters[i]--;

                if (letters[i] < 0)
                    return false;
            }

            return true;
        }
        #endregion

        #region Prob_1_3_Urlify
        /// <summary>
        /// Write a method to replaces spaces in an string (char array) with %20
        /// Example:
        /// input  'Mr John Smith    '
        /// output 'Mr%20John%20Smith' 
        /// You can assume that you are given the true lenght of the string (including spaces)
        /// which in the example above is 13 (Mr John Smith).
        /// Note: While not mentioned in the book, you are expected to re-arrange the
        /// input array. The original input just has the same lenght as if you took 'Mr Jon Smith'
        /// and replaced the spaces with %20, resulting in 'Mr%20Jon%20Smith', except that
        /// extra spaces are shifted (towards the end), so you are suppose to re-arrange the original input array so 
        /// the extra spaces at the end are used to shift the array to you have the output 'Mr%20John%20Smith' where the 
        /// original first space is 
        /// </summary>
        public static void Prob_1_3_Urlify_Test()
        {
            int trueStringLenght = 13;
            var input = "Mr John Smith    ".ToCharArray();
            var expectedOutput = "Mr%20John%20Smith".ToCharArray();

            ReplaceWhitespace(input, trueStringLenght);

            var outputAsString = new string(input);
            var expectedOutputAsString = new string(expectedOutput);
            Console.WriteLine($"Output: {outputAsString}");
            Console.WriteLine($"Expected output: {expectedOutputAsString}");

            var success = outputAsString.Equals(expectedOutputAsString);
            Console.WriteLine($"Success: {success}");
        }

        public static void Prob_1_3_Urlify_Test_v2()
        {
            int trueStringLenght = 13;
            var input = "Mr John Smith    ".ToCharArray();
            var expectedOutput = "Mr%20John%20Smith";

            var outputAsString = ReplaceWhitespace_WithNewString(input, trueStringLenght);
            Console.WriteLine($"Output: {outputAsString}");
            Console.WriteLine($"Expected output: {expectedOutput}");

            var success = outputAsString.Equals(expectedOutput);
            Console.WriteLine($"Success: {success}");
        }
        public static void ReplaceWhitespace(char[] input, int trueLength)
        {
            int spaceCount = 0;
            //Since we know the true lenght and extra space in the input are only
            //on the end, we can use the true lenght to get real number of spaces
            for(int i = 0; i < trueLength; i++)
            {
                if (input[i] == ' ')
                    spaceCount++;
            }

            int index = input.Length;
            for(int i = trueLength -1; i >= 0; i--)
            {
                if(input[i] == ' ')
                {
                    input[index - 1] = '0';
                    input[index - 2] = '2';
                    input[index - 3] = '%';
                    index -= 3;
                }
                else
                {
                    input[index - 1] = input[i];
                    index--;
                }
            }
        }

        public static string ReplaceWhitespace_WithNewString(char[] input, int trueLength)
        {
            var results = new StringBuilder();
            for(int i =0; i < trueLength; i++)
            {
                if (input[i] == ' ')
                    results.Append("%20");
                else
                    results.Append(input[i]);
            }

            return results.ToString();
        }


        /// <summary>
        /// ----THIS EXAMPLE AND SOLUTION ARE FOR A MISSUNDERSTOOP QUESTION
        /// Write a method to replace all spaces in a string with %20
        /// Example:
        /// input 'Mr. John Smith    '
        /// output 'Mr%20John%20Smith' (length is 13, when replace %20 with a single space)
        /// You can assume that you are given the true lenght of the string
        /// whinch in the example above is 13.
        /// Note: While not mentioned in the book, it does NOT expect leading
        /// or trailing space to be converted to %20, instead they are trimmed
        /// </summary>
        public static void Prob_1_3_Urlify_Test_NOT_SOLVING_CORRECT_PROBLEM()
        {
            var input = "Mr.  John     Smith    ";
            var expectedOutput = "Mr.%20John%20Smith";

            var output = ReplaceWhitespace_SOLVED_OTHER_PROBLEM(input);
            Console.WriteLine($"Output: {output}. Expecetd output: {expectedOutput}");
            var success = output.Equals(expectedOutput);
            Console.WriteLine($"Success: {success}");
        }

        public static string ReplaceWhitespace_SOLVED_OTHER_PROBLEM(string input)
        {
            input = input.Trim();

            var currentWhiteSpaceIndex = input.IndexOf(' ', 0);
            if (currentWhiteSpaceIndex == -1)
                return input;

            var result = new StringBuilder();
            var currentTextIndex = 0;
            var textLength = (currentWhiteSpaceIndex) - currentTextIndex;
            var text = input.Substring(currentTextIndex, textLength);

            Console.WriteLine($"currentStartingIndex: {currentWhiteSpaceIndex}. Appending Text: {text}");
            result.Append(text);
            Console.WriteLine($"Current Results {result.ToString()}");

            var nextWhiteSpaceIndex = -1;


            do
            {
                nextWhiteSpaceIndex = input.IndexOf(' ', currentWhiteSpaceIndex + 1);
                Console.WriteLine($"New nextWhiteSpaceIndex: {nextWhiteSpaceIndex}");

                if (nextWhiteSpaceIndex == -1)
                {
                    result.Append("%20");
                    Console.WriteLine($"nextWhiteSpaceIndex is -1. Appending %20. Result: {result.ToString()}");
                    textLength = input.Length - (currentWhiteSpaceIndex + 1);
                    Console.WriteLine($"nextWhiteSpaceIndex is -1. Last text length: {textLength}");
                    var lastChunkOfText = input.Substring(currentWhiteSpaceIndex + 1, textLength);
                    Console.WriteLine($"lastChunkOfText: {lastChunkOfText}");
                    result.Append(lastChunkOfText);
                }
                else if (nextWhiteSpaceIndex == currentWhiteSpaceIndex + 1)
                {
                    Console.WriteLine($"nextWhiteSpaceIndex equals currentWhiteSpaceIndex + 1. Setting currentWhiteSpaceIndex to {nextWhiteSpaceIndex}");
                    currentWhiteSpaceIndex = nextWhiteSpaceIndex;
                }
                else
                {
                    result.Append("%20");
                    Console.WriteLine($"Adding %20 for whitespace. Current string {result.ToString()}");

                    currentTextIndex = currentWhiteSpaceIndex + 1;
                    currentWhiteSpaceIndex = nextWhiteSpaceIndex;

                    var textLenght = nextWhiteSpaceIndex - currentTextIndex;
                    Console.WriteLine($"currentTextIndex: {currentTextIndex}. currentWhiteSpaceIndex: {currentWhiteSpaceIndex} textLenght: {textLenght}");

                    var chunkOfText = input.Substring(currentTextIndex, textLenght);
                    Console.WriteLine($"Adding chunkOfText: {chunkOfText}");
                    result.Append(chunkOfText);
                    Console.WriteLine($"Current Results: {result.ToString()}");
                }

            } while (nextWhiteSpaceIndex != -1);

            Console.WriteLine($"Result: {result.ToString()}");
            return result.ToString();
        }

        #endregion

        #region Prob_1_4_IsPalindromePermutation
        /// <summary>
        /// Probs the 1 4 is palindrome test:
        /// Given a string, write a funciton to check if it is a permutation
        /// of a palindrome. A palindrome is a word or phrase that is the same
        /// forwards or backwards. A permutation is a rearrengement of letters.
        /// The palindrome doe snot need to be limited to just dictionary words.
        /// Input: Tact Coa
        /// Output: True (permutations: 'taco cat', 'atco cta')
        /// 
        /// Note: based on examples, it does not seem like case matters or whitespace
        /// So we need to find if a given input string is a permutation of a palindrome
        /// </summary>
        private static string prob_1_4_input = "Tact Coa";
        private static string prob_1_4_permutation1 = "taco cat";
        private static string prob_1_4_permutation2 = "atco cta";


        public static void Prob_1_4_IsPalindromePermutation_Test()
        {
            Console.WriteLine($"Input: {prob_1_4_input}. Permutation: {prob_1_4_permutation1}");
            var result0 = Prob_1_4_IsPalindromePermutation_v0(prob_1_4_input);
            Console.WriteLine($"Is Palindrom Permutaiton v1 Solution: {result0}");

            /*
            var result3 = Prob_1_4_IsPalindromePermutation_v3(prob_1_4_input, prob_1_4_permutation1);
            var result4 = Prob_1_4_IsPalindromePermutation_v4(prob_1_4_input, prob_1_4_permutation1);
            var result5 = Prob_1_4_IsPalindromePermutation_v5(prob_1_4_input, prob_1_4_permutation1);

            Console.WriteLine($"Is Palindrom Permutaiton v1 Solution: {result3}");
            Console.WriteLine($"Is Palindrom Permutaiton v2 Solution: {result4}");
            Console.WriteLine($"Is Palindrom Permutaiton v3 Solution: {result5}");
            */           

        }

        public static bool Prob_1_4_IsPalindromePermutation_v0(string input)
        {
            var charCounter = new Hashtable();

            for (int i = 0; i < input.Length; i++)
            {
                var key = char.ToLower(input[i]);
                if (charCounter.ContainsKey(key))
                    charCounter[key] = (int)charCounter[key] + 1;
                else
                    charCounter.Add(key, 1);

                Console.WriteLine($"Current key: {key}. Current count: {(int)charCounter[key]}");
            }

            var spaceCount = charCounter.ContainsKey(' ')
                ? (int)charCounter[' ']
                : 0;
            var realPalindromeLength = input.Length - spaceCount;
            Console.WriteLine($"spaceCount {spaceCount}. realPalindromeLenght: {realPalindromeLength}");

            var shouldHaveOneOddCount = realPalindromeLength % 2 != 0;
            var oddCount = 0;
            var evenCount = 0;

            charCounter.Remove(' ');
            foreach (int charCount in charCounter.Values)
            {
                if (charCount % 2 == 0)
                    evenCount++;
                else
                    oddCount++;
            }

            Console.WriteLine($"shouldHaveOneOddCount: {shouldHaveOneOddCount}. oddCount: {oddCount}. EvenCount: {evenCount}");

            var isPalindromePermutation = (shouldHaveOneOddCount && oddCount == 1)
                || (!shouldHaveOneOddCount && oddCount == 0);

            Console.WriteLine($"isPalindromePermutation:{isPalindromePermutation}");
            return isPalindromePermutation;
        }



        /// <summary>
        /// Probs the 1 4 is palindrome permutation v3 to 5 solve the WRONG problem.
        /// they take another input, permutation. The actual problem just requires us to find
        /// out if the (only) input string is a palindrome permutation
        /// </summary>
        public static bool Prob_1_4_IsPalindromePermutation_v3(string input, string permutaiton)
        {
            if(input.Length != permutaiton.Length)
                return false;

            var sortedInput = input.ToLower().ToCharArray();
            Array.Sort(sortedInput);
            var sortedPermutation = permutaiton.ToLower().ToCharArray();
            Array.Sort(sortedPermutation);

            for(int i = 0; i < sortedInput.Length; i++)
            {
                if (sortedInput[i] != sortedPermutation[i])
                    return false;
            }
            var date = new DateTime(2011, 1, 20);
            date.DayOfWeek.ToString();
            return true;
        }

        public static bool Prob_1_4_IsPalindromePermutation_v4(string input, string permutaiton)
        {
            if (input.Length != permutaiton.Length)
                return false;

            //assume ASCI
            var uniqueLetterTableCount = new int[128];

            for (int i = 0; i < input.Length; i++)
            {
                var key = char.ToLower(input[i]);
                uniqueLetterTableCount[key]++;
            }

            for (int i = 0; i < permutaiton.Length; i++)
            {
                var key = char.ToLower(permutaiton[i]);
                uniqueLetterTableCount[key]--;

                if (uniqueLetterTableCount[key] < 0)
                    return false;
            }

            return true;
        }


        public static bool Prob_1_4_IsPalindromePermutation_v5(string input, string permutaiton)
        {
            if (input.Length != permutaiton.Length)
                return false;

            var uniqueLetterTableCount = new Hashtable();

            for (int i = 0; i < input.Length; i++)
            {
                var key = char.ToLower(input[i]);

                if (uniqueLetterTableCount.ContainsKey(key))
                    uniqueLetterTableCount[key] = (int)uniqueLetterTableCount[key] +1;
                else
                    uniqueLetterTableCount.Add(key, 1);

            }

            for(int i = 0; i < permutaiton.Length; i++)
            {
                var key = char.ToLower(permutaiton[i]);

                if (!uniqueLetterTableCount.ContainsKey(key))
                    return false;

                uniqueLetterTableCount[key] = (int)uniqueLetterTableCount[key] - 1;

                if ((int)uniqueLetterTableCount[key] < 0)
                    return false;
            }

            return true;
        }
        #endregion

        #region Prob_1_5_One_Way

        public static bool Prob_1_5_IsOneEditAwayTemp(string s1, string s2)
        {
            if (Math.Abs(s1.Length - s2.Length) > 1) return false;

            string s = s1.Length > s2.Length ? s2 : s1;
            string l = s1.Length > s2.Length ? s1 : s2;
            int diffCount = 0, sIdx = 0, lIdx = 0;

            while(sIdx < s.Length && lIdx < l.Length)
            {
                if (s[sIdx] == l[lIdx])
                    sIdx++;
                else
                {
                    diffCount++;
                    if (diffCount > 1) return false;
                    if (s.Length == l.Length)
                        sIdx++;
                }
                lIdx++;

                /*
                if (s[sIdx] == l[lIdx])
                {
                    sIdx++;
                    lIdx++;
                } else
                {
                    diffCount++;
                    if (diffCount > 1) return false;
                    if(s.Length == l.Length)
                    {
                        lIdx++;
                        sIdx++;
                    }
                    else
                        lIdx++;
                }
                */
            }
            return true;
        }

        /// <summary>
        /// There are three types of edits that can be performed on strings:
        ///     inseart a character
        ///     remove a character
        ///     replace a character
        /// Given two strings, write a function to check if they are one edit
        /// (or zero) edits away. Examples
        /// pale, ple   -> true
        /// pales, pale -> true
        /// pale, bale  -> true
        /// pale, bake  -> false
        /// </summary>
        public static void Prob_1_5_One_Way_Test()
        {
            foreach(var inputs in Prob_1_5_SampleInputs)
            {
                Console.WriteLine($"Compareing input: {inputs.Item1}. To output: {inputs.Item2}. Expected result {inputs.Item3}");

                var result = Prob_1_5_One_Way_v1(inputs.Item1, inputs.Item2);
                var isCorrect = result == inputs.Item3;

                Console.WriteLine($"{isCorrect}:::: Compared input: {inputs.Item1}. To output: {inputs.Item2}. Expected result {inputs.Item3}. Actual result: {result}");
            }
        }

        private static List<Tuple<string, string, bool>> Prob_1_5_SampleInputs = new List<Tuple<string, string, bool>>()
        {
            new Tuple<string, string, bool>("pale", "ple", true),
            new Tuple<string, string, bool>("pale", "pale", true),
            new Tuple<string, string, bool>("pale", "bale", true),
            new Tuple<string, string, bool>("pale", "bake", false)
        };

        public static bool Prob_1_5_One_Way_v1(string input, string final)
        {
            var lengthDiff = input.Length - final.Length;
            lengthDiff = lengthDiff < 0 ? (lengthDiff * -1) : lengthDiff;
            Console.WriteLine($"Length diff is {lengthDiff}");

            if (lengthDiff > 2)
                return false;

            int differences = 0;
            if(lengthDiff == 0)
            {
                for(int i = 0; i < input.Length; i++)
                {
                    Console.WriteLine($"Comparing current input: {input[i]} and final: {final[i]}");
                    if(input[i] != final[i])
                    {
                        differences++;
                        if(differences > 1)
                        {
                            Console.WriteLine($"Differences is greater than one. Returning false");
                            return false;
                        }
                    }
                }
                Console.WriteLine($"There are {differences} differences.Returning True");
                return true;
            }
            else
            {
                string shortText, longText;

                if (input.Length > final.Length)
                {
                    longText = input;
                    shortText = final;
                } else
                {
                    longText = final;
                    shortText = input;
                }

                var offset = 0;
                for (int i = 0; i < shortText.Length; i++)
                {
                    Console.WriteLine($"Comparing current shortText: {shortText[i]} and long: {longText[i + offset]}");
                    if (shortText[i] == longText[i + offset])
                    {
                        Console.WriteLine("They are equal moving to next");
                        continue;
                    }

                    offset++;
                    if (offset > 1)
                    {
                        Console.WriteLine($"offset is greater than one. Returning false");
                        return false;
                    }

                    if (shortText[i] != longText[i + offset])
                    {
                        Console.WriteLine($"Difference in more than one. Returning flase");
                        return false;
                    }
                }
                Console.WriteLine($"There is {offset} offset. Returning True");
                return true;
            }
        }

        public static bool Prob_1_5_One_Way_BookSolution(string first, string second)
        {
            /* Length checks. */
                if (Math.Abs(first.Length - second.Length) > 1)
            {
                return false;
            }

            /* Get shorter and longer string.*/
            String s1 = first.Length < second.Length ? first : second;
            String s2 = first.Length < second.Length ? second : first;

            int index1 = 0;
            int index2 = 0;
            bool foundDifference = false;
            while (index2 < s2.Length && index1 < s1.Length)
            {
                if (s1[index1] != s2[index2])
                {
                    /* Ensure that this is the first difference found.*/
                    if (foundDifference) return false;
                    foundDifference = true;
                    if (s1.Length == s2.Length)
                    { // On replace, move shorter pointer
                        index1++;
                    }
                }
                else
                {
                    index1++; // If matching, move shorter pointer
                }
                index2++; // Always move pointer for longer string
            }
            return true;
        }

        #endregion

        #region  Prob_1_6_StringCompression

        public static string Prob_1_6_StringCompression_Test_Input = "aabcccccaaa";
        public static string Prob_1_6_StringCompression_Test_Output = "a2b1c5a3";

        public static void Prob_1_6_StringCompression_Test()
        {
            //var result = Prob_1_6_StringCompression_v1(Prob_1_6_StringCompression_Test_Input);
            var result = Prob_1_6_StringCompression_Temp(Prob_1_6_StringCompression_Test_Input);

            var isCorrect = result.Equals(Prob_1_6_StringCompression_Test_Output);
            Console.WriteLine($"Output: {result} Expected output: {Prob_1_6_StringCompression_Test_Output}" +
            	$". Is Successfull: {isCorrect}");
        }

        public static string Prob_1_6_StringCompression_Temp(string input)
        {
            var compressedString = new StringBuilder();
            char lastChar = input[0];
            int count = 1;

            for(int i = 1; i < input.Length; i++)
            {
                var curChar = input[i];
                if (lastChar == curChar)
                    count++;
                else
                {
                    compressedString.Append($"{lastChar}{count}");
                    lastChar = curChar;
                    count = 1;
                }
            }
            compressedString.Append($"{lastChar}{count}");

            return compressedString.Length > input.Length ? input : compressedString.ToString();
        }

        /// <summary>
        /// Probs the 1 6 string compression v1.
        /// Implement a method to perform basic string compression using
        /// the counts of repeated characters. For example:
        /// Input: aabcccccaaa
        /// Output: a2b1c5a3
        /// If the compressed string would not become smaller than the original
        /// string, your method should return the original string. You can assume
        /// the string has only uppercase and lowercase letters (a-z)
        /// </summary>
        /// <returns>The 1 6 string compression v1.</returns>
        /// <param name="input">Input.</param>
        public static string Prob_1_6_StringCompression_v1(string input)
        {
            var compressedString = new StringBuilder();
            var lastLetterCount = 1;
            char lastLetter = input[0];

            for (int i = 1; i < input.Length; i++)
            {
                var currentLetter = input[i];

                if (currentLetter == lastLetter)
                    lastLetterCount++;
                else
                {
                    compressedString.Append($"{lastLetter}{lastLetterCount}");
                    lastLetter = currentLetter;
                    lastLetterCount = 1;
                }
            }

            compressedString.Append($"{lastLetter}{lastLetterCount}");

            return compressedString.Length <= input.Length
                ? compressedString.ToString()
                : input;
        }


        #endregion
    }
}
