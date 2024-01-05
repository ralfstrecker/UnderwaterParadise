using UnityEngine;
using GC.Events;

namespace GC.Variables
{
    [CreateAssetMenu(fileName = "DoubleVariable", menuName = "GC/Variables/DoubleVariable")]
    public class DoubleVariable : BaseVariable<double, DoubleEvent> { }
}