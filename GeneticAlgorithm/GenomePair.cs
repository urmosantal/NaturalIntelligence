using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalIntelligence.GeneticAlgorithm
{
    public class GenomePair<TGene>
    {
        public Genome<TGene> GenomeA { get; set; }

        public Genome<TGene> GenomeB { get; set; }
    }
}
