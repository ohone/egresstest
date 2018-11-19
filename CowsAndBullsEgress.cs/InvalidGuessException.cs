using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CowsAndBullsEgress.cs
{
    public class InvalidGuessException : Exception
    {
        public int[] Guess { get; set; }
    }
}
