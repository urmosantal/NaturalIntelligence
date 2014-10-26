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

        public virtual IList<double> Activate(params double[] inputs)
        {
            return Activate(inputs.ToList());
        }

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

        public Layer Create()
        {
            var newLayer = new Layer(Layers.Count < 1 ? null : Layers.Last());
            Layers.Add(newLayer);
            return newLayer;
        }
    }
}