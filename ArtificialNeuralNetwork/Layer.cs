using System.Collections.Generic;
using System.Linq;

namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    public class Layer : List<Unit>
    {
        public void ResetActivations()
        {
            ForEach(u => u.Reset());
        }

        public IList<double> GetActivations()
        {
            return this.Select(i => i.Activation.Value).ToList();
        }
    }
}