using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    public class PerceptronJsonSerializer
    {
        private JsonSerializer serializer;

        public PerceptronJsonSerializer()
        {
            serializer = JsonSerializer.Create(new JsonSerializerSettings() {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Full,
                TypeNameHandling = TypeNameHandling.Objects
            });
        }

        public Perceptron DeserializeTo(string json, Perceptron toPopulate)
        {
            using (var reader = new JsonTextReader(new StringReader(json)))
            {
                var data = serializer.Deserialize<PerceptronData>(reader);
                toPopulate.Layers.Clear();
                foreach (var arrayOfUnits in data.Layers)
                {
                    toPopulate.Create().AddDisconnected(arrayOfUnits.ToArray());
                }

            }
            return toPopulate;
        }

        public string SerializeFrom(Perceptron perceptron)
        {
            using (var sb = new StringWriter())
            {
                serializer.Serialize(sb, new PerceptronData(perceptron));
                sb.Flush();
                return sb.GetStringBuilder().ToString();
            }
        }

        [Serializable, JsonObject]
        private class PerceptronData
        {
            //Jagged arrays because serializers usually generally don't like generic collections
            public Unit[][] Layers = null;
            
            public PerceptronData() { }

            public PerceptronData(Perceptron perceptron)
            {
                int layerCount = perceptron.Layers.Count();
                Layers = new Unit[layerCount][];
                for (int l = 0; l < layerCount; l++)
                {
                    int unitCount = perceptron.Layers[l].Count();
                    Layers[l] = new Unit[unitCount];
                    for(int i = 0; i < unitCount; i++)
                    {
                        Layers[l][i] = perceptron.Layers[l][i];
                    }
                }
                Layers = perceptron.Layers.Select(l => l.ToArray()).ToArray();
            }
        }
    }
}
