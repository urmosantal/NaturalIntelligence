using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace NaturalIntelligence.GeneticAlgorithm
{
    public class BiasedRouletteWheelMatingPoolSelection<TGene> : MatingPoolSelection<TGene>
    {
        public IList<Genome<TGene>> GenerateMatingPool(IList<Genome<TGene>> population)
        {
            using(var biasedRouletteWheel = new BiasedRouletteWheel<TGene>(population))
            {
                return biasedRouletteWheel.GeneratePopulation();
            }
        }
    }
}