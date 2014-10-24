using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalIntelligence.GeneticAlgorithm
{
    public interface NewPopulationSelection<TGene>
    {
        IList<Genome<TGene>> NextGeneration(IList<Genome<TGene>> thisGeneration);
    }
}
