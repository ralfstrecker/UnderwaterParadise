using UnityEngine;
using UnityEditor;

namespace GC.Variables
{
    [CustomPropertyDrawer(typeof(DoubleReference))]
    public class DoubleReferenceDrawer : BaseReferenceDrawer
    {
        protected override void DrawConstantInputField(SerializedProperty constantValueProperty, Rect position)
        {
            double constantValue = constantValueProperty.doubleValue;
            double newConstantValue = EditorGUI.DoubleField(position, constantValue);
            constantValueProperty.doubleValue = newConstantValue;
        }
    }
}