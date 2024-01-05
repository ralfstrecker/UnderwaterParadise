using UnityEditor;

namespace GC.Events
{
    [CustomEditor(typeof(BoolEvent))]
    public class BoolEventEditor : BaseEventEditor<bool, BoolEvent>
    {
        protected override bool DrawDebugInputField(string label, bool debugValue)
        {
            return EditorGUILayout.Toggle(label, debugValue);
        }
    }
}