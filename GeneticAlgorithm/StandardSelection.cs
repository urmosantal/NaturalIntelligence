using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalIntelligence.GeneticAlgorithm
{
    public class StandardSelection<TGene> : NewPopulationSelection<TGene>
    {
        public Func<Genome<TGene>, Genome<TGene>, GenomePair<TGene>> CrossoverFunction { get; set; }

        public Func<Genome<TGene>, Genome<TGene>> MutationFunction { get; set; }

        public Func<IList<Genome<TGene>>, Genome<TGene>> SelectForNextGeneration { get; set; }

        public IList<Genome<TGene>> NextGeneration(IList<Genome<TGene>> thisGeneration)
        {
            var nextGeneration = new List<Genome<TGene>>();
            for (int g1 = 0, g2 = 1; g1 < thisGeneration.Count(); g1 += 2, g2 = g1 + 1)
            {
                var genomeA = SelectForNextGeneration(thisGeneration);
                Genome<TGene> genomeB = null;
                bool canMakePair = g2 < thisGeneration.Count();
                if (canMakePair)
                {
                    genomeB = SelectForNextGeneration(thisGeneration);
                    var crossedOverPair = CrossoverFunction(genomeA, genomeB);
                    genomeA = crossedOverPair.GenomeA;
                    genomeB = crossedOverPair.GenomeB;
                }
                processGenomeAfterSelection(nextGeneration, genomeA, genomeB);
            };

            return nextGeneration;
        }

        private void processGenomeAfterSelection(IList<Genome<TGene>> newPopulation, params Genome<TGene>[] genomes)
        {
            foreach(var g in genomes.Where(o => o != null))
            {
                newPopulation.Add(MutationFunction != null ? MutationFunction(g) : g);
            };
        }
    }
}
