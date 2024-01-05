using UnityEditor;
using GC.Events;
using UnityEngine;

namespace GC.Variables
{
    [CustomEditor(typeof(Vector2Variable))]
    public class Vector2VariableEditor : BaseVariableEditor<Vector2, Vector2Variable, Vector2Event> { }
}