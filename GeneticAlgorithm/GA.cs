using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaturalIntelligence.GeneticAlgorithm
{
    public class GA<TGene>
    {
        public MatingPool<TGene> MatingPoolSelection { get; set; }

        public NewPopulationSelection<TGene> NewPopulationSelection { get; set; }

        public IList<Genome<TGene>> Population { get; set; }

        public Func<Genome<TGene>, double> FitnessFunction { get; set; }

        public GA()
        {
            Population = new List<Genome<TGene>>();
            elitismRandomizer = new Random();
        }

        private Genome<TGene> bestResult;

        private Random elitismRandomizer;

        public bool UseElitism { get; set; }

        public void EvolveSingle()
        {
            if(UseElitism)
            {
                bestResult = Population.OrderByDescending(o => o.Fitness).First();
            }

            EvaluatePopulationFitness();

            var matingPool = MatingPoolSelection.CreateMatingPool(Population);

            Population = NewPopulationSelection.NextGeneration(matingPool);

            if(UseElitism)
            {
                int randomIndex = elitismRandomizer.Next(Population.Count);
                Population[randomIndex] = bestResult;
            }
        }

        public void Evolve(Func<bool> stoppingCondition)
        {
            while (stoppingCondition() != true)
            {
                EvolveSingle();
            }
        }

        public void EvaluatePopulationFitness()
        {
            foreach (var genome in Population)
            {
                genome.Fitness = FitnessFunction(genome);
            }
        }

        public double CurrentPopulationFitness
        {
            get
            {
                return Population.Sum(p => p.Fitness);
            }
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            foreach (var genome in Population)
            {
                stringBuilder.Append(genome.Select(g => g.ToString()).SelectMany(str => str).ToArray());
                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }
    }
}