using UnityEditor;
using GC.Events;

namespace GC.Variables
{
    [CustomEditor(typeof(StringVariable))]
    public class StringVariableEditor : BaseVariableEditor<string, StringVariable, StringEvent> { }
}