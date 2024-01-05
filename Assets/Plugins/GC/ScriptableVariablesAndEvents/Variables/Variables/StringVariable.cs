using UnityEngine;
using GC.Events;

namespace GC.Variables
{
    [CreateAssetMenu(fileName = "StringVariable", menuName = "GC/Variables/StringVariable")]
    public class StringVariable : BaseVariable<string, StringEvent> { }
}