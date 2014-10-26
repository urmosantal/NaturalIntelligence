using System;
using System.Collections.Generic;

namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    public class PerceptronTrainer
    {
        protected internal PerceptronTrainer() { }

        public virtual LearningRule LearningRule { get; set; }

        public virtual Perceptron Perceptron { get; set; }

        public virtual Func<double> WeightStrengthCreator { get; set; }

        public virtual void Train(IEnumerable<Exemplar> trainingData)
        {
            WeightInitializer.InitializeWeightsRandomly(Perceptron, WeightStrengthCreator);

            LearningRule.Train(trainingData);
        }
    }
}