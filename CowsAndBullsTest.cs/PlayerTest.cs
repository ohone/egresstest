using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Linq;
using CowsAndBullsEgress.cs;

namespace CowsAndBullsTest.cs
{
    [TestFixture]
    public class PlayerTest
    {
        public static IEnumerable<int[]> GetAllOptions()
        {
            var list = GetPermutations(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 4).ToList().Select(o => o.ToArray()).ToList();
            return GetPermutations(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 4).ToList().Select(o => o.ToArray());
        }

        static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        [Test]
        [TestCaseSource("GetAllOptions")]
        public void PlayerGetsCorrectAnswerForAllInputs(int[] _case)
        {
            var game = new Game(_case);
            CowsAndBullsPlayer player = new CowsAndBullsPlayer();
            var result = player.PlayGame(game);
            Assert.AreEqual(result, _case);
        }
    }
}
