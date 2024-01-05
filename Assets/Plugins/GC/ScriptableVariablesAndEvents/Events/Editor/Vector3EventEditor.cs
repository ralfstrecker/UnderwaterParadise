using UnityEditor;
using UnityEngine;

namespace GC.Events
{
    [CustomEditor(typeof(Vector3Event))]
    public class Vector3EventEditor : BaseEventEditor<Vector3, Vector3Event>
    {
        protected override Vector3 DrawDebugInputField(string label, Vector3 debugValue)
        {
            return EditorGUILayout.Vector3Field(label, debugValue);
        }
    }
}