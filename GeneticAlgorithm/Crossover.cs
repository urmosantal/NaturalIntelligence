using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaturalIntelligence.GeneticAlgorithm
{
    public interface Crossover<TGene>
    {
        double CrossoverRate { get; set; }

        GenomePair<TGene> CrossoverFunction(Genome<TGene> genomeA, Genome<TGene> genomeB);
    }
}
