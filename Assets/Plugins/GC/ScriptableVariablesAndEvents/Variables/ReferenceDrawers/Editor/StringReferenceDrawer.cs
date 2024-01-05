using UnityEngine;
using UnityEditor;

namespace GC.Variables
{
    [CustomPropertyDrawer(typeof(StringReference))]
    public class StringReferenceDrawer : BaseReferenceDrawer
    {
        protected override void DrawConstantInputField(SerializedProperty constantValueProperty, Rect position)
        {
            string constantValue = constantValueProperty.stringValue;
            string newConstantValue = EditorGUI.TextField(position, constantValue);
            constantValueProperty.stringValue = newConstantValue;
        }
    }
}