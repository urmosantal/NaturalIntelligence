namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    public interface Weight
    {
        Unit IncomingUnit { get; }

        Unit OutgoingUnit { get; }

        void Signal(double value);

        double Strength { get; set; }
    }
}