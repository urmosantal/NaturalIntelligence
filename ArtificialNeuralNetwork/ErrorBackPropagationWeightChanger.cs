using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    public class ErrorBackPropagationWeightChanger : WeightChanger
    {
        private Queue<WeightMap> uncommittedChanges;

        private Queue<WeightMap> previousCycleChanges;

        public double LearningConstant { get; set; }

        public double MomentumConstant { get; set; }

        public bool UseBias { get; set; }

        public int NumberOfLayers { get; set; }

        protected internal ErrorBackPropagationWeightChanger() { }

        public ErrorBackPropagationWeightChanger(PerceptronSettings settings) : this()
        {
            previousCycleChanges = new Queue<WeightMap>();
            uncommittedChanges = new Queue<WeightMap>();
            LearningConstant = 1;
            NumberOfLayers = settings.HiddenLayers.Count + 2;
        }

        public virtual void Enqueue(WeightMap change)
        {
            uncommittedChanges.Enqueue(change);

            //If this is the last change of the cycle then push to previous changes queue
            if(uncommittedChanges.Count + 1 == NumberOfLayers)
            {
                while(uncommittedChanges.Count > 0)
                {
                    previousCycleChanges.Enqueue(uncommittedChanges.Dequeue());
                }
            }
        }

        public virtual WeightMap Dequeue()
        {
            return previousCycleChanges.Count == 0 ? null : previousCycleChanges.Dequeue();
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

        public virtual double CalculateInputToOutputWeightChange(double activation, double error)
        {
            return LearningConstant * error * activation;
        }

        public virtual void ChangeWeightStrengths(IList<IList<double>> weightChanges, Layer upperLayer)
        {
            var lastWeightChanges = Dequeue();

            for (int iUpper = 0; iUpper < weightChanges.Count; iUpper++)
            {
                var weightChangesForOutput = weightChanges[iUpper];
                for (int w = 0; w < weightChangesForOutput.Count; w++)
                {
                    double weightChange = weightChangesForOutput[w];
                    double momentumChange = 0;
                    if (MomentumConstant > 0 && lastWeightChanges != null)
                    {
                        momentumChange = MomentumConstant * lastWeightChanges[iUpper][w];
                    }
                    var weightToChange = upperLayer[iUpper].IncomingWeights[w];
                    weightToChange.Strength += weightChange + momentumChange;
                }
            }
        }

        public virtual WeightMap ChangeWeights(IList<double> errors, IList<double> inputActivations, Layer upperLayer)
        {
            var inputToHiddenWeightChanges = CalculateInputToOutputWeightChanges(errors, inputActivations);
            if (UseBias)
            {
                for (int i = 0; i < upperLayer.Count(); i++)
                {
                    upperLayer[i].AddInput(errors[i]);
                }
            }
            ChangeWeightStrengths(inputToHiddenWeightChanges, upperLayer);
            return inputToHiddenWeightChanges;
        }

    }
}
