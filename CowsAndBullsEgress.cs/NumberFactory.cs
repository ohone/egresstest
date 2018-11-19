using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CowsAndBullsEgress.cs
{
    public static class NumberFactory
    {
        public static int[] GetNumbers()
        {
            var rand = new Random();

            StringBuilder sb = new StringBuilder();

            var intlist = new int[4];

            for(int i = 0; i < 4; i++)
            {
                intlist[i] = GetRandomNumber(rand, intlist);
            }
           
            return intlist;
        }

        private static int GetRandomNumber(Random random, params int[] exclude)
        {
            int result = 0;
            while (exclude.Contains(result))
            {
                result = random.Next(8) + 1;
            }
            return result;
        }
    }
}
