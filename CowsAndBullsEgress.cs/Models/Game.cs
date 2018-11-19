using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CowsAndBullsEgress.cs
{
    public class Game
    {
        public int[] SetToMatch { get; set; }

        public List<Guess> UserTries = new List<Guess>();

        public Game(int[] Numbers)
        {
            SetToMatch = Numbers;
        }

        public Game()
        {
            SetToMatch = NumberFactory.GetNumbers();
        }

        public (int bulls, int cows) SubmitGuess(int[] guess)
        {
            if (!GuessIsAllowed(guess))
            {
                throw new InvalidGuessException()
                {
                    Guess = guess
                };
            }
            var result = CowsAndBullsEngine.CheckForMatch(SetToMatch, guess);
            UserTries.Add(new Guess{ cows = result.cows, bulls = result.bulls, guess = guess});
            return result;
        }

        public static bool GuessIsAllowed(int[] guess)
        {
            return !guess.GroupBy(o => o).Any(o => o.Count() > 1);
        }

        public List<Guess> GetUserTries()
        {
            return UserTries;
        }
    }
}
