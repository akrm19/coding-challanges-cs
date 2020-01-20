using System;
using System.Collections.Generic;

namespace CrackingTheCodingInterviewProblems
{
    public static class Sec6_MathLogic
    {
        /// <summary>
        /// Sec6s the check if prime.
        /// Checks i f anumber is prime
        /// </summary>
        /// <returns><c>true</c>, if check if prime was sec6ed, <c>false</c> otherwise.</returns>
        /// <param name="i">The index.</param>
        public static bool Sec6_CheckIfPrime(int x)
        {
            if (x < 2) return false;
            for (int i = 2; i < x; i++)
            {
                if (x % i == 0) return false;
            }
            return true;
        }

        public static bool Sec6_CheckIfPrimeBetter(int x)
        {
            if (x < 2) return false;
            int sqrt = (int)Math.Sqrt(x);
            for (int i = 2; i < sqrt; i++)
            {
                if (x % i == 0) return false;
            }
            return true;
        }

        public static List<int> GetPrimesUpTo2(int max)
        {
            Console.WriteLine($"Finding primes found in numbers up to {max}");
            var listOfPrimes = new List<int>();
            var sieveOfEratosthenes = GetSieveOfEratosthenes(max);

            for (int i = 2; i < sieveOfEratosthenes.Length; i++)
            {
                if (sieveOfEratosthenes[i])
                {
                    listOfPrimes.Add(i);
                    Console.WriteLine($"Adding Prime: {i}");
                }   
            }
            return listOfPrimes;
        }

        public static List<int> GetPrimesUpTo(int max)
        {
            var listOfPrimes = new List<int>();
            var sieveOfEratosthenes = GetSieveOfEratosthenes(max);

            for (int i = 2; i < sieveOfEratosthenes.Length; i++)
                if (sieveOfEratosthenes[i])
                    listOfPrimes.Add(i);

            return listOfPrimes;
        }

        public static bool[] GetSieveOfEratosthenes(int max)
        {
            var sieve = new bool[max + 1];
            for (int i = 0; i < sieve.Length; i++)
                sieve[i] = true;

            //Start with first prime #
            int currentPrime = 2;

            while(currentPrime < Math.Sqrt(max))
            {
                CrossOffCompositesOfPrime(sieve, currentPrime);
                currentPrime = GetNextPrimeInSieve(sieve, currentPrime);
            }

            return sieve;
        }

        private static void CrossOffCompositesOfPrime(bool[] sieve, int prime)
        {
            //We can skip all the numbers < current prime, since they 
            //would have been crossed of by earlier primes
            var currentComplement = prime;
            var currentComposite = prime * currentComplement;

            while (currentComposite <= sieve.Length)
            {
                sieve[currentComposite] = false;
                currentComplement++;
                currentComposite = prime * currentComplement;
            }
        } 

        public static int GetNextPrimeInSieve(bool[] sieve, int currentNum)
        {
            var next = currentNum + 1;
            while(next < sieve.Length && !sieve[next])
                next++;

            return next;
        }
    }
}
