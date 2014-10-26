using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    internal static class WeightInitializer
    {
        public static void InitializeWeightsRandomly(Perceptron perceptron, Func<double> weightStrengthFunction)
        {
            for (int i = 0; i < perceptron.Layers.Count - 1; i++)
            {
                var layer = perceptron.Layers[i];
                foreach (var unit in layer)
                {
                    unit.OutgoingWeights.ForEach(w => {
                        w.Strength = weightStrengthFunction();
                    });
                }
            }
        }
    }
}
