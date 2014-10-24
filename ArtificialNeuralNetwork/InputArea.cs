namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    public interface InputArea
    {
        void AddInput(double value);
        void Reset();
        double Total { get; }
    }
}
