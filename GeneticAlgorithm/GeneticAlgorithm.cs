using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalIntelligence.GeneticAlgorithm
{
    public class GeneticAlgorithm<TGene>
    {
        public IList<TGene> Population { get; set; }

        public MatingPool<TGene> MatingPool { get; set; }

        public Func<IList<TGene>, IList<TGene>> Selection { get; set; }
    }
}
