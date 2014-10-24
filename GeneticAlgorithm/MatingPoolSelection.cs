using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalIntelligence.GeneticAlgorithm
{
    public interface MatingPoolSelection<TGene>
    {
        IList<Genome<TGene>> GenerateMatingPool(IList<Genome<TGene>> population);
    }
}
