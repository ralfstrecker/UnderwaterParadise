using UnityEditor;

namespace GC.Events
{
    [CustomEditor(typeof(DoubleEvent))]
    public class DoubleEventEditor : BaseEventEditor<double, DoubleEvent>
    {
        protected override double DrawDebugInputField(string label, double debugValue)
        {
            return EditorGUILayout.DoubleField(label, debugValue);
        }
    }
}