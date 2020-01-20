using System;
using System.Collections.Generic;
using System.Linq;
//using CrackingTheCodingInterviewProblems.Common.Extensions;

namespace CrackingTheCodingInterviewProblems.Common
{
    public static class SortingAlgorithms
    {
        #region MergeSort
        public static int[] TestMergeSort()
        {
            
            var input = new int[] { 8, 5, 20, 305, 1,  2, 3, 2 };
            input.ToStringConsole();
            Console.WriteLine($"Input array: {input}");
            var result = MergeSort(input);

            Console.WriteLine($"Sorted results: {result}");
            result.ToStringConsole();
            return result;
        }

        public static int[] MergeSort(int[] unsortedArray)
        {
            var result = MergeSort_Split(unsortedArray);
            return result;
        }

        private static int[] MergeSort_Split(int[] array)
        {
            if (array.Length <= 1) return array;

            int unitsToMidPoint = array.Length / 2;
            var left = array.Take(unitsToMidPoint).ToArray();
            var right = array.Skip(unitsToMidPoint).Take(array.Length - unitsToMidPoint).ToArray();

            //recursively call method
            return MergeSort_Merge(MergeSort_Split(left), MergeSort_Split(right));
        }

        private static int[] MergeSort_Merge(int[] left, int[] right)
        {
            var result = new List<int>();

            while(left.Length > 0 || right.Length > 0)
            {
                if (left.Length > 0 && right.Length > 0)
                {
                    if(left[0] < right[0])
                    {
                        result.Add(left[0]);
                        left = left.Skip(1).ToArray();
                    } else
                    {
                        result.Add(right[0]);
                        right = right.Skip(1).ToArray();
                    }
                }
                else if (left.Length == 0)
                {
                    result.Add(right[0]);
                    right = right.Skip(1).ToArray();
                }
                else
                {
                    result.Add(left[0]);
                    left = left.Skip(1).ToArray();
                }
            }
            return result.ToArray();
        }
        #endregion

        #region QuickSort
        public static int[] TestQuickSort()
        {
            var input = new int[] { 3, 5, 1, 0, 40, 2, 2305, 0, 1, 9, 3, 8 };
            Console.WriteLine($"Input array:");
            input.ToStringConsole();
            var result = QuickSort(input);

            Console.WriteLine($"Sorted results:");
            result.ToStringConsole();
            return result;
        }

        public static int[] QuickSort(int[] arr)
        {
            QuickSort(arr, 0, arr.Length - 1);
            return arr;
        }

        private static void QuickSort(int[] arr, int leftIdx, int rightIdx)
        {
            Console.WriteLine($"\n\nCalling QuickSort w/lIdx {leftIdx} and rIdx {rightIdx}");
            if (arr.Length < 2) return;

            var pivot = Partition(arr, leftIdx, rightIdx);
            Console.WriteLine($"RETURNED partition (w/pivot idx {pivot} and val {arr[pivot]}:");
            arr.ToStringConsole();

            if(leftIdx < pivot -1)
            {
                //Console.WriteLine($"\n\nCalling QuickSort w/lIdx {leftIdx} and rIdx {pivot - 1}");
                QuickSort(arr, leftIdx, pivot - 1);
            }

            if(pivot + 1 < rightIdx)
            {
                //Console.WriteLine($"\n\nCalling QuickSort w/lIdx {pivot + 1} and rIdx {rightIdx}");
                QuickSort(arr, pivot + 1, rightIdx);
            }
        }

        private static int Partition(int[] arr, int l, int r)
        {
            var pivot = r--;
            Console.WriteLine($"*** Current pivot idx: {pivot} (val {arr[pivot]}) ***");

            while(l <= r)
            {
                while (arr[l] <= arr[pivot] && l < pivot)
                {
                    Console.WriteLine($"+lIdx: {l} w/val {arr[l]} is less/equal to pivot {arr[pivot]}. Increasing lIdx to {l + 1}");
                    l++;
                }

                while (arr[r] > arr[pivot] && r >= 0)
                {
                    Console.WriteLine($"-rIdx: {r} w/val {arr[r]} is greater than pivot {arr[pivot]}. Decreasing rIdx to {r - 1}");
                    r--;
                }
            
                if(l < r)
                {
                    Console.WriteLine($"Swapping lIdx: {l} w/val {arr[l]} AND rIdx: {r} w/val {arr[r]}.");
                    Swap(arr, l, r);
                    l++;
                    r--;
                    Console.WriteLine($"Update indexes to: l {l},  r {r}");
                }
            }

            Console.WriteLine($"Done arraging around pivot at index {pivot} (val {arr[pivot]}). Swapping with lIdx {l} (val {arr[l]})");
            Swap(arr, l, pivot);
            Console.WriteLine($"Returing new pivot at index {l} with val {arr[l]}");
            return l;
        }

        private static void Swap(int[] arr, int leftIdx, int rigthIdx)
        {
            var temp = arr[leftIdx];
            arr[leftIdx] = arr[rigthIdx];
            arr[rigthIdx] = temp;
        }
        #endregion
    }
}
