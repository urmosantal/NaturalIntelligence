﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalIntelligence.ArtificialNeuralNetwork
{
    public interface SignalSubscriber
    {
        void Signal(double value);
    }
}
