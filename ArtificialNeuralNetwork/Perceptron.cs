using System;
using System.Collections.Generic;
using System.Linq;

namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    public class Perceptron
    {
        private List<Layer> layers;

        protected internal Perceptron()
        {
            layers = new List<Layer>();
        }

        public virtual IList<Layer> Layers { get { return layers; } }

        public Func<double> WeightStrengthCreator { get; set; }

        public virtual IList<double> Activate(IList<double> inputs)
        {
            var inputLayer = layers[0];
            var outputLayer = layers.Last();
            for (int i = 0; i < inputs.Count(); i++)
            {
                inputLayer[i].Signal(inputs[i]);
            }

            return outputLayer.Select(ol => ol.Activation.Value).ToList();
        }

        public virtual void AddLayer(Layer layer)
        {
            layers.Add(layer);
            if (layers.Count > 1)
            {
                int secondFromLastIndex = layers.Count - 2;
                var secondFromLayerLayer = layers[secondFromLastIndex];
                ConnectLayers(secondFromLayerLayer, layer);
            }
        }

        protected void ConnectLayers(Layer lower, Layer upper)
        {
            lower.ForEach(lowerUnit =>
            {
                upper.ForEach(upperUnit =>
                {
                    var connection = new StandardWeight(lowerUnit, upperUnit);
                    connection.Strength = WeightStrengthCreator();
                    lowerUnit.Subscribe(connection);
                });
            });
        }
    }
}