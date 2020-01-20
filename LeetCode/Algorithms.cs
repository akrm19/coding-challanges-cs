using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrackingTheCodingInterviewProblems.Common;

namespace CrackingTheCodingInterviewProblems.LeetCode
{
    public static class Algorithms
    {
        #region TwoSum
        /// <summary>
        /// Given an array of integers, return indices of
        /// the two numbers such that they add up to a specific target.
        /// You may assume that each input would have exactly one solution,
        /// and you may not use the same element twice.
        /// Ex:
        /// Given nums = [2, 7, 11, 15], target = 9,
        /// Because nums[0] + nums[1] = 2 + 7 = 9,
        /// return [0, 1].
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int[] TwoSum_v2(int[] nums, int target)
        {
            var existingVals = new Dictionary<int, int>();

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] > target) continue;

                //Removing neededVal and accessing nums[i]
                //directly improves performance to 99.7%
                int neededVal = target - nums[i]; 
                if (existingVals.ContainsKey(neededVal))
                    return new int[] { i, existingVals[neededVal] };

                existingVals.TryAdd(nums[i], i);
            }
            return new int[] { };
        }
        #endregion

        #region Reverse Int
        public static int ReverseInt_v1(int x)
        {
            var num = x > 0 ? x.ToString() : x.ToString().Trim('-');
            double result = 0;

            for (int i = num.Length - 1; i >= 0; i--)
            {
                var temp = double.Parse(num[i].ToString()) * Math.Pow(10, i);
                Console.WriteLine($"i = {i}. Adding {(int)temp} to {result}");
                result += double.Parse(num[i].ToString()) * Math.Pow(10, i);
            }

            result = x < 0 ? result * -1 : result;
            return (result <= Int32.MinValue || result >= Int32.MaxValue)
                ? 0
                : (int)result;
        }

        public static int ReverseInt_v2(int x)
        {
            var num = x > 0 ? x.ToString() : x.ToString().Trim('-');
            var resultStr = new StringBuilder();

            for (int i = num.Length - 1; i >= 0; i--)
                resultStr.Append(num[i]);

            double result = x > 0
                ? double.Parse(resultStr.ToString())
                : double.Parse(resultStr.ToString()) * -1;
            return (result <= Int32.MinValue || result >= Int32.MaxValue)
                ? 0
                : (int)result;
        }
        #endregion

        #region IsPalindrome_Int
        public static bool IsPalindrome(int x)
        {
            if (x < 0) return false;
            var num = x.ToString();
            var endIdx = num.Length - 1;
            for (int i = 0; i < endIdx; i++)
            {
                if (num[i] != num[endIdx])
                    return false;
                endIdx--;
            }
            return true;
        }
        #endregion

        #region LongestCommonPrefix
        public static string LongestCommonPrefix(string[] strs)
        {
            if (strs.Count() == 1) return strs[0];

            int prefixCount = 0, minLength = int.MaxValue;
            foreach (var str in strs)
                if (str.Length < minLength)
                    minLength = str.Length;

            if (minLength == 0 || minLength == int.MaxValue) return "";

            for (int i = 0; i < minLength; i++)
            {
                if (strs.Any(s => s[i] != strs[0][i]))
                    return strs[0].Substring(0, prefixCount);
                prefixCount++;
            }
            return strs[0].Substring(0, prefixCount);
        }
        #endregion

        #region ValidParentheses
        /// <summary>
        /// Given a string containing just the characters '(', ')', '{', '}',
        /// '[' and ']', determine if the input string is valid.
        ///
        /// An input string is valid if:
        /// Open brackets must be closed by the same type of brackets.
        /// Open brackets must be closed in the correct order.
        /// Note that an empty string is also considered valid.
        ///
        /// Ex:
        /// Input: "()[]{}" Output: true
        /// Input: "(]"     Output: false
        /// Input: "([)]"   Output: false
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool ValidParentheses(string s)
        {
            if (s.Length % 2 != 0) return false;

            var stack = new Stack<char>();
            char prev;
            for (int i = 0; i < s.Length; i++)
            {
                if (IsClosingBracket(s[i]))
                {
                    if (!stack.TryPop(out prev) || !IsCorrectClosingBracket(prev, s[i]))
                        return false;
                }
                else
                    stack.Push(s[i]);
            }
            return stack.Count == 0;
        }

        public static bool IsClosingBracket(char c)
        {
            return (c == ')' || c == ']' || c == '}');
        }

        public static bool IsOpeningBracket(char c)
        {
            return (c == '(' || c == '[' || c == '{');
        }

        public static bool IsCorrectClosingBracket(char open, char close)
        {
            switch (open)
            {
                case '(':
                    return close == ')';
                case '{':
                    return close == '}';
                case '[':
                    return close == ']';
                default:
                    return false;
            }
        }

        #endregion

        #region MergerTwoSortedLists

            //Definition for singly-linked list.
        public class LeetCodeListNode {
                public int val;
                public LeetCodeListNode next;
                public LeetCodeListNode(int x) { val = x; }
        }


        /// <summary>
        /// Merge two sorted linked lists and return it as a new list.
        /// The new list should be made by splicing together the nodes of the first two lists.
        /// Ex:
        /// Input: 1->2->4, 1->3->4
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public static LeetCodeListNode MergeTwoLists(LeetCodeListNode l1, LeetCodeListNode l2)
        {
            LeetCodeListNode curr = null, head = null, next = null;// = new ListNode();

            while (l1 != null || l2 != null)
            {
                if (l1?.val > l2?.val)
                {
                    next = new LeetCodeListNode(l1.val);
                    l1 = l1.next;
                }
                else
                {
                    next = new LeetCodeListNode(l2.val);
                    l2 = l2.next;
                }

                if (head == null)
                {
                    head = next;
                    curr = next;
                }
                else
                {
                    curr.next = next;
                    curr = curr.next;
                }
            }

            return l1;
        }
        #endregion

        #region Remove Sorted Array Dups
        /// <summary>
        /// Given a sorted array nums, remove the duplicates
        /// in-place such that each element appear only once
        /// and return the new length. Do not allocate extra
        /// space for another array, you must do this by modifying
        /// the input array in-place with O(1) extra memory.
        ///
        /// Ex:
        /// Given nums = [1,1,2], Your function should return length = 2,
        /// with the first two elements of nums being 1 and 2 respectively.
        /// It doesn't matter what you leave beyond the returned length.
        ///
        /// Ex2:
        /// Given nums = [0,0,1,1,1,2,2,3,3,4], Your function should
        /// return length = 5, with the first five elements of nums
        /// being modified to 0, 1, 2, 3, and 4 respectively.
        ///
        /// It doesn't matter what values are set beyond the returned length.
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int RemoveDuplicates(int[] nums)
        {
            int length = nums.Length;
            if (length < 2) return length;

            int prevIdx = 0;
            for (int i = 1; i < length; i++)
            {
                if (nums[i] != nums[prevIdx])
                    nums[++prevIdx] = nums[i];
            }
            return prevIdx + 1;
        }

        public static int RemoveDuplicates_Slow(int[] nums)
        {
            if (nums.Length < 2) return nums.Length;

            int dups = 0, tempDups = 0;
            for (int i = nums.Length - 1; i > 0; i--)
            {
                var origI = i;
                while (i > 0 && nums[i] == nums[i - 1])
                {
                    tempDups++;
                    i--;
                }

                if (tempDups > 0)
                {
                    ShiftElementsDown(nums, origI, (nums.Length - dups - 1), tempDups);
                    dups += tempDups;
                    tempDups = 0;
                }
            }
            return nums.Length - dups;
        }

        private static void ShiftElementsDown(int[] arr, int startIdx, int endIdx = -1, int positionsToShift = 1)
        {
            for (int i = startIdx; i <= endIdx; i++)
                arr[i - positionsToShift] = arr[i];
        }
        #endregion

        #region Remove From Array (in-place)
        /// <summary>
        /// Given an array nums and a value val, remove all instances of
        /// that value in-place and return the new length.
        /// Do not allocate extra space for another array, you must do this
        /// by modifying the input array in-place with O(1) extra memory.
        /// The order of elements can be changed. It doesn't matter what you
        /// leave beyond the new length.
        ///
        /// Ex:
        /// Given nums = [3,2,2,3], val = 3, Your function should
        /// return length = 2, with the first two elements of nums being 2.
        /// It doesn't matter what you leave beyond the returned length.
        ///
        /// Ex2:
        /// Given nums = [0,1,2,2,3,0,4,2], val = 2,
        /// Your function should return length = 5, with the first five
        /// elements of nums containing 0, 1, 3, 0, and 4.
        /// Note that the order of those five elements can be arbitrary.
        /// It doesn't matter what values are set beyond the returned length.
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int RemoveElement(int[] nums, int val)
        {
            int valIdx = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] != val)
                    nums[valIdx++] = nums[i];
            }
            return valIdx;
        }
        #endregion

        #region Find SubString in String
        /// <summary>
        /// Return the index of the first occurrence of needle in haystack,
        /// or -1 if needle is not part of haystack.
        ///
        /// Ex:
        /// Input: haystack = "hello", needle = "ll" Output: 2
        /// </summary>
        /// <param name="haystack"></param>
        /// <param name="needle"></param>
        /// <returns></returns>
        public static int FindSubString(string haystack, string needle)
        {
            return 0;
        }

        public static int BoyerMooreSearch(string pattern, string toSearch)
        {
            if (string.IsNullOrWhiteSpace(toSearch)) return -1;

            var badMatchTbl = new BadMatchTable(pattern);
            var currentIdx = 0;

            while(currentIdx <= toSearch.Length - pattern.Length)
            { 
                int charsLeftToMatch = pattern.Length - 1;

                while (charsLeftToMatch >= 0 && pattern[charsLeftToMatch] == toSearch[currentIdx + charsLeftToMatch])
                    charsLeftToMatch--;

                if (charsLeftToMatch < 0)
                    return currentIdx;

                currentIdx += badMatchTbl[toSearch[currentIdx + pattern.Length - 1]];
            }

            return -1;
        }

        private class BadMatchTable
        {
            private readonly int defaultVal;
            private Dictionary<int, int> distanceToSkip;

            public BadMatchTable(string pattern)
            {
                defaultVal = pattern.Length;
                distanceToSkip = new Dictionary<int, int>();

                for (int i = 0; i < pattern.Length; i++)
                    distanceToSkip[pattern[i]] = pattern.Length - i - 1;
            }

            public int this[int index]
            {
                get
                {
                    return distanceToSkip.ContainsKey(index)
                        ? distanceToSkip[index]
                        : defaultVal;
                }
            }
        }
        #endregion

        #region Find Max SubArray
        /// <summary>
        /// Given an integer array nums, find the contiguous subarray
        /// (containing at least one number) which has the largest sum
        /// and return its sum.
        ///
        /// Ex:
        /// Input: [-2,1,-3,4,-1,2,1,-5,4]  Output: 6
        /// Explanation: [4,-1,2,1] has the largest sum = 6.
        ///
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int MaxSubArray(int[] nums)
        {
            for (int i = 1; i < nums.Length; i++)
            {
                //if(nums[i-1] >= 0)
                //    nums[i] = nums[i] + nums[i-1];
                nums[i] = Math.Max(nums[i], nums[i] + nums[i - 1]);
            }
            return nums.Max();
            //return GetSubArrayVal_v2(nums, 1, nums[0]);
        }

        /// <summary>
        /// Bad & slow solution, non-working solution. Do not use.
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="startIdx"></param>
        /// <param name="currMax"></param>
        /// <returns></returns>
        private static int GetSubArrayVal_v2(int[] arr, int startIdx, int currVal)
        {
            if (startIdx >= arr.Length)
                return currVal;

            int subArrVal = Math.Max(arr[startIdx], currVal + arr[startIdx]);
            //int subArrVal = currVal + arr[startIdx];
            //if (subArrVal < arr[startIdx])
            //    subArrVal = arr[startIdx];
            Console.WriteLine($"startIdx: {startIdx}. currVal {currVal}. SubArrVal: {subArrVal}");

            return Math.Max(subArrVal, GetSubArrayVal_v2(arr, startIdx + 1, subArrVal));
        }

        public static void MaxSubArray_Test()
        {
            var input = new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4};
            Console.Write("Input: ");
            input.ToStringConsole();
            Console.WriteLine();
            var result = MaxSubArray(input);
            Console.WriteLine($"\n\nResult: {result}. Expected output: 6");
        }
        #endregion


        #region Lenght Of Last Word

        /// <summary>
        /// Given a string s consists of upper/lower-case alphabets and
        /// empty space characters ' ', return the length of last word
        /// in the string. If the last word does not exist, return 0.
        /// Note: A word is defined as a character sequence consists
        /// of non-space characters only.
        ///
        /// Example:
        /// Input: "Hello World"  Output: 5
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int LengthOfLastWord(string s)
        {
            var words = s.Split(' ');
            if (words.Length < 1) return 0;

            return words.Last().Length;
        }
        #endregion

        #region PLusOne For Num As Int Array
        /// <summary>
        /// Given a non-empty array of digits representing a non-negative
        /// integer, plus one to the integer. The digits are stored such
        /// that the most significant digit is at the head of the list,
        /// and each element in the array contain a single digit. You may
        /// assume the integer does not contain any leading zero, except
        /// the number 0 itself.
        ///
        /// Ex:
        /// Input: [1,2,3] Output: [1,2,4]
        /// Explanation: The array represents the integer 123.
        /// </summary>
        /// <param name="digits"></param>
        /// <returns></returns>
        public static int[] PlusOne(int[] digits)
        {
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                if (digits[i] < 9)
                {
                    digits[i]++;
                    return digits;
                }
                else
                {
                    digits[i] = 0;
                }
            }
            Math.Sqrt(2);
            var newResult = digits.ToList();
            newResult.Insert(0, 1);
            return newResult.ToArray();
        }
        #endregion

        #region Num Of Possible Ways To Reach top
        /// <summary>
        /// You are climbing a stair case. It takes n steps to reach
        /// to the top. Each time you can either climb 1 or 2 steps.
        /// In how many distinct ways can you climb to the top?
        /// Note: Given n will be a positive integer.
        ///
        /// Ex:
        /// Input: 3 Output: 3
        /// Explanation: There are three ways to climb to the top.
        /// 1. 1 step + 1 step + 1 step
        /// 2. 1 step + 2 steps
        /// 3. 2 steps + 1 step
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int ClimbStairs(int n)
        {
            var cache = new Dictionary<int, int>();
            cache.Add(1, 1);
            cache.Add(2, 2);
            cache.Add(3, 3);

            return ClimbStairs(cache, n);
        }

        public static int ClimbStairs(Dictionary<int, int> cache, int num)
        {
            int val;
            if (cache.TryGetValue(num, out val))
                return val;


            val = ClimbStairs(cache, num - 1) + ClimbStairs(cache, num - 2);
            cache.Add(num, val);
            return val;
        }
        #endregion

        #region Merger Sorted Array (int)
        /// <summary>
        /// Given two sorted integer arrays nums1 and nums2, merge nums2 into
        /// nums1 as one sorted array.
        /// Note: The number of elements initialized in nums1 and nums2
        /// are m and n respectively. You may assume that nums1 has enough
        /// space (size that is greater or equal to m + n) to hold additional
        /// elements from nums2.
        ///
        /// Ex:
        /// Input:
        /// nums1 = [1,2,3,0,0,0], m = 3
        /// nums2 = [2,5,6],       n = 3
        /// Output: [1,2,2,3,5,6]
        /// </summary>
        /// <param name="nums1"></param>
        /// <param name="m"></param>
        /// <param name="nums2"></param>
        /// <param name="n"></param>
        public static void Merge(int[] nums1, int m, int[] nums2, int n)
        {
            var joinSize = m-- + n--;

            for (int i = joinSize - 1; i >= 0; i--)
            {
                if (n < 0)
                    return;
                if (m < 0 || nums1[m] <= nums2[n])
                {
                    nums1[i] = nums2[n];
                    n--;
                }
                else
                {
                    nums1[i] = nums1[m];
                    m--;
                }
            }
        }
        #endregion

        #region Same (Binary) Tree
        ///**
        // * Definition for a binary tree node.
        public class LeetCodeTreeNode {
            public int val;
            public LeetCodeTreeNode left;
            public LeetCodeTreeNode right;
            public LeetCodeTreeNode(int x) { val = x; }
        }

        public class Solution
        {
            public bool IsSameTree(LeetCodeTreeNode p, LeetCodeTreeNode q)
            {
                if (p == null)
                    return q == null;

                if (q == null)
                    return false;

                if (p.val != q.val)
                    return false;

                return IsSameTree(p.left, q.left) && IsSameTree(p.right, q.right);
            }
        }
        #endregion
    }
}
