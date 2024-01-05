using UnityEngine;
using UnityEditor;

namespace GC.Variables
{
    [CustomPropertyDrawer(typeof(IntReference))]
    public class IntReferenceDrawer : BaseReferenceDrawer
    {
        protected override void DrawConstantInputField(SerializedProperty constantValueProperty, Rect position)
        {
            int constantValue = constantValueProperty.intValue;
            int newConstantValue = EditorGUI.IntField(position, constantValue);
            constantValueProperty.intValue = newConstantValue;
        }
    }
}