namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    public class ThresholdActivationFunction : ActivationFunction
    {
        public double Threshold { get; set; }

        public double Activate(double input)
        {
            return input >= Threshold ? 1 : 0;
        }
    }
}