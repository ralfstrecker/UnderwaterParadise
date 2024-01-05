using System;
using GC.Events;

namespace GC.Variables
{
    [Serializable]
    public class DoubleReference : BaseReference<double, DoubleVariable, DoubleEvent> { }
}