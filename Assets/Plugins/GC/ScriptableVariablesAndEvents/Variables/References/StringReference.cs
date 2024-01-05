using System;
using GC.Events;

namespace GC.Variables
{
    [Serializable]
    public class StringReference : BaseReference<string, StringVariable, StringEvent> { }
}