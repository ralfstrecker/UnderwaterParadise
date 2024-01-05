using UnityEditor;
using UnityEngine;

namespace GC.Events
{
    [CustomEditor(typeof(Vector2Event))]
    public class Vector2EventEditor : BaseEventEditor<Vector2, Vector2Event>
    {
        protected override Vector2 DrawDebugInputField(string label, Vector2 debugValue)
        {
            return EditorGUILayout.Vector2Field(label, debugValue);
        }
    }
}