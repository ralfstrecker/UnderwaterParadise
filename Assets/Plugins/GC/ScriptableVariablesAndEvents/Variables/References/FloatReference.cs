using System;
using GC.Events;

namespace GC.Variables
{
    [Serializable]
    public class FloatReference : BaseReference<float, FloatVariable, FloatEvent> { }
}