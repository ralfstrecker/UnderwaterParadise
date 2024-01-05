using UnityEngine;
using GC.Events;

namespace GC.Variables
{
    [CreateAssetMenu(fileName = "Vector3Variable", menuName = "GC/Variables/Vector3Variable")]
    public class Vector3Variable : BaseVariable<Vector3, Vector3Event> { }
}