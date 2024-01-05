using UnityEditor;
using GC.Events;

namespace GC.Variables
{
    [CustomEditor(typeof(FloatVariable))]
    public class FloatVariableEditor : BaseVariableEditor<float, FloatVariable, FloatEvent> { }
}