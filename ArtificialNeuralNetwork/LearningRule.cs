using System.Collections.Generic;

namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    public interface LearningRule
    {
        Perceptron Perceptron { get; }

        void Train(IEnumerable<Exemplar> trainingData);
    }
}