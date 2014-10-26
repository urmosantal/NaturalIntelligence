using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    public interface ActivationFunction
    {
        double Activate(double input);
    }
}
