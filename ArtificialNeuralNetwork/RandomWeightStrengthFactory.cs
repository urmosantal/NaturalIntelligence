using System;
using System.Security.Cryptography;

namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    public class RandomWeightStrengthFactory : IDisposable
    {
        private RNGCryptoServiceProvider randomizer;

        public RandomWeightStrengthFactory()
        {
            StartRange = -2;
            EndRange = 2;
            randomizer = new RNGCryptoServiceProvider();
        }

        public double StartRange { get; set; }

        public double EndRange { get; set; }

        public double GetStrength()
        {
            double difference = Math.Abs(EndRange - StartRange);
            return Randomize() * difference + StartRange;
        }

        public virtual double Randomize()
        {
            var result = new byte[8];
            randomizer.GetBytes(result);
            double randomWeightStrength = (double)BitConverter.ToUInt64(result, 0) / ulong.MaxValue;
            return randomWeightStrength;
        }

        /* See http://msdn.microsoft.com/en-us/library/b1yfkh5e(v=vs.110).aspx */
        private bool _disposed = false;
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

        ~RandomWeightStrengthFactory()
        {
            Dispose(false);
        }
    }
}