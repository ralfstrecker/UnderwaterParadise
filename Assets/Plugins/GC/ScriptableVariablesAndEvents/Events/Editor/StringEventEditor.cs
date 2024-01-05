using UnityEditor;

namespace GC.Events
{
    [CustomEditor(typeof(StringEvent))]
    public class StringEventEditor : BaseEventEditor<string, StringEvent>
    {
        protected override string DrawDebugInputField(string label, string debugValue)
        {
            return EditorGUILayout.TextField(label, debugValue);
        }
    }
}