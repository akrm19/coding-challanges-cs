using System;
using System.Collections.Generic;

namespace CrackingTheCodingInterviewProblems.LeetCode
{
    public static class Algorithms_Medium
    {
        #region Common Structures
        public class LeetCodeListNode
        {
            public int val;
            public LeetCodeListNode next;
            public LeetCodeListNode(int x) { val = x; }
        }
        #endregion

        #region Add Two Numbers (represent by int linked lists)
        /// <summary>
        /// You are given two non-empty linked lists representing two
        /// non-negative integers.The digits are stored in reverse order
        /// and each of their nodes contain a single digit.Add the two
        /// numbers and return it as a linked list.
        /// You may assume the two numbers do not contain any leading zero,
        /// except the number 0 itself.
        ///
        /// Example:
        /// Input: (2 -> 4 -> 3) + (5 -> 6 -> 4)
        /// Output: 7 -> 0 -> 8
        /// Explanation: 342 + 465 = 807.
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public static LeetCodeListNode AddTwoNumbers(LeetCodeListNode l1, LeetCodeListNode l2)
        {
            int carryOver = 0;
            var sum = l1.val + l2.val;

            if (sum > 9)
            {
                carryOver++;
                sum %= 10;
            }

            LeetCodeListNode head = new LeetCodeListNode(sum);
            AddTwoNumbers(l1?.next, l2?.next, carryOver, head);

            return head;
        }

        private static void AddTwoNumbers(LeetCodeListNode l1, LeetCodeListNode l2, int carry, LeetCodeListNode prevNode)
        {
            if (l2 == null && l1 == null && carry == 0) return;

            var sum = (l1 == null ? 0 : l1.val) + (l2 == null ? 0 : l2.val) + carry;

            if (sum > 9)
            {
                carry = 1;
                sum %= 10;
            }
            else
                carry = 0;

            var next = new LeetCodeListNode(sum);
            prevNode.next = next;
            AddTwoNumbers(l1?.next, l2?.next, carry, next);
        }
        #endregion


        #region Longest Substring Without Repeating Characters
        /// <summary>
        ///  Given a string, find the length of the longest substring
        ///  without repeating characters.
        ///
        /// Ex:
        /// Input: "abcabcbb" Output: 3
        /// Explanation: The answer is "abc", with the length of 3.
        ///
        /// Input: "abcabcbb" Output: 3
        /// Explanation: The answer is "abc", with the length of 3.
        ///
        /// Input: "pwwkew" Output: 3
        /// Explanation: The answer is "wke", with the length of 3.
        /// Note: that the answer must be a substring, "pwke" is a
        /// 
        /// subsequence and not a substring.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int LengthOfLongestSubstring(string s)
        {
            var hash = new Dictionary<char, int>();
            int longestSubs = 0, currentSubs = 0, lastRepeatIdx = 0;

            for (int i = 0; i < s.Length; i++)
            {
                if (hash.ContainsKey(s[i]))
                {
                    lastRepeatIdx = Math.Max(lastRepeatIdx, hash[s[i]] + 1);
                    hash[s[i]] = i;
                }
                else
                    hash.Add(s[i], i);

                currentSubs = Math.Max(currentSubs, i - lastRepeatIdx + 1);
            }
            return currentSubs;
            //var hash = new Dictionary<char, int>();
            //int longestSubs = 0, currentSubs = 0, lastRepeatIdx = 0;

            //for (int i = 0; i < s.Length; i++)
            //{
            //    if (hash.ContainsKey(s[i]))
            //    {
            //        lastRepeatIdx = hash[s[i]] + 1;
            //        hash[s[i]] = i;

            //        var newcurrentSubs = Math.Max(0, currentSubs - (i - (lastRepeatIdx + 1)));
            //        Console.WriteLine($"Idx: {i}, let:{s[i]}, lastRepeatIdx: {lastRepeatIdx}, presCurrSub: {currentSubs}, newCurrSub{newcurrentSubs}");
            //        currentSubs = newcurrentSubs;
            //    }
            //    else
            //        hash.Add(s[i], i);

            //    currentSubs += 1;
            //    if (currentSubs > longestSubs)
            //        longestSubs = currentSubs;

            //    Console.WriteLine($"End of {i} iteration. currentSubs: {currentSubs}");
            //}
            //return longestSubs;
        }

        public static void LengthOfLongestSubstring_Test()
        {
            var input = "tmmzuxt";
            var result = LengthOfLongestSubstring(input);

        }
        #endregion

        #region Longest Palindromic Substring
        /// <summary>
        /// Given a string s, find the longest palindromic substring in s.
        /// You may assume that the maximum length of s is 1000.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string LongestPalindrome(string s)
        {
            if (s.Length < 2) return s;
            //Given X string, find palindrome, record length if bigger

            //Dictionary of char we seen, and where
            var occurances = new Dictionary<char, List<int>>();
            string longest = "";


            //Traverse string.
            for (int i = 0; i < s.Length; i++)
            {
                //if we have not seen char, add to dictionary, continue
                if (!occurances.ContainsKey(s[i]))
                    occurances.Add(s[i], new List<int> { i });
                //if we seen char:
                else
                {
                    //For each place we seen char, check if its substring of it 
                    //and current char are palindrom.
                    foreach (var idx in occurances[s[i]])
                    {
                        //If so add, check possible lenght. If bigger set it as longest
                        var potentialLength = (i - idx) + 1;
                        if (potentialLength > longest.Length && IsPalindrome(s, idx, i))
                        {
                            longest = s.Substring(idx, potentialLength);
                        }
                    }
                    occurances[s[i]].Add(i);
                }
            }

            //return biggest length
            return longest.Length == 0 ? s[0].ToString() : longest;
        }

        //Create method that take an array, 2 indexes, and check it palindrome
        //Optional: have memo table
        public static bool IsPalindrome(string s, int l, int r)
        {
            while (l < r)
            {
                if (s[l++] != s[r--])
                    return false;
            }
            return true;
        }
        #endregion

        #region Nim Game


        #endregion
        /*

        You are playing the following Nim Game with your friend:
        There is a heap of stones on the table, each time one of you take
        turns to remove 1 to 3 stones.The one who removes the last stone
        will be the winner.You will take the first turn to remove the stones.

        Both of you are very clever and have optimal strategies for the game.
        Write a function to determine whether you can win the game given the
        number of stones in the heap.


        Example:

        Input: 4
        Output: false 
        Explanation:
        If there are 4 stones in the heap, then you will never win the game;
        No matter 1, 2, or 3 stones you remove, the last stone will always be
        removed by your friend.


        */
        public static bool CanWinNim(int n)
        {
            return CanWinNim(n, true);
        }

        public static bool CanWinNim(int n, bool myTurn)
        {
            //Check if I can win on my turn
            //If there is 1 to 3 stones left I can win
            if (n >= 0 && n <= 3)
                return myTurn;

            //check if I will loose no matter what in the next move
            var mov1 = n - 1;
            var mov2 = n - 2;
            var mov3 = n - 3;

            var sortedList = new SortedList<int, int>();
            
            // if((mov3 <= 3  && mov3 > 0) && (mov2 <= 3 && mov2 > 0) && (mov1 <= 3 && mov1 > 0))
            //     return !myTurn;

            return CanWinNim(mov3, !myTurn) || CanWinNim(mov2, !myTurn) || CanWinNim(mov1, !myTurn);
        }
        /*

        The Hamming distance between two integers is the number of positions
        at which the corresponding bits are different.
        Given two integers x and y, calculate the Hamming distance.

        Note: 0 ≤ x, y< 231.

        Example:
        Input: x = 1, y = 4
        Output: 2

        Explanation:
        1   (0 0 0 1)
        4   (0 1 0 0)
                ↑   ↑

        The above arrows point to positions where the corresponding bits are different.
        */
    }
}
