using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace CowsAndBullsEgress.cs.Pages
{
    public class AboutModel : PageModel
    {
        public string Message { get; set; }
        public void OnGet()
        {
            HttpContext.Session.Set("Game", new Game());

            Message = "The computer has thought up a code. It's your job to crack it!";
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
            
            Message = ResultToString(HttpContext.Session.Get<Game>("Game").SubmitGuess(guess));
        }

        public void OnPostDelete()
        {
            HttpContext.Session.Set("Game", new Game());

            Message = "A new combination has been set.";
        }

        public string ResultToString((int bulls, int cows) result)
        {
            return $"{result.cows} cow{(result.cows != 1 ? "s" : "")} and {result.bulls} bull{(result.bulls != 1 ? "s" : "")}";
        }
    }
}