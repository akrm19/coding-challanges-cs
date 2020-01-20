using System;
using System.Collections;
using System.Linq;

namespace CrackingTheCodingInterviewProblems.Common
{
    public static class Extensions
    {
        public static void ToStringConsole(this int[] arr)
        {
            Array.ForEach(arr, i => Console.Write($" {i},"));
        }

        public static void ToStringConsole(this IList arr)
        {
            foreach(var i in arr)
                Console.Write($" {i},");
        }
    }
}
