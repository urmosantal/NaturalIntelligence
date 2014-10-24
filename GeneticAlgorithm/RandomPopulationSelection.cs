using System;
using System.Collections.Generic;

namespace NaturalIntelligence.GeneticAlgorithm
{
    public class RandomPopulationSelection<TGene> : RandomNumberGenerator
    {
        public Genome<TGene> NextGeneration(IList<Genome<TGene>> pool)
        {
            int populationSize = pool.Count;
            var newPopulation = new List<Genome<TGene>>();
            int individualIndex = Convert.ToInt32(Math.Floor(Randomize() * populationSize));
            return pool[individualIndex];
        }
    }
}