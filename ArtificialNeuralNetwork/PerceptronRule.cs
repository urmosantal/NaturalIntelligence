using System;
using System.Collections.Generic;
using System.Linq;

namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    public class PerceptronRule : LearningRule
    {
        public PerceptronRule(Perceptron perceptron)
        {
            Perceptron = perceptron;
        }

        public Perceptron Perceptron { get; protected set; }

        private Layer OutputLayer { get { return Perceptron.Layers.Last(); } }

        private Layer InputLayer { get { return Perceptron.Layers.First(); } }

        public WeightChanger WeightChanger { get; set; }

        /// <summary>
        /// Runs training until the stopping condition is met
        /// </summary>
        public virtual void Train(IEnumerable<Exemplar> trainingData)
        {
            while (TrainCycle(trainingData) == false) ;
        }

        /// <summary>
        /// Presents all exemplars in the training dataset, adjusts weights and compares the netError with the errorMax
        /// </summary>
        /// <returns>'true' if training is complete, 'false' otherwise</returns>
        public virtual bool TrainCycle(IEnumerable<Exemplar> trainingData)
        {
            return trainingData.All(td => TrainSingle(td));
        }

        /// <summary>
        /// Presents a single exemplar to the network and adjusts weights accordingly
        /// </summary>
        public virtual bool TrainSingle(Exemplar exemplar)
        {
            Perceptron.Activate(exemplar.Inputs);

            var outputsAreCorrect = CalculateIfUnitsRespondCorrectly(exemplar);

            bool allCorrect = outputsAreCorrect.All(isCorrect => isCorrect);

            if(allCorrect == false)
            {
                //Apply weight changes
                var outputSignals = new List<double>();
                for(int o = 0; o < OutputLayer.Count(); o++)
                {
                    double err = OutputLayer[o].Activation.Value - exemplar.Desired[o];
                    outputSignals.Add(err);
                }
                WeightChanger.ChangeWeights(outputSignals, exemplar.Inputs, OutputLayer);
            }

            ResetActivations();

            return allCorrect;
        }

        private void ResetActivations()
        {
            foreach (var layer in Perceptron.Layers)
            {
                layer.ResetActivations();
            }
        }

        /// <summary>
        /// Precondition: exemplar.Desired.Count == numberOfOutputUnits
        /// </summary>
        public virtual IEnumerable<bool> CalculateIfUnitsRespondCorrectly(Exemplar exemplar)
        {
            for (int i = 0; i < OutputLayer.Count(); i++)
            {
                double activation = OutputLayer[i].Activation.Value;
                double desired = exemplar.Desired[i];
                yield return desired == activation;
            }
        }
    }
}