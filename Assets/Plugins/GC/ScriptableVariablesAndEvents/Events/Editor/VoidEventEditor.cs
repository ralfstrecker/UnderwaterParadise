using UnityEditor;

namespace GC.Events
{
    [CustomEditor(typeof(VoidEvent))]
    public class VoidEventEditor : BaseEventEditor<Void, VoidEvent>
    {
        protected override Void DrawDebugInputField(string label, Void debugValue)
        {
            return new Void();
        }
    }
}