using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    public class Layer : IEnumerable<Unit>
    {
        private List<Unit> units;

        public Layer(Layer lowerLayer = null, Layer upperLayer = null)
        {
            units = new List<Unit>();

            LayerBelow = lowerLayer;
            if(LayerBelow != null)
            {
                LayerBelow.LayerAbove = this;
            }
            LayerAbove = upperLayer;
            if(LayerAbove != null)
            {
                LayerAbove.LayerBelow = this;
            }
        }

        public Layer LayerBelow { get; set; }

        public Layer LayerAbove { get; set; }

        public void ResetActivations()
        {
            units.ForEach(u => u.Reset());
        }

        public Layer AddDisconnected(params Unit[] newUnits)
        {
            units.AddRange(newUnits);
            return this;
        }

        public Layer Add(params Unit[] newUnits)
        {
            AddDisconnected(newUnits);
            foreach (var unit in newUnits)
            {
                unit.ConnectTo(LayerBelow, LayerAbove);
            }
            return this;
        }

        public void ForEach(Action<Unit> action)
        {
            units.ForEach(u => action(u));
        }

        public IList<double> GetActivations()
        {
            return this.Select(i => i.Activation.Value).ToList();
        }

        public IEnumerator<Unit> GetEnumerator()
        {
            return units.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return units.GetEnumerator();
        }

        public Unit this[int index]
        {
            get
            {
                return units[index];
            }
        }

        public int Count()
        {
            return units.Count;
        }
    }
}