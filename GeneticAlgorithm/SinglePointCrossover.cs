using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalIntelligence.GeneticAlgorithm
{
    public class SinglePointCrossover<TGene> : RandomNumberGenerator, Crossover<TGene>
    {
        public double CrossoverRate { get; set; }

        public GenomePair<TGene> CrossoverFunction(Genome<TGene> genomeA, Genome<TGene> genomeB)
        {
            double diceRoll = Randomize();
            if(diceRoll < CrossoverRate)
            {
                double randomPoint = (Randomize() * (genomeA.Count - 1)) + 1;
                int crossoverPoint = Convert.ToInt32(Math.Floor(randomPoint));
                var newGenomeA = new Genome<TGene>();
                var newGenomeB = new Genome<TGene>();
                for (int p = 0; p < genomeA.Count; p++)
                {
                    if (p < crossoverPoint)
                    {
                        newGenomeA.Add(genomeA[p]);
                        newGenomeB.Add(genomeB[p]);
                    }
                    else
                    {
                        newGenomeA.Add(genomeB[p]);
                        newGenomeB.Add(genomeA[p]);
                    }
                }
                genomeA = newGenomeA;
                genomeB = newGenomeB;
            }

            return new GenomePair<TGene>() { GenomeA = genomeA, GenomeB = genomeB };
        }
    }
}
