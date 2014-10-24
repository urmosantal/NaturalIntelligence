using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    public class RandomExemplarEnumerator : IEnumerable<Exemplar>
    {
        private static RNGCryptoServiceProvider randomizer;
        static RandomExemplarEnumerator()
        {
            randomizer = new RNGCryptoServiceProvider();
        }

        private static double GetNext()
        {
            var result = new byte[8];
            randomizer.GetBytes(result);
            return (double)BitConverter.ToUInt64(result, 0) / ulong.MaxValue;
        }

        private IEnumerable<Exemplar> randomList;

        public RandomExemplarEnumerator(IEnumerable<Exemplar> exemplars)
        {
            randomList = exemplars.OrderBy(i => GetNext());
        }

        public IEnumerator<Exemplar> GetEnumerator()
        {
            return randomList.ToList().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return randomList.ToList().GetEnumerator();
        }
    }
}