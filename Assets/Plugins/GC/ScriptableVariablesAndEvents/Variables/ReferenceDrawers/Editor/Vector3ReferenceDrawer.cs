using UnityEngine;
using UnityEditor;

namespace GC.Variables
{
    [CustomPropertyDrawer(typeof(Vector3Reference))]
    public class Vector3ReferenceDrawer : BaseReferenceDrawer
    {
        protected override void DrawConstantInputField(SerializedProperty constantValueProperty, Rect position)
        {
            Vector3 constantValue = constantValueProperty.vector3Value;
            Vector3 newConstantValue = EditorGUI.Vector3Field(position, GUIContent.none, constantValue);
            constantValueProperty.vector3Value = newConstantValue;
        }
    }
}