using UnityEngine;
using GC.Events;

namespace GC.Variables
{
    [CreateAssetMenu(fileName = "Vector2Variable", menuName = "GC/Variables/Vector2Variable")]
    public class Vector2Variable : BaseVariable<Vector2, Vector2Event> { }
}