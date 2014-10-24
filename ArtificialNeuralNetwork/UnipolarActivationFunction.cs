using System;

namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    public class UnipolarActivationFunction
    {
        public double Steepness { get; set; }

        public UnipolarActivationFunction()
        {
            Steepness = 1;
        }

        public double Activate(double input)
        {
            return 1 / (Math.Pow(Math.E, -Steepness * input) + 1);
        }
    }
}