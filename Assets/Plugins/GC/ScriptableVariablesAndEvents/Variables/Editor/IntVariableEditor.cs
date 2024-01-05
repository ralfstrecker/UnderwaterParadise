using UnityEditor;
using GC.Events;

namespace GC.Variables
{
    [CustomEditor(typeof(IntVariable))]
    public class IntVariableEditor : BaseVariableEditor<int, IntVariable, IntEvent> { }
}