using UnityEditor;
using GC.Events;
using UnityEngine;

namespace GC.Variables
{
    [CustomEditor(typeof(Vector3Variable))]
    public class Vector3VariableEditor : BaseVariableEditor<Vector3, Vector3Variable, Vector3Event> { }
}