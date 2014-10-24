using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NaturalIntelligence.GeneticAlgorithm
{
    public class StandardMatingPool<TGene> : MatingPool<TGene>
    {
        public MatingPoolSelection<TGene> MatingPoolSelection { get; set; }

        public Func<Genome<TGene>, Genome<TGene>> MutationFunction { get; set; }

        public Func<Genome<TGene>, double> FitnessFunction { get; set; }

        public IList<Genome<TGene>> CreateMatingPool(IList<Genome<TGene>> currentPopulation)
        {
            var matingPool = MatingPoolSelection.GenerateMatingPool(currentPopulation);

            if (MutationFunction != null)
            {
                foreach(var individual in matingPool)
                {
                    MutationFunction(individual);
                }
            }

            return matingPool;
        }


    }
}