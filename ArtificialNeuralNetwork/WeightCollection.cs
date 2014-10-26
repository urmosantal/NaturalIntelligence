using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    [Serializable, JsonArray(ItemIsReference=true)]
    public class WeightCollection : List<Weight> { }
}