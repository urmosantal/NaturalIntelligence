using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificialNeuralNetwork
{
    public class ClampingException : Exception
    {
        public ClampingException() { }

        public ClampingException(string message)
            : base(message) { }
    }
}
