using UnityEngine;
using GC.Events;

namespace GC.Variables
{
    [CreateAssetMenu(fileName = "FloatVariable", menuName = "GC/Variables/FloatVariable")]
    public class FloatVariable : BaseVariable<float, FloatEvent> { }
}