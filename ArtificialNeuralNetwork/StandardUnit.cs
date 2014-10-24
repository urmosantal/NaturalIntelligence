using System;
using System.Collections.Generic;

namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    public class StandardUnit : Unit, SignalSubscriber
    {
        private int signalsReceived;

        private List<SignalSubscriber> subscribers;

        public StandardUnit()
        {
            IncomingWeights = new WeightCollection();
            OutgoingWeights = new WeightCollection();
            subscribers = new List<SignalSubscriber>();
        }

        public double? Activation { get; set; }

        public Func<double, double> ActivationFunction { get; set; }

        public WeightCollection IncomingWeights { get; protected set; }

        public InputArea InputArea { get; set; }

        public string Name { get; set; }

        public WeightCollection OutgoingWeights { get; protected set; }

        public void AddInput(double input)
        {
            InputArea.AddInput(input);
        }

        public void Reset()
        {
            Activation = null;
        }

        public virtual void Signal(double value)
        {
            signalsReceived++;
            AddInput(value);
            if (signalsReceived >= IncomingWeights.Count)
            {
                Fire();
                signalsReceived = 0;
                InputArea.Reset();
            }
        }

        public void Subscribe(SignalSubscriber subscriber)
        {
            subscribers.Add(subscriber);
        }

        protected internal virtual void Fire()
        {
            Activation = ActivationFunction(InputArea.Total);
            subscribers.ForEach(s => s.Signal(Activation.Value));
        }
    }
}