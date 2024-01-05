using UnityEngine;
using GC.Events;

namespace GC.Variables
{
    [CreateAssetMenu(fileName = "IntVariable", menuName = "GC/Variables/IntVariable")]
    public class IntVariable : BaseVariable<int, IntEvent> { }
}