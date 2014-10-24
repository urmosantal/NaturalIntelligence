using System.Collections.Generic;

namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    public class Exemplar
    {
        public Exemplar()
            : this(new List<double>(), new List<double>()) { }

        public Exemplar(IList<double> inputs, IList<double> desired)
        {
            Inputs = inputs;
            Desired = desired;
        }

        public IList<double> Inputs { get; protected set; }

        public IList<double> Desired { get; protected set; }
    }
}