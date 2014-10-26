using System;
using System.Collections.Generic;
using System.Linq;

namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    public class ErrorBackPropagation : LearningRule
    {
        public ErrorBackPropagation(Perceptron perceptron)
        {
            Perceptron = perceptron;
            WeightChanger = new ErrorBackPropagationWeightChanger()
            {
                LearningConstant = 0.1,
            };
        }

        public double ErrorMax { get; set; }

        public Perceptron Perceptron { get; protected set; }

        public WeightChanger WeightChanger { get; set; }

        private Layer InputLayer { get { return Perceptron.Layers.First(); } }

        private Layer OutputLayer { get { return Perceptron.Layers.Last(); } }

        public static double CalculateHiddenError(Unit hiddenUnit, IList<double> outputErrors)
        {
            double hiddenUnitActivation = hiddenUnit.Activation.Value;
            double outputErrorTimesWeightCumulative = 0;

            int numberOfOutputUnits = outputErrors.Count;
            for (int i = 0; i < numberOfOutputUnits; i++)
            {
                double weightStrength = hiddenUnit.OutgoingWeights[i].Strength;
                double error = outputErrors[i];
                outputErrorTimesWeightCumulative += error * weightStrength;
            }

            return hiddenUnitActivation * (1 - hiddenUnitActivation) * outputErrorTimesWeightCumulative;
        }

        /// <summary>
        /// Calculates the error for an exemplar pair
        /// Precondition: For all output units, Activation must not be null
        /// </summary>
        public double CalculateCycleError(Exemplar exemplar)
        {
            double sumErr = 0;
            int numberOfOutputUnits = OutputLayer.Count();
            for (int i = 0; i < numberOfOutputUnits; i++)
            {
                sumErr += Math.Pow(exemplar.Desired[i] - OutputLayer[i].Activation.Value, 2);
            }
            return 0.5 * sumErr;
        }

        /// <summary>
        /// Precondition: outputErrors.Count == numberOfOutputUnits
        /// </summary>
        public virtual IList<double> CalculateHiddenLayerErrors(IList<double> outputErrors, Layer layer)
        {
            var hiddenErrors = new List<double>();
            foreach(var hiddenUnit in layer)
            {
                double hiddenError = CalculateHiddenError(hiddenUnit, outputErrors);
                hiddenErrors.Add(hiddenError);
            };
            return hiddenErrors;
        }

        public virtual double CalculateOutputError(double desiredOutput, double actualOutput)
        {
            return (desiredOutput - actualOutput) * actualOutput * (1 - actualOutput);
        }

        /// <summary>
        /// Precondition: exemplar.Desired.Count == numberOfOutputUnits
        /// </summary>
        public virtual IList<double> CalculateTopLayerErrors(Exemplar exemplar)
        {
            var errors = new List<double>();
            int numberOfOutputUnits = OutputLayer.Count();
            for (int i = 0; i < numberOfOutputUnits; i++)
            {
                double activation = OutputLayer[i].Activation.Value;
                double desired = exemplar.Desired[i];
                errors.Add(CalculateOutputError(desired, activation));
            }
            return errors;
        }

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
            return trainingData.Sum(td => TrainSingle(td)) < ErrorMax;
        }

        /// <summary>
        /// Presents a single exemplar to the network and adjusts weights accordingly
        /// </summary>
        public virtual double TrainSingle(Exemplar exemplar)
        {
            Perceptron.Activate(exemplar.Inputs);

            var outputErrors = CalculateTopLayerErrors(exemplar);
            var hiddenToOutputWeightChanges = WeightChanger.ChangeWeights(outputErrors, Perceptron.Layers[Perceptron.Layers.Count - 2].GetActivations(), OutputLayer);

            int lastHiddenIndex = Perceptron.Layers.Count - 2;
            for (int i = lastHiddenIndex; i > 0; i--)
            {
                var upperLayer = Perceptron.Layers[i];
                var lowerLayer = Perceptron.Layers[i - 1];
                var upperErrors = CalculateHiddenLayerErrors(outputErrors, upperLayer);
                var inputToHiddenWeightChanges = WeightChanger.ChangeWeights(upperErrors, lowerLayer.GetActivations(), upperLayer);
                outputErrors = upperErrors;
            }

            double sumErr = CalculateCycleError(exemplar);

            ResetActivations();

            return sumErr;
        }

        private void ResetActivations()
        {
            foreach (var layer in Perceptron.Layers)
            {
                layer.ResetActivations();
            }
        }
    }
}