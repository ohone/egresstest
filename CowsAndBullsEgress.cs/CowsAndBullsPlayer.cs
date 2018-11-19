using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CowsAndBullsEgress.cs
{
    public class CowsAndBullsPlayer
    {
        private Game _game;

        public int[] PlayGame(Game game)
        {
            this._game = game;
            int[] FourCows = GetFourCows();
            int[] FourBulls = GetFourBulls(FourCows);

            return FourBulls;
        }

        /// <summary>
        /// Get's the array of four cows by brute force.
        /// </summary>
        /// <returns></returns>
        public int[] GetFourCows()
        {
            HashSet<int> Cows = new HashSet<int>();
            HashSet<int> NotCows = new HashSet<int>();

            int[] guess = { 1, 2, 3, 4 };
            (int bulls, int cows) = _game.SubmitGuess(guess);
            var PreviousGuess = (guess, bulls, cows);

            if (bulls + cows == 4)
            {
                return guess;
            }

            for (int indexToIterate = 0; indexToIterate < 4 && Cows.Count() != 4; indexToIterate++)
            {
                for (int i = 0; i < 6; i++)
                {
                    var newGuess = IterateGuess(PreviousGuess.guess, indexToIterate, NotCows);

                    var result = _game.SubmitGuess(newGuess);
                    
                    if (result.bulls + result.cows == 4)
                    {
                        return newGuess;
                    }


                    // Index result is a cow, previous was not.
                    if (result.cows + result.bulls > PreviousGuess.cows + PreviousGuess.bulls)
                    {
                        Cows.Add(newGuess[indexToIterate]);
                        NotCows.Add(PreviousGuess.guess[indexToIterate]);
                        PreviousGuess = (newGuess, result.bulls, result.cows);
                        break ;
                    }

                    // Previous result was a cow, this is not.
                    if (result.cows + result.bulls < PreviousGuess.cows + PreviousGuess.bulls)
                    {
                        Cows.Add(PreviousGuess.guess[indexToIterate]);
                        NotCows.Add(newGuess[indexToIterate]);
                        PreviousGuess = (newGuess, result.bulls, result.cows);
                        continue;
                    }

                    // Swapped a bull for a cow or vice versa?
                    if (result.bulls > PreviousGuess.bulls || result.cows > PreviousGuess.cows)
                    {
                        Cows.Add(newGuess[indexToIterate]);
                        Cows.Add(PreviousGuess.guess[indexToIterate]);
                        PreviousGuess = (newGuess, result.bulls, result.cows);
                        continue;
                    }

                    PreviousGuess = (newGuess, result.bulls, result.cows);
                }
            }
            return Cows.ToArray();
        }

        /// <summary>
        /// Turns an array of four cows into the four bulls we need.
        /// </summary>
        /// <param name="cows"></param>
        /// <returns></returns>
        public int[] GetFourBulls(int[] cows)
        {
            var permutations = GetPermutations(cows, 4);

            foreach(var permutation in permutations)
            {
                var permutationArray = permutation.ToArray();
                var result = _game.SubmitGuess(permutationArray);
                if (result.bulls == 4)
                {
                    return permutationArray;
                }
            }
            throw new Exception("Something went terribly wrong with the algorithm.");
        }
        
        static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        /// <summary>
        /// Take the previous guess, creates the next guess to try.
        /// </summary>
        /// <param name="previousGuess">The previous guess.</param>
        /// <param name="index_to_iterate">The index in the guess to iterate.</param>
        /// <param name="NotCows">An array of integers that aren't cows, and shouldn't be used.</param>
        /// <returns>A new guess to push.</returns>
        public static int[] IterateGuess(int[] previousGuess, int index_to_iterate, HashSet<int> NotCows)
        {
            var thisGuess = (int[])previousGuess.Clone();
            thisGuess[index_to_iterate] =
                thisGuess[index_to_iterate] == 9
                ? 1
                : thisGuess[index_to_iterate] + 1;

            if (!GuessIsValid(thisGuess) || NotCows.Contains(thisGuess[index_to_iterate]))
            {
                return IterateGuess(thisGuess, index_to_iterate, NotCows);
            }

            return thisGuess;
        }

        /// <summary>
        /// Returns whether a guess is valid to submit.
        /// </summary>
        /// <param name="currentGuess"></param>
        /// <returns></returns>
        public static bool GuessIsValid(int[] currentGuess)
        {
            return !currentGuess.GroupBy(o => o).Any(o => o.Count() > 1);
        }
    }
}
