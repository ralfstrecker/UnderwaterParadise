using UnityEditor;
using GC.Events;

namespace GC.Variables
{
    [CustomEditor(typeof(BoolVariable))]
    public class BoolVariableEditor : BaseVariableEditor<bool, BoolVariable, BoolEvent> { }
}