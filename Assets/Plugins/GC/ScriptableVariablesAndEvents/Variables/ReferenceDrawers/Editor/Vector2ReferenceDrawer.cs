using UnityEngine;
using UnityEditor;

namespace GC.Variables
{
    [CustomPropertyDrawer(typeof(Vector2Reference))]
    public class Vector2ReferenceDrawer : BaseReferenceDrawer
    {
        protected override void DrawConstantInputField(SerializedProperty constantValueProperty, Rect position)
        {
            Vector2 constantValue = constantValueProperty.vector2Value;
            Vector2 newConstantValue = EditorGUI.Vector2Field(position, GUIContent.none, constantValue);
            constantValueProperty.vector2Value = newConstantValue;
        }
    }
}