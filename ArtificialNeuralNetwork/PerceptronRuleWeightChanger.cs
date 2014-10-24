using System;
using System.Collections.Generic;
using System.Linq;

namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    public class PerceptronRuleWeightChanger : WeightChanger
    {
        public PerceptronRuleWeightChanger()
        {
            
        }

        public WeightMap ChangeWeights(IList<double> outputErrors, IList<double> inputActivations, Layer upperLayer)
        {
            var inputToHiddenWeightChanges = CalculateInputToOutputWeightChanges(outputErrors, inputActivations);
            ChangeWeightStrengths(inputToHiddenWeightChanges, upperLayer);
            return inputToHiddenWeightChanges;
        }

        public virtual void ChangeWeightStrengths(IList<IList<double>> weightChanges, Layer upperLayer)
        {
            for (int iUpper = 0; iUpper < weightChanges.Count; iUpper++)
            {
                var weightChangesForOutput = weightChanges[iUpper];
                for (int w = 0; w < weightChangesForOutput.Count; w++)
                {
                    double weightChange = weightChangesForOutput[w];
                    var weightToChange = upperLayer[iUpper].IncomingWeights[w];
                    weightToChange.Strength += weightChange;
                }
            }
        }

        public virtual WeightMap CalculateInputToOutputWeightChanges(IList<double> outputErrors, IList<double> inputActivations)
        {
            var setsOfChanges = new WeightMap();
            for (int o = 0; o < outputErrors.Count; o++)
            {
                var weightChanges = new List<double>();
                for (int h = 0; h < inputActivations.Count; h++)
                {
                    double weightChange = CalculateInputToOutputWeightChange(inputActivations[h], outputErrors[o]);
                    weightChanges.Add(weightChange);
                }
                setsOfChanges.Add(weightChanges);
            }
            return setsOfChanges;
        }

        public virtual double CalculateInputToOutputWeightChange(double inputActivation, double outputError)
        {
            double multiplier = 0;

            if (outputError < 0) { multiplier = 1; }
            else if (outputError > 0) { multiplier = -1; }

            return multiplier * LearningConstant * inputActivation;
        }

        public double LearningConstant { get; set; }
    }
}