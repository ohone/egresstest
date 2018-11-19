using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CowsAndBullsEgress.cs
{
    public class CowsAndBullsEngine
    {
        public static (int bulls, int cows) CheckForMatch(int[] state, int[] guess)
        {
            var guessCopy = (int[])guess.Clone();
            var bulls = CheckForBulls(state, guessCopy);
            var cows = CheckForCows(state, guessCopy);

            return (bulls, cows); 
        }

        private static int CheckForBulls(int[] state, int[] guess)
        {
            int bulls = 0;

            for(int i = 0; i < 4; i++)
            {
                if (state[i] == guess[i])
                {
                    bulls++;
                    guess[i] = 0;
                }
            }

            return bulls;
        }

        private static int CheckForCows(int[] state, int[] guessExceptBulls)
        {
            return guessExceptBulls.Where(o => state.Contains(o)).Count();
        }
    }
}
