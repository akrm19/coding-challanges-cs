using System;
using System.Collections.Generic;
using System.Drawing;

namespace CrackingTheCodingInterviewProblems
{
    public static  class Sec8_RecursionDynamicProg
    {
        #region Fibonacci
        /// <summary>
        /// Calculates the i-th fibonacci number.
        /// Simple but very inefficient solution
        /// </summary>
        /// <returns>The fibonacci.</returns>
        /// <param name="i">The index.</param>
        public static int Fibonacci(int i)
        {
            if (i < 2) return i;

            return Fibonacci(i - 1) + Fibonacci(i - 2);
        }

        public static int FibonacciGood(int i)
        {
            return FibonacciGood(i, new int[i + 1]);
        }

        public static int FibonacciGood(int i, int[] memo)
        {
            if (i < 2) return i;

            if (memo[i] == 0)
                memo[i] = FibonacciGood(i - 1, memo) 
                    + FibonacciGood(i - 2, memo);

            return memo[i];
        }

        public static int FibonacciBottomUp(int n)
        {
            if (n < 2) return n;

            int[] memo = new int[n];
            memo[0] = 0;
            memo[1] = 1;

            for (int i = 2; i < n; i++)
                memo[n] = memo[n - 1] + memo[n - 2];

            return memo[n - 1] + memo[n - 2];
        }

        /// <summary>
        /// Calculates Fib num for given int
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int Fibonacci_Recursion(int n)
        {
            // fib(n) = fib(n-1) + fib (n-2);
            return Fibonacci_Recursion(n, new int[n + 1]);
        }

        public static int Fibonacci_Recursion(int n, int[] memo)
        {
            if (n < 2) return n;

            if (memo[n] == 0)
                memo[n] = Fibonacci_Recursion(n - 1, memo) + Fibonacci_Recursion(n - 2, memo);

            return memo[n];
        }

        public static int Fibonacci_Iterative(int n)
        {
            if (n < 2) return n;

            var memo = new int[n + 1];
            memo[0] = 0;
            memo[1] = 1;

            for (int i = 2; i < n; i++)
                memo[i] = memo[i - 1] + memo[i - 2];

            return memo[n -1] + memo[n - 2];
        }
        #endregion

        #region Prob_8_1_TripleStep
        /// <summary>
        /// Probs the 8 1 triple step.
        /// A child is running up a staircase with n steps
        /// and can hop either 1, 2, or 3 steps at a time.
        /// Implement a method to count how many possible ways
        /// the child can run up the stairs.
        /// </summary>
        /// <returns>The 8 1 triple step.</returns
        public static int Prob_8_1_TripleStep(int numOfSteps)
        {
            //return CountWaysInefficient(numOfSteps);
            var memo = new int[numOfSteps + 1];
            Array.Fill(memo, -1);
            return CountWays(numOfSteps, memo);
        }

        public static int CountWaysInefficient(int x)
        {
            if (x < 0) return 0;

            if (x == 0) return 1;

            return CountWaysInefficient(x -1) + CountWaysInefficient(x - 2) + CountWaysInefficient(x- 3);
        }

        public static int CountWays(int x, int[] memo)
        {
            if (x < 0) return 0;
            if (x == 0) return 1;

            if (memo[x] == -1)
                memo[x] = CountWays(x - 1, memo) + CountWays(x - 2, memo) + CountWays(x - 3, memo);

            return memo[x];
        }

        public static int Prob_8_1_TripleStep_Temp(int steps)
        {
            if (steps < 0) return 0;

            if (steps == 0)
                return 1;

            return Prob_8_1_TripleStep_Temp(steps - 3) + Prob_8_1_TripleStep_Temp(steps - 2) + Prob_8_1_TripleStep_Temp(steps - 1);
        }

        public static int Prob_8_1_TripleStep_Temp_WithMemo(int steps)
        {
            var memo = new int[steps + 1];
            memo[1] = 1;
            memo[2] = 2;

            return Prob_8_1_TripleStep_Temp_WithMemo(steps, memo);
        }

        public static int Prob_8_1_TripleStep_Temp_WithMemo(int steps, int[] memo)
        {
            if (steps < 0) return 0;

            if (steps == 0)
                return 1;

            if(memo[steps] == 0)
                memo[steps] = Prob_8_1_TripleStep_Temp_WithMemo(steps - 3, memo)
                    + Prob_8_1_TripleStep_Temp_WithMemo(steps - 2, memo) + Prob_8_1_TripleStep_Temp_WithMemo(steps - 1, memo);

            return memo[steps];
        }
        #endregion

        #region Prob_8_2_RobotInGrid
        /// <summary>
        /// Probs the 8 2 robot in grid.
        /// IMagine a robot sitting on the upper left corner of a grid
        /// with r rows and c columns. The robot can only move in two
        /// directions, right and down, but certain cells are "off-limits"
        /// such that the robot cannot step on them. Design an algorithm 
        /// to find a path for the robot from the top left to the bottom right.
        /// </summary>
        /// <returns>The 8 2 robot in grid.</returns>
        /// <param name="columns">Columns.</param>
        /// <param name="rows">Rows.</param>
        public static int[] Prob_8_2_RobotInGrid(int columns, int rows)
        {
            var path = new List<Tuple<int, int>>();
            var grid = new int[columns, rows];

            if(Prob_8_2_RobotInGrid_TempIsValidPath(grid, rows - 1, columns - 1, path))
            {

            }
            
            return null;
        }
        
        public static bool Prob_8_2_RobotInGrid_TempIsValidPath(int[,] grid, int r, int c, List<Tuple<int, int>> path)
        {
            if (r < 0 || c < 0)
                return false;

            if (grid[r, c] == -1) //invalid path
                return false;

            if (r == 0 && c == 0)
                return true;

            if (Prob_8_2_RobotInGrid_TempIsValidPath(grid, r - 1, c, path) == true)
                path.Add(new Tuple<int, int>(r - 1, c));
            else if (Prob_8_2_RobotInGrid_TempIsValidPath(grid, r, c - 1, path) == true)
                path.Add(new Tuple<int, int>(r, c - 1));

            return false;
        }

        public static List<Point> Prob_8_2_RobotInGrid(int[,] grid)
        {
            var paths = new List<Point>();
            var failedPaths = new List<Point>();
            //Bottom right of grid
            var startingPositionRow = grid.Length - 1;
            var startingPositionCol = grid.GetUpperBound(startingPositionRow);

            if (IsPossiblePosition(startingPositionRow, startingPositionCol, grid, paths, failedPaths))
                return paths;

            return null;
        }

        public static bool IsPossiblePosition(int row, int col, int[,] grid, List<Point> paths, List<Point> failedPaths)
        {
            if (row < 0 || col < 0) return false;

            var currentPoint = new Point(col, row);

            if (failedPaths.Contains(currentPoint))
                return false;

            if (IsTopLeft(row, col, grid) || IsPossiblePosition(row -1,col, grid, paths, failedPaths) 
                || IsPossiblePosition(row, col-1, grid, paths, failedPaths))
            {
                paths.Add(currentPoint);
                return true;
            }

            failedPaths.Add(currentPoint);
            return false;
        }

        private static bool IsTopLeft(int row, int col, int[,] grid)
        {
            return col == grid.Length && row == 0;
        }
        #endregion


        #region Prob_8_3_MagicIndex
        public static int Prob_8_3_MagicIndex_BruteForce(int[] arr)
        {
            return Prob_8_3_MagicIndex_Binary(arr, 0, arr.Length - 1);
        }

        public static int Prob_8_3_MagicIndex_Binary(int[] arr, int start, int end)
        {
            if (end < start)
                return -1;

            int middle = (end + start) / 2;

            if (middle == arr[middle])
                return middle;

            return arr[middle] < middle
                ? Prob_8_3_MagicIndex_Binary(arr, start, middle)
                : Prob_8_3_MagicIndex_Binary(arr, middle, end);
        }
        #endregion
    }
}
