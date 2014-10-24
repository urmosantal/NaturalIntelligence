using System.Collections.Generic;

namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    public interface WeightChanger
    {
        WeightMap ChangeWeights(IList<double> outputErrors, IList<double> inputActivations, Layer upperLayer);

        double LearningConstant { get; set; }
    }
}