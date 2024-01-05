using System;
using GC.Events;

namespace GC.Variables
{
    [Serializable]
    public class IntReference : BaseReference<int, IntVariable, IntEvent> { }
}