using UnityEngine;
using UnityEditor;

namespace GC.Variables
{
    [CustomPropertyDrawer(typeof(BoolReference))]
    public class BoolReferenceDrawer : BaseReferenceDrawer
    {
        protected override void DrawConstantInputField(SerializedProperty constantValueProperty, Rect position)
        {
            bool constantValue = constantValueProperty.boolValue;
            bool newConstantValue = EditorGUI.Toggle(position, constantValue);
            constantValueProperty.boolValue = newConstantValue;
        }
    }
}