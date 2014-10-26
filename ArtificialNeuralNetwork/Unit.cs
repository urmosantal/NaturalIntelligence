using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    public interface Unit
    {
        double? Activation { get; set; }

        ActivationFunction ActivationFunction { get; set; }

        WeightCollection IncomingWeights { get; }

        WeightCollection OutgoingWeights { get; }

        void ConnectTo(IEnumerable<Unit> unitsBelow, IEnumerable<Unit> unitsAbove);

        void Signal(double value);

        void Subscribe(SignalSubscriber subscriber);

        void Reset();

        void AddInput(double input);
    }
}