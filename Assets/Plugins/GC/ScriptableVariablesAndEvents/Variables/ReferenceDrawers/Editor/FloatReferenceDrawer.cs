using UnityEngine;
using UnityEditor;

namespace GC.Variables
{
    [CustomPropertyDrawer(typeof(FloatReference))]
    public class FloatReferenceDrawer : BaseReferenceDrawer
    {
        protected override void DrawConstantInputField(SerializedProperty constantValueProperty, Rect position)
        {
            float constantValue = constantValueProperty.floatValue;
            float newConstantValue = EditorGUI.FloatField(position, constantValue);
            constantValueProperty.floatValue = newConstantValue;
        }
    }
}