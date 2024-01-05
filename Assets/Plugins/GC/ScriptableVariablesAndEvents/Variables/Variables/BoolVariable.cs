using UnityEngine;
using GC.Events;

namespace GC.Variables
{
    [CreateAssetMenu(fileName = "BoolVariable", menuName = "GC/Variables/BoolVariable")]
    public class BoolVariable : BaseVariable<bool, BoolEvent> { }
}