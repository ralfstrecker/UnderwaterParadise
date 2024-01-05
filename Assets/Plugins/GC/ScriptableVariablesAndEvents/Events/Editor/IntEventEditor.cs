using UnityEditor;

namespace GC.Events
{
    [CustomEditor(typeof(IntEvent))]
    public class IntEventEditor : BaseEventEditor<int, IntEvent>
    {
        protected override int DrawDebugInputField(string label, int debugValue)
        {
            return EditorGUILayout.IntField(label, debugValue);
        }
    }
}