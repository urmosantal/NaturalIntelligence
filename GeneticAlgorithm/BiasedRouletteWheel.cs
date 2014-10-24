using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NaturalIntelligence.GeneticAlgorithm
{
    //Credit to http://www.vcskicks.com/random-element.php for the code adapted to write this class
    public class BiasedRouletteWheel<TGene> : RandomNumberGenerator
    {
        public IList<Genome<TGene>> Population { get; protected set; }

        public BiasedRouletteWheel() : this(new List<Genome<TGene>>()) { }
        
        /// <summary>
        /// Pre-condition: Ensure the total fitness is not greater than 1 - ie ensure fitness values are normalized
        /// </summary>
        public BiasedRouletteWheel(IList<Genome<TGene>> population)
        {
            this.Population = population;
        }

        public virtual IList<Genome<TGene>> GeneratePopulation()
        {
            var newPopulation = new List<Genome<TGene>>();
            while(newPopulation.Count < Population.Count)
            {
                newPopulation.Add(SelectSingle());
            }

            if(newPopulation.Any(p => p == null))
            {
                int x = 0;
            }

            return newPopulation;
        }

        protected virtual Genome<TGene> SelectSingle()
        {
            double populationFitness = Population.Sum(g => g.Fitness);
            return populationFitness > 0 ? SelectSingleByBiasedRouletteWheelMethod(populationFitness) : SelectRandom();
        }

        private Genome<TGene> SelectRandom()
        {
            int randomIndex = Convert.ToInt32(Math.Floor(Randomize() * Population.Count));
            return Population[randomIndex];
        }

        private Genome<TGene> SelectSingleByBiasedRouletteWheelMethod(double populationFitness)
        {
            double randomValue = Randomize() * populationFitness;
            double cumulative = 0.0;
            Genome<TGene> selectedElement = null;
            foreach (var individual in Population)
            {
                cumulative += individual.Fitness;
                if (randomValue < cumulative)
                {
                    selectedElement = individual;
                    break;
                }
            }
            return selectedElement;
        }
    }
}
