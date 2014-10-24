using System;
namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    public static class PerceptronFactory
    {
        public static Perceptron Create(PerceptronSettings settings)
        {
            settings.InputActivationFunction = settings.InputActivationFunction ?? new DummyActivationFunction().Activate;
            settings.HiddenActivationFunction = settings.HiddenActivationFunction ?? new UnipolarActivationFunction().Activate;
            settings.OutputActivationFunction = settings.OutputActivationFunction ?? new UnipolarActivationFunction().Activate;

            var perceptron = new Perceptron()
            {
                WeightStrengthCreator = settings.WeightStrengthCreator
            };
            AddLayer(settings.InputUnits, settings.InputActivationFunction, settings.InputUnitsInputAreaCreator, perceptron);

            //Add n hidden layers
            foreach (var numberOfUnits in settings.HiddenLayers)
            {
                //Number of units in each hidden layer
                AddLayer(numberOfUnits, settings.HiddenActivationFunction, settings.HiddenUnitsInputAreaCreator, perceptron);
            }

            AddLayer(settings.OutputUnits, settings.OutputActivationFunction, settings.OutputUnitsInputAreaCreator, perceptron);
            return perceptron;
        }


        private static void AddLayer(int numberOfUnits, Func<double, double> activationFunction, Func<InputArea> createInputArea, Perceptron perceptron)
        {
            Layer layer = new Layer();
            for (int i = 0; i < numberOfUnits; i++)
            {
                layer.Add(new StandardUnit()
                {
                    ActivationFunction = activationFunction,
                    InputArea = createInputArea()
                });
            }
            perceptron.AddLayer(layer);
        }
    }
}