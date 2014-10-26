using System;
namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    public static class PerceptronFactory
    {
        public static Perceptron Create(PerceptronSettings settings)
        {
            settings.InputActivationFunction = settings.InputActivationFunction ?? new DummyActivationFunction();
            settings.HiddenActivationFunction = settings.HiddenActivationFunction ?? new UnipolarActivationFunction();
            settings.OutputActivationFunction = settings.OutputActivationFunction ?? new UnipolarActivationFunction();

            var perceptron = new Perceptron();
            AddLayer(settings.InputUnits, settings.InputActivationFunction, settings.InputUnitsInputAreaCreator, perceptron, "I");

            //Add n hidden layers
            foreach (var numberOfUnits in settings.HiddenLayers)
            {
                //Number of units in each hidden layer
                AddLayer(numberOfUnits, settings.HiddenActivationFunction, settings.HiddenUnitsInputAreaCreator, perceptron, "H");
            }

            AddLayer(settings.OutputUnits, settings.OutputActivationFunction, settings.OutputUnitsInputAreaCreator, perceptron, "O");
            return perceptron;
        }


        private static void AddLayer(int numberOfUnits, ActivationFunction activationFunction, Func<InputArea> createInputArea, Perceptron perceptron, string layerNumber)
        {
            Layer layer = perceptron.Create();
            for (int i = 0; i < numberOfUnits; i++)
            {
                layer.Add(new StandardUnit()
                {
                    ActivationFunction = activationFunction,
                    InputArea = createInputArea(),
                });
            }
        }
    }
}