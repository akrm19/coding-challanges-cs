using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrackingTheCodingInterviewProblems.LeetCode
{
    public static class ArrayAndStrings
    {
        /// <summary>
        /// Given a string, find the length of the longest
        /// substring without repeating characters.
        /// </summary>
        /*
            Example 1:
            Input: "abcabcbb"
            Output: 3 
            Explanation: The answer is "abc", with the length of 3. 
            Example 2:

            Input: "bbbbb"
            Output: 1
            Explanation: The answer is "b", with the length of 1.
            Example 3:

            Input: "pwwkew"
            Output: 3
            Explanation: The answer is "wke", with the length of 3. 
            Note that the answer must be a substring, "pwke" is a
            subsequence an and not a substring.
        */
        public static int LongestSubstring(string s)
        {
            //Keep track of longest substring, curr start Idx
            int longest = 0, startIdx = 0;

            //Keep track of seen chars and index of last seen
            var seen = new Dictionary<char, int>();

            //iterate through string
            for (int i = 0; i < s.Length; i++)
            {
                //Check if we seen the char
                if (seen.ContainsKey(s[i]))
                {
                    //If so, update startIdx to either:
                    //-one over the last time we seen the char
                    //-or keep the last startIdx, if it is higher
                    //(to prevent start from moving back, in cases like:
                    //"abba"
                    startIdx = Math.Max((seen[s[i]] + 1), startIdx);
                    //Update last time we seen char
                    seen[s[i]] = i;
                }
                //Else add char and idx to hash
                else
                    seen.Add(s[i], i);

                //Calc curr substring length. Update longest if needed
                longest = Math.Max(longest, (i - startIdx) + 1);
            }
            return longest;
        }



        /*
            Implement atoi which converts a string to an integer.
            The function first discards as many whitespace characters
            as necessary until the first non-whitespace character is
            found. Then, starting from this character, takes an optional
            initial plus or minus sign followed by as many numerical
            digits as possible, and interprets them as a numerical value.

            The string can contain additional characters after those that
            form the integral number, which are ignored and have no effect
            on the behavior of this function.

            If the first sequence of non-whitespace characters in str is
            not a valid integral number, or if no such sequence exists
            because either str is empty or it contains only whitespace
            characters, no conversion is performed.

            If no valid conversion could be performed, a zero value is returned.

            Note:
            Only the space character ' ' is considered as whitespace character.
            Assume we are dealing with an environment which could only store
            integers within the 32-bit signed integer range: [−231,  231 − 1].
            If the numerical value is out of the range of representable values,
            INT_MAX (231 − 1) or INT_MIN (−231) is returned.
         */
        public static int MyAtoi(string str)
        {
            //Check that its not null or empty
            if (string.IsNullOrWhiteSpace(str)) return 0;

            //Discard whitespace
            str = str.Trim();

            //Check: first char is number or -
            bool skipFirst = str[0] == '-' || str[0] == '+';

            if (skipFirst && str.Length == 1)
                return 0;

            //return 0 otherwise
            if (!skipFirst && !char.IsDigit(str[0]))
                return 0;

            var resultStr = new StringBuilder(skipFirst ? str[0].ToString() : "");
            //iterate through remaining chars
            for (int i = skipFirst ? 1 : 0; i < str.Length; i++)
            {
                if (!char.IsDigit(str[i]))
                    break;
                //while the char is valid num
                //keep adding to another array
                resultStr.Append(str[i]);
            }

            //Check if there are any valid nmums. Return 0 if not
            if (skipFirst && resultStr.Length == 1)
                return 0;

            var result = 0;
            //Try to convert to Int32
            if (Int32.TryParse(resultStr.ToString(), out result))
                return result;

            //check if it is bigger than 32-bit int,
            //if so return max 32-bit number
            return skipFirst && str[0] == '-'
                ? Int32.MinValue
                : Int32.MaxValue;
        }




        /*
                Roman numerals are represented by seven different symbols:
         *      I, V, X, L, C, D and M.
         *
         *      For example, two is written as II in Roman numeral,
         *      just two one's added together. Twelve is written as, XII,
         *      which is simply X + II. The number twenty seven is written
         *      as XXVII, which is XX + V + II.

                Roman numerals are usually written largest to smallest from
                left to right. However, the numeral for four is not IIII.
                Instead, the number four is written as IV. Because the one is
                before the five we subtract it making four. The same principle
                applies to the number nine, which is written as IX. There are
                six instances where subtraction is used:

                I can be placed before V (5) and X (10) to make 4 and 9. 
                X can be placed before L (50) and C (100) to make 40 and 90. 
                C can be placed before D (500) and M (1000) to make 400 and 900.

                Given a roman numeral, convert it to an integer. Input is
                guaranteed to be within the range from 1 to 3999.
         */
        public static int RomanToInt(string s)
        {
            var romanToNum = new Dictionary<char, int>() {
                {'I', 1},
                {'V', 5},
                {'X', 10},
                {'L', 50},
                {'C', 100},
                {'D', 500},
                {'M', 1000},
            };

            int total = 0;

            //iterate through chars
            for (int i = 0; i < s.Length; i++)
            {
                //Get roman value
                var curVal = romanToNum[s[i]];
                //check next char (ensure within length)
                //and see if it is special case like IX
                if (i + 1 < s.Length && IsSpecialCase(s[i], s[i + 1]))
                {
                    //Is so add special numb and skip 1 extra space
                    curVal = romanToNum[s[++i]] - curVal;
                }
                //add to total
                total += curVal;
            }
            return total;
        }

        public static bool IsSpecialCase(char curr, char next)
        {
            return ((curr == 'I' && (next == 'V' || next == 'X'))
                    || (curr == 'X' && (next == 'L' || next == 'C'))
                    || (curr == 'C' && (next == 'D' || next == 'M')));

        }


        /*
            Given an array nums of n integers, are there elements
            a, b, c in nums such that a + b + c = 0? Find all unique 
            triplets in the array which gives the sum of zero.

            Note:
            The solution set must not contain duplicate triplets.

            Example:
            Given array nums = [-1, 0, 1, 2, -1, -4],

            A solution set is:
            [
              [-1, 0, 1],
              [-1, -1, 2]
            ]
        */
        public static IList<IList<int>> ThreeSum(int[] nums)
        {
            var results = new List<IList<int>>();
            var hash = new Dictionary<int, List<int>>();

            //Iterate through array
            for (int i = 0; i < nums.Length; i++)
            {
                //get curr a =val
                int a = nums[i];
                int targetBC = 0 - a;

                //Add to hash
                if (!hash.ContainsKey(a))
                    hash.Add(a, new List<int>() { i });

                //inner loop. Iterate (increase by 1)
                for (int x = i + 1; x < nums.Length; x++)
                {
                    //set curr = b and targetC
                    int b = nums[x];
                    int targetC = targetBC - b;

                    //look for c in hash
                    if (hash.ContainsKey(targetC))
                    {
                        //if found, add ABC to results
                        results.Add(new List<int>() { a, b, hash[targetC][0] });
                    }

                    //if not found add curr to hash 
                    if (!hash.ContainsKey(b))
                        hash.Add(b, new List<int>() { x });
                    else if (!hash[b].Contains(x))
                        hash[b].Add(x);
                }
            }

            return (IList<IList<int>>)results;
        }




        /*
        Given a sorted array nums, remove the duplicates in-place 
        such that each element appear only once and return the new length.

        Do not allocate extra space for another array, you must do this by 
        modifying the input array in-place with O(1) extra memory.
        */
        public static int RemoveDuplicates(int[] nums)
        {
            //check for empty or 1 element array
            if (nums.Length < 2) return nums.Length;

            //slowIdx
            int slow = 0;

            //iterate through the array (start w/fast = 1)
            for (int fast = 1; fast < nums.Length; fast++)
            {
                //If slow != fast, copy fast new vals in slow
                if (nums[slow] != nums[fast])
                {
                    //first increment slow by one to new spot
                    slow++;
                    if (slow != fast)
                        nums[slow] = nums[fast];
                }
                // else continue moving fast forward
            }
            //Increment slow by one
            return slow + 1;
        }


        /*
        Given an array nums, write a function to move all 0's to the end 
        of it while maintaining the relative order of the non-zero elements.

        Example:

        Input: [0,1,0,3,12]
        Output: [1,3,12,0,0]
        Note:

        You must do this in-place without making a copy of the array.
        Minimize the total number of operations.
        */
        public static void MoveZeroes(int[] nums)
        {
            //Slow pointer
            int slow = 0;

            //fast pointer
            for (int fast = 0; fast < nums.Length; fast++)
            {
                //When non-zero encountered, copy/switch item to slow
                if (nums[fast] != 0)
                    //move slow forward after that
                    nums[slow++] = nums[fast];

                //else keep moving fast forward
            }

            //set rest of items to 0
            for (; slow < nums.Length; slow++)
                nums[slow] = 0;
        }




        public static string Multiply(string num1, string num2)
        {
            if (num1 == "0" || num2 == "0") return "0";
            var results = new List<string>();
            //Take into account the offset (increasing by x10 from left to right)
            int offset = 0, longest = 0;
            foreach (char multiplierChar in num2.ToCharArray().Reverse())
            {
                int multiplier = int.Parse(multiplierChar.ToString());
                var resultStr = new StringBuilder(new String('0', offset));
                int carryover = 0;
                foreach (char multiplicandChar in num1.ToCharArray().Reverse())
                {
                    int multiplicand = int.Parse(multiplicandChar.ToString());
                    var result = (multiplier * multiplicand) + carryover;
                    carryover = result > 9
                        ? result / 10
                        : 0;

                    if (result > 9)
                        result = result % 10;

                    resultStr.Append(result);
                }
                if (carryover > 0)
                    resultStr.Append(carryover);

                longest = resultStr.Length > longest
                    ? resultStr.Length : longest;


                //Add result to a List of strings
                results.Add(resultStr.ToString());
                offset++;
            }

            //Add list of strings 
            var sumStr = new StringBuilder();
            int carryover2 = 0;

            for (int i = 0; i < longest; i++)
            {
                var sum = carryover2;
                foreach (var r in results)
                {
                    if (r.Length > i)
                        sum += int.Parse(r[i].ToString());
                }

                carryover2 = sum > 9
                    ? sum / 10 : 0;

                if (sum > 9)
                    sum = sum % 10;

                sumStr.Append(sum);
            }

            if (carryover2 > 0)
                sumStr.Append(carryover2);

            var reversedResult = sumStr.ToString();
            sumStr.Clear();
            for (int i = reversedResult.Length - 1; i >= 0; i--)
                sumStr.Append(reversedResult[i]);

            return sumStr.ToString();
        }


        /*
        Given a string s of '(' , ')' and lowercase English characters. 

        Your task is to remove the minimum number of parentheses ( '(' or ')',
        in any positions ) so that the resulting parentheses string is valid
        and return any valid string.

        Formally, a parentheses string is valid if and only if:

        It is the empty string, contains only lowercase characters, or
        It can be written as AB (A concatenated with B), where A and B are valid
        strings, or it can be written as (A), where A is a valid string.

        Example 1:
        Input: s = "lee(t(c)o)de)"
        Output: "lee(t(c)o)de"
        Explanation: "lee(t(co)de)" , "lee(t(c)ode)" would also be accepted.

        Example 2:
        Input: s = "a)b(c)d"
        Output: "ab(c)d"

        Example 3:
        Input: s = "))(("
        Output: ""
        Explanation: An empty string is also valid.

        Example 4:
        Input: s = "(a(b(c)d)"
        Output: "a(b(c)d)"
        */
        public static string MinRemoveToMakeValid(string s)
        {
            //validate parenthesis string (on lowercase english or empty)
            var stack = new Stack<Tuple<char, int>>();
            var charArr = s.ToCharArray();

            //iterate through string
            for (int i = 0; i < charArr.Length; i++)
            {
                var c = charArr[i];
                //as parenthesis are encountered. /Check if valid:
                //If opening its OK, add to stack
                if (c == '(')
                    stack.Push(new Tuple<char, int>(c, i));
                //If closing, peek stack
                else if (c == ')')
                {
                    // If stack is empty or doe not hae a matching closin stack
                    if (stack.Count == 0 || stack.Peek().Item1 != '(')
                        //Mark for override
                        charArr[i] = '*';
                    //if stack last item is a matching opening parenthises, its OK
                    else if (stack.Peek().Item1 == '(')
                        //pop opening parentheses and skip closing parentheses
                        stack.Pop();
                }
            }

            //At end check stack for any remaining parenthesis
            while (stack.Count != 0)
            {
                //Mark for removal
                var cur = stack.Pop();
                charArr[cur.Item2] = '*';
            }

            return new string(charArr).Replace("*", "");
        }


        /*
        Given a string, determine if it is a palindrome, considering 
        only alphanumeric characters and ignoring cases.

        Note: For the purpose of this problem, we define empty string 
        as valid palindrome.

        Example 1:
        Input: "A man, a plan, a canal: Panama"
        Output: true

        Example 2:
        Input: "race a car"
        Output: false
        */
        public static bool IsPalindrome(string s)
        {
            //check for empty string
            if (string.IsNullOrWhiteSpace(s)) return true;

            //Use two pointers.
            //Starting one: sIdx & an ending one: eIdx
            int sIdx = 0, eIdx = s.Length - 1;

            //While the sIdx < eIdx
            while (sIdx < eIdx)
            {
                //get next sidx char (skip non alpha numeric)
                if (!char.IsLetterOrDigit(s, sIdx))
                    sIdx++;
                //get next eIdx char (skip non alpha numeric)
                else if (!char.IsLetterOrDigit(s, eIdx))
                    eIdx--;
                //If not equal return false
                else if (char.ToLower(s[sIdx++]) != char.ToLower(s[eIdx--]))
                    return false;

                //If equal continue. Increment sIdx & decrement eIdx
            }

            return true;
        }


        /*
        Given a string S, check if the letters can be rearranged 
        so that two characters that are adjacent to each other are not the same.

        If possible, output any possible result.  If not possible, return the empty string.

        Example 1:
        Input: S = "aab"
        Output: "aba"

        Example 2:
        Input: S = "aaab"
        Output: ""
        Note:

        S will consist of lowercase letters and have length in range [1, 500].
        */
        public static string ReorganizeString(string S)
        {
            //check for empty string or length of 1
            if (string.IsNullOrWhiteSpace(S) || S.Length < 2) return S;

            //order string
            S = S.OrderBy(c => c).ToString();

            //Use two stacks:
            //ordered = ordered string
            //toPlace = dups that need to be placed
            var ordered = new Stack<char>();
            var toPlace = new Stack<char>();

            //iterate through the string
            for (int i = 0; i < S.Length; i++)
            {
                if (ordered.Count == 0)
                    ordered.Push(S[i]);
                else if (ordered.Peek() == S[i])
                    toPlace.Push(S[i]);
                else
                {
                    while (toPlace.Count > 0 && toPlace.Peek() != S[i] && ordered.Peek() != ordered.Peek())
                        ordered.Push(ordered.Pop());

                    ordered.Push(S[i]);
                    //check ordered stack
                    //if ordered is empty add to stack

                    //else check if ordered.Peek() is equal curr
                    //if so add toOrder

                    //else add to toPlaceStack
                }
            }

            if (toPlace.Count > 0)
                return "";

            var result = new StringBuilder();

            while (ordered.Count > 0)
                result.Append(toPlace.Pop());

            return result.ToString();
        }



        /*
        A matrix is Toeplitz if every diagonal from top-left to bottom-right
        has the same element. Now given an M x N matrix, return True if and
        only if the matrix is  Toeplitz.

        Example 1:
        Input:
        matrix = [
          [1,2,3,4],
          [5,1,2,3],
          [9,5,1,2]
        ]
        Output: True
        Explanation:
        In the above grid, the diagonals are:
        "[9]", "[5, 5]", "[1, 1, 1]", "[2, 2, 2]", "[3, 3]", "[4]".
        In each diagonal all elements are the same, so the answer is True.
        */
        public static bool IsToeplitzMatrix(int[][] matrix)
        {
            //Iterate through each row greater than the fist (0)
            for (int row = 1; row < matrix.Length; row++)
            {
                //Iterate through each col greater than one
                for (int col = 1; col < matrix[0].Length; col++)
                {
                    //Now check the the adj (top left) item of the
                    //current item is the same as the current
                    if (matrix[row][col] != matrix[row - 1][col - 1])
                        return false;
                }
            }
            return true;
        }



        /*
        Given a non-empty string s and a dictionary wordDict containing a
        list of non-empty words, determine if s can be segmented into a
        space-separated sequence of one or more dictionary words.

        Note:

        The same word in the dictionary may be reused multiple times in the
        segmentation. You may assume the dictionary does not contain duplicate words.

        Example 1:
        Input: s = "leetcode", wordDict = ["leet", "code"]
        Output: true
        Explanation: Return true because "leetcode" can be segmented as "leet code".

        Example 3:
        Input: s = "catsandog", wordDict = ["cats", "dog", "sand", "and", "cat"]
        Output: false
        */
        public static bool WordBreak(string s, IList<string> wordDict)
        {
            if (s.Length == 0) return true;

            //given the curr string
            //go through each word in wordDict
            foreach (string w in wordDict)
            {
                //if S does not contains the current word, move to next
                if (!s.Contains(w))
                    continue;

                var newStrings = s.Split(w);

                if (newStrings.All(nw => WordBreak(nw, wordDict)))
                    return true;
            }
            return false;
        }

        /*
        public ListNode MergeTwoLists(ListNode l1, ListNode l2) 
        {
            if(l1 == null && l2 == null) return null;
 
            ListNode preHead = new ListNode(-1), curr = preHead;
            while(l1 != null && l2 != null)
            {
                if(l1.val < l2.val)
                {
                    curr.next = l1;
                    l1 = l1.next;
                }
                else
                {
                    curr.next = l2;
                    l2 = l2.next;
                }
                curr = curr.next;
            
            }
            curr.next = l1 == null ? l2 : l1;
            return preHead.next;
        }

        public ListNode MergeTwoLists(ListNode l1, ListNode l2) {
            if(l1 == null)
                return l2;
            if(l2 == null)
                return l1;
        
            if(l1.val < l2.val) {
                l1.next = MergeTwoLists(l1.next, l2);
                return l1;
            } else{
                l2.next = MergeTwoLists(l1, l2.next);
                return l2;
        }
    
        */




        public static bool IsAlienSorted(string[] words, string order)
        {
            var orderDict = new Dictionary<char, int>();
            int count = 0;
            foreach (char c in order)
                orderDict.Add(c, count++);

            if (words.Length == null || words.Length < 2) return true;

            //compare two words at a time. starting from left to right
            for (int i = 0; i < words.Length - 1; i++)
            {
                var word1 = words[i];
                var word2 = words[i + 1];
                //while the two words are in correct lexicographical order
                //continue to shift right until we reach the end
                if (!AreLexicographical(word1, word2, orderDict))
                    return false;
            }
            return true;
        }

        //write sep method to compare two words
        public static bool AreLexicographical(string w1, string w2, Dictionary<char, int> order)
        {
            if (w1.Equals(w2)) return true;

            //if w2 starts the same as w1, it should be first
            if (w1.StartsWith(w2))
                return false;

            var w2Idx = 0;
            //Compare each letter val one by one
            for (int i = 0; i < w1.Length - 1; i++)
            {
                //if we already processed all of w2
                //return false (it should be first)
                if (w2Idx >= w2.Length)
                    return true;

                //get num val of w1 char
                //get num val of w2 char
                var w1CharVal = order[w1[i]];
                var w2CharVal = order[w2[w2Idx++]];

                //if w1 > w2 return false
                if (w1CharVal > w2CharVal)
                    return false;
            }
            return true;
        }


        public static IList<IList<int>> ThreeSum2(int[] nums)
        {
            //TODO add memoization to improve speed
            //create results
            var results = new List<IList<int>>();
            var t = new List<int>();
            t.Prepend(1);
            //check edge cases
            if (nums.Length == 0) return results;

            //iterate through nums
            for (int aIdx = 0; aIdx < nums.Length; aIdx++)
            {
                //Given curA =  element a
                var curA = nums[aIdx];
                //We know a + bc = 0 || b + c = 0 - a
                //inner loop (start 1 idx over)
                for (int bIdx = aIdx + 1; bIdx < nums.Length; bIdx++)
                {
                    var curB = nums[bIdx];
                    //curB we know that out missing ele is C = 0 - curB - curA
                    var target = 0 - curA - curB;

                    //in inner elem look for C. If found add to list
                }

            }
            return results;
        }


        public static IList<string> RemoveInvalidParentheses(string s)
        {
            var results = new List<string>();

            //use stak to keep track of parenthsis
            var stack = new Stack<char>();

            //var result = new Prio()
            

        //iterate through string FROM BEGINNING
            for (int i = 0; i < s.Length; i++)
            {

                //If open parenthesis is encountered add to stack

                //If close parenthesis is encountered. Check stack
                //if it is empty or does not contain matching opening
                //parenthesis. mark it for removal
            }
            //at end, check if there are any extra openeing parenthesis
            //if so mark them for removal
            return results;
        }

        /*
    Remove the minimum number of invalid parentheses in order 
    to make the input string valid. Return all possible results.

    Note: The input string may contain letters other than the parentheses ( and ).
*/
        public static IList<string> RemoveInvalidParentheses2(string s)
        {
            var results = new List<string>();

            var orig = RemoveInvalidParentheses(s, false);
            var rev = RemoveInvalidParentheses(s, true);

            results.Add(orig);
            results.Add(rev);

            return results;
        }

        public static string RemoveInvalidParentheses(string s, bool reverse)
        {
            char lookFor = reverse ? '(' : ')';
            char skip = reverse ? ')' : '(';
            var res = new StringBuilder(s.Length);

            //use stak to keep track of parenthsis
            var stack = new Stack<char>();

            //iterate through string FROM BEGINNING
            foreach (char c in s)
            {
                if (c != lookFor)
                {
                    res.Append(c);
                    //If open parenthesis is encountered add to stack
                    if (c == skip)
                        stack.Push(c);
                }

                //If close parenthesis is encountered. 
                else if (c == lookFor) //redudant?
                {
                    //Check stack, if it is empty or does not contain matching opening
                    //parenthesis. mark it for removal
                    if (stack.Count > 0)
                    {
                        stack.Pop();
                        res.Append(c);
                    }
                }
            }

            var result = res.ToString();
            //at end, check if there are any extra openeing parenthesis
            //if so mark them for removal
            while (stack.Count > 0)
                result = result.Remove(result.IndexOf(stack.Pop()), 1);

            return result;
        }

        /*
        Given a non-empty string s, you may delete at most one character. 
        Judge whether you can make it a palindrome.
        */
        public static bool ValidPalindrome(string s)
        {
            //check for null or 1 length string
            if (string.IsNullOrWhiteSpace(s) || s.Length < 2)
                return true;

            //count num of changes (int or bool)
            bool changeMade = false;
            //create 2 idxs for s. One at beginning & one at 3nd
            int sIdx = 0, eIdx = s.Length - 1;

            //while start < end
            while (sIdx < eIdx)
            {
                char startChar = s[sIdx], endChar = s[eIdx];
                //check if curr startChar == endChar
                //if not and # of changes < 1
                if (startChar != endChar)
                {
                    Console.WriteLine($"Char {startChar} at {sIdx} doesnt equal char {endChar} at {eIdx}");
                    if (changeMade) return false;

                    //Check if increment st by 1 equal endChar
                    //if so, make change and update # of changes
                    if (s[sIdx + 1] == endChar)
                        sIdx++;
                    //else if decrementing endChar == startChar
                    //make change and update # of changes
                    else if (startChar == s[eIdx - 1])
                        eIdx--;
                    //Else, return false, since 1 change is not enough
                    else
                        return false;

                    changeMade = true;
                }
                else
                {
                    //remender to increment and decrement
                    sIdx++;
                    eIdx--;
                }
            }
            return true;
        }


        /*
        Find the kth largest element in an unsorted array. Note that it is 
        the kth largest element in the sorted order, not the kth distinct element.
        */
        public static int FindKthLargest_2(int[] nums, int k)
        {
            //Order list
            return nums.OrderByDescending(i => i).Skip(k - 1).First();
        }

        public static int FindKthLargest(int[] nums, int k)
        {
            //return nums.OrderByDescending(i => i).Skip(k - 1).First();
            int l = 0, r = nums.Length - 1, targetIdx = nums.Length - k;

            while (l < r)
            {
                var partitionIdx = Partition(nums, l, r);
                if (partitionIdx == targetIdx)
                    return nums[targetIdx];

                if (partitionIdx < targetIdx)
                    l = partitionIdx + 1;
                else
                    r = partitionIdx - 1;
            }
            return nums[targetIdx];
        }

        public static int Partition(int[] nums, int leftIdx, int rigthIdx)
        {
            var pivotVal = nums[rigthIdx];
            for (int i = leftIdx; i < rigthIdx; i++)
            {
                if (nums[i] <= pivotVal)
                {
                    Swap(nums, leftIdx, i);
                    leftIdx++;
                }
            }
            Swap(nums, leftIdx, rigthIdx);
            return leftIdx;
        }

        public static void Swap(int[] nums, int source, int dest)
        {
            if (source != dest)
            {
                var temp = nums[dest];
                nums[dest] = nums[source];
                nums[source] = temp;
            }
        }

        public static int[] ProductExceptSelf(int[] nums)
        {
            var l2r = new Dictionary<int, int>() { { 0, nums[0] } };
            for (int i = 1; i < nums.Length; i++)
                l2r.Add(i, l2r[i - 1] * nums[i]);

            var r2l = new Dictionary<int, int>() { { nums.Length - 1, nums[nums.Length - 1] } };
            for (int r = nums.Length - 2; r >= 0; r--)
                r2l.Add(r, nums[r] * r2l[r + 1]);

            //create result array
            var results = new int[nums.Length];
            results[0] = r2l[1];
            results[nums.Length - 1] = l2r[nums.Length - 2];
            for (int i = 1; i < nums.Length - 1; i++)
                results[i] = l2r[i - 1] * r2l[i + 1];

            return results;
        }

        public static int SubarraySum(int[] nums, int k)
        {
            if (nums == null) return 0;
            //have counter
            int count = 0, sum = 0, leftIdx = 0;

            var sums = new int[nums.Length + 1];
            sums[0] = 0;
            for (int i = 1; i <= nums.Length; i++)
                sums[i] = sums[i - 1] + nums[i - 1];

            for (int sIdx = 0; sIdx < nums.Length; sIdx++)
            {
                for (int eIdx = sIdx + 1; eIdx <= nums.Length; eIdx++)
                {
                    if (sums[eIdx] - sums[sIdx] == k)
                        count++;
                }
            }

            return count;
        }

        public static int FindPeakElement(int[] nums)
        {
            //handle edge cases
            if (nums.Length < 2)
                return 0;

            //iterate through arr w/2 pointers
            //curr (i)
            //next (i +1)
            for (int i = 0; i < nums.Length - 1; i++)
            {
                int curr = nums[i], next = nums[i + 1];
                //check if curr is > next
                if (curr > next)
                    return i; //if so return            
            }

            //check last element
            if (nums[nums.Length - 1] > nums[nums.Length - 2])
                return nums.Length - 1;

            return 0;
        }


        public static int CalculateVal(string s)
        {
            var result = 0;
            if (!s.Contains("-") && !s.Contains("+") && !s.Contains("*") && !s.Contains("/"))
                return 0;

            if (s.Contains("+"))
                result = s.Split("+", StringSplitOptions.RemoveEmptyEntries).Aggregate( Aggregate(0, (sum, n) => sum + CalculateVal(n));
            //1st: split string based on + 

            //2ns split on - 

            //for each split string get val
            //add vals

            //3nd: split on *

            //4th spit on /

            return 0;
        }


    }
}
