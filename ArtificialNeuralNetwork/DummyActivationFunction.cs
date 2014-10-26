namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    public class DummyActivationFunction : ActivationFunction
    {
        public double Activate(double input)
        {
            return input;
        }
    }
}