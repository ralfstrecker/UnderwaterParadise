using System;
using GC.Events;

namespace GC.Variables
{
    [Serializable]
    public class BoolReference : BaseReference<bool, BoolVariable, BoolEvent> { }
}