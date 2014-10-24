namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    public class StandardWeight : Weight, SignalSubscriber
    {
        public StandardWeight(Unit incomingUnit, Unit outgoingUnit)
        {
            IncomingUnit = incomingUnit;
            incomingUnit.OutgoingWeights.Add(this);
            OutgoingUnit = outgoingUnit;
            outgoingUnit.IncomingWeights.Add(this);
        }

        public double Strength { get; set; }

        public Unit IncomingUnit { get; protected set; }

        public Unit OutgoingUnit { get; protected set; }

        public void Signal(double value)
        {
            OutgoingUnit.Signal(value * Strength);
        }
    }
}