using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NaturalIntelligence.GeneticAlgorithm
{
    public class RandomNumberGenerator : IDisposable
    {
        private RNGCryptoServiceProvider randomizer;

        public RandomNumberGenerator()
        {
            randomizer = new RNGCryptoServiceProvider();
        }

        public virtual double Randomize()
        {
            var result = new byte[8];
            randomizer.GetBytes(result);
            double randomWeightStrength = (double)BitConverter.ToUInt64(result, 0) / ulong.MaxValue;
            return randomWeightStrength;
        }

        private bool _disposed = false;

        /* See http://msdn.microsoft.com/en-us/library/b1yfkh5e(v=vs.110).aspx */
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    randomizer.Dispose();
                }
            }
        }

        ~RandomNumberGenerator()
        {
            Dispose(false);
        }
    }
}
