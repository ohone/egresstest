using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CowsAndBullsEgress.cs.Pages
{
    public class PlayTheComputerModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Enter your code, and the computer will attempt to crack it.";
        }

        private bool ValidateInput(out int[] guess, params string[] strings )
        {
            guess = new int[4];

            if (strings.Length != 4)
            {
                return false;
            }
            for(int i = 0; i < 4; i++)
            {
                if (!int.TryParse(strings[i], out var result))
                {
                    return false;
                }
                guess[i] = result;
            }
            return true;
        }

        public void OnPost(string FirstDigit, string SecondDigit, string ThirdDigit, string FourthDigit)
        {
            if (!ValidateInput(out int[] guess, FirstDigit, SecondDigit, ThirdDigit, FourthDigit))
            {
                Message = $"Invalid input. Each digit must be a number between 1 and 9.";
                return;
            }

            if (!Game.GuessIsAllowed(guess))
            {
                Message = $"Input must contain only one of each number.";
                return;
            }

            var game = new Game(guess);

            var player = new CowsAndBullsPlayer();
            var result = player.PlayGame(game);
            Message = $"Your code is {result[0]},{result[1]},{result[2]},{result[3]}. The computer took {game.UserTries.Count} guesses";
        }
    }
}