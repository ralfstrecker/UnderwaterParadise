using UnityEditor;

namespace GC.Events
{
    [CustomEditor(typeof(FloatEvent))]
    public class FloatEventEditor : BaseEventEditor<float, FloatEvent>
    {
        protected override float DrawDebugInputField(string label, float debugValue)
        {
            return EditorGUILayout.FloatField(label, debugValue);
        }
    }
}