using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    [Serializable, JsonObject(IsReference = true)]
    public class StandardUnit : Unit, SignalSubscriber
    {
        [NonSerialized]
        private int signalsReceived;

        [JsonProperty(ItemIsReference = true)]
        private List<SignalSubscriber> subscribers;

        public StandardUnit()
        {
            IncomingWeights = new WeightCollection();
            OutgoingWeights = new WeightCollection();
            subscribers = new List<SignalSubscriber>();
        }

        [JsonIgnore]
        public double? Activation { get; set; }

        [JsonProperty(IsReference = true)]
        public ActivationFunction ActivationFunction { get; set; }

        [JsonIgnore]//Ignore. Including will duplicate on deserialization due to two-way referencing
        public WeightCollection IncomingWeights { get; set; }

        [JsonProperty(IsReference = true)]
        public InputArea InputArea { get; set; }

        [JsonIgnore]//Ignore. Including will duplicate on deserialization due to two-way referencing
        public WeightCollection OutgoingWeights { get; set; }

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
            Activation = ActivationFunction.Activate(InputArea.Total);
            subscribers.ForEach(s => s.Signal(Activation.Value));
        }

        public void ConnectTo(IEnumerable<Unit> unitsBelow, IEnumerable<Unit> unitsAbove)
        {
            if (unitsBelow != null)
            {
                foreach (var unitBelow in unitsBelow)
                {
                    var connection = new StandardWeight(unitBelow, this);
                    unitBelow.Subscribe(connection);
                }
            }

            if (unitsAbove != null)
            {
                foreach (var unitAbove in unitsAbove)
                {
                    var connection = new StandardWeight(this, unitAbove);
                    Subscribe(connection);
                }
            }
        }
    }
}