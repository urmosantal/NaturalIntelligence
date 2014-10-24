namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    public class SummedInputArea : InputArea
    {
        public SummedInputArea() { }

        public virtual void Reset()
        {
            Total = 0;
        }

        public virtual double Total { get; protected set; }

        public virtual void AddInput(double value)
        {
            Total += value;
        }
    }
}