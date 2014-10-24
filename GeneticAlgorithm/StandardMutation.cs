using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalIntelligence.GeneticAlgorithm
{
    /// <summary>
    /// This is a standard mutation class that deals with the stochastic element of mutation based on a mutation rate, random selection of a mutation point and object cloning of a genome
    /// </summary>
    public class StandardMutation<TGene> : RandomNumberGenerator
    {
        private Func<Genome<TGene>, int, Genome<TGene>> mutationFunctionWhenShouldMutate;

        public StandardMutation(Func<Genome<TGene>, int, Genome<TGene>> mutationFunctionWhenShouldMutate)
        {
            this.mutationFunctionWhenShouldMutate = mutationFunctionWhenShouldMutate;
        }

        public double MutationRate { get; set; }

        /// <summary>
        /// This function deals with the randomization based on mutation rate. All that is required is a handler for the mutation itself
        /// </summary>
        public Genome<TGene> MutationFunction(Genome<TGene> genome)
        {
            double diceRoll = Randomize();
            if (diceRoll < MutationRate)
            {
                int mutationPoint = Convert.ToInt32(Math.Floor(Randomize() * genome.Count));
                genome = mutationFunctionWhenShouldMutate(genome.Clone(), mutationPoint);
            }
            return genome;
        }
    }
}
