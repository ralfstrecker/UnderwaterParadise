using UnityEditor;
using GC.Events;

namespace GC.Variables
{
    [CustomEditor(typeof(DoubleVariable))]
    public class DoubleVariableEditor : BaseVariableEditor<double, DoubleVariable, DoubleEvent> { }
}