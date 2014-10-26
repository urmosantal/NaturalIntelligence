using System;
using System.Collections.Generic;

namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    public class PerceptronSettings
    {
        public PerceptronSettings()
        {
            WeightStrengthCreator = new RandomWeightStrengthFactory().GetStrength;
            HiddenLayers = new List<int>();
            InputUnitsInputAreaCreator = () => new SummedInputArea();
            HiddenUnitsInputAreaCreator = () => new SummedInputArea();
            OutputUnitsInputAreaCreator = () => new SummedInputArea();
        }

        public int InputUnits { get; set; }

        public int OutputUnits { get; set; }

        public IList<int> HiddenLayers { get; set; }

        public static int CalculateOptimalNumberOfHiddenUnits(int numberOfOutputUnits)
        {
            return Convert.ToInt32(Math.Ceiling(Math.Log(numberOfOutputUnits)));
        }

        public ActivationFunction InputActivationFunction { get; set; }

        public Func<InputArea> InputUnitsInputAreaCreator { get; set; }

        public ActivationFunction HiddenActivationFunction { get; set; }

        public Func<InputArea> HiddenUnitsInputAreaCreator { get; set; }

        public ActivationFunction OutputActivationFunction { get; set; }

        public Func<InputArea> OutputUnitsInputAreaCreator { get; set; }

        public Func<double> WeightStrengthCreator { get; set; }
    }
}