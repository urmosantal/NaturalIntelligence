using System.Collections.Generic;
namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    public class PerceptronTrainer
    {
        internal PerceptronTrainer() { }

        public LearningRule LearningRule { get; set; }

        public Perceptron Perceptron { get; set; }

        public void Train(IEnumerable<Exemplar> trainingData)
        {
            LearningRule.Train(trainingData);
        }
    }
}