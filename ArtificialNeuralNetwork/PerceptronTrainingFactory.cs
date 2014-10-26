using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    public static class PerceptronTrainingFactory
    {
        public static PerceptronTrainer Create(PerceptronSettings settings)
        {
            var perceptron = PerceptronFactory.Create(settings);
            return new PerceptronTrainer()
            {
                Perceptron = perceptron,
                LearningRule = new ErrorBackPropagation(perceptron),
                WeightStrengthCreator = settings.WeightStrengthCreator
            };
        }
    }
}
