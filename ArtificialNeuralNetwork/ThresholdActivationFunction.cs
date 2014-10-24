namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    public class ThresholdActivationFunction
    {
        public double Threshold { get; set; }

        public double Activate(double input)
        {
            return input >= Threshold ? 1 : 0;
        }
    }
}