using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalIntelligence.GeneticAlgorithm
{
    public interface MatingPool<TGene>
    {
        MatingPoolSelection<TGene> MatingPoolSelection { get; set; }

        Func<Genome<TGene>, Genome<TGene>> MutationFunction { get; set; }

        IList<Genome<TGene>> CreateMatingPool(IList<Genome<TGene>> currentPopulation);
    }
}
