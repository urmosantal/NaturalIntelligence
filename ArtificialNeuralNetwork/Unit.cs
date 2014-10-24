using System;

namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    public interface Unit
    {
        double? Activation { get; set; }

        Func<double, double> ActivationFunction { get; set; }

        WeightCollection IncomingWeights { get; }

        WeightCollection OutgoingWeights { get; }

        void Signal(double value);

        void Subscribe(SignalSubscriber subscriber);

        void Reset();

        void AddInput(double input);
    }
}