using UnityEngine;
using UnityEditor;

namespace GC.Variables
{
    public class BaseReferenceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            //How wide should the dropdown button be?
            const float dropdownWidth = 30f;

            //Store nested properties for easier access
            SerializedProperty useConstantProperty = property.FindPropertyRelative(nameof(FloatReference.useConstant));
            SerializedProperty constantValueProperty = property.FindPropertyRelative(nameof(FloatReference.constantValue));
            SerializedProperty variableProperty = property.FindPropertyRelative(nameof(FloatReference.variable));

            //Get bool UseConstant from Property
            bool useConstant = useConstantProperty.boolValue;

            //Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            //Set size and style of dropdown button
            Rect dropdownRect = new Rect(position.position, new Vector2(dropdownWidth, position.height));

            //Draw dropdown button
            if (EditorGUI.DropdownButton(dropdownRect, new GUIContent(EditorGUIUtility.IconContent("SettingsIcon")), FocusType.Keyboard))
            {
                GenericMenu menu = new GenericMenu();
                menu.AddItem(new GUIContent("Use Constant"), useConstant, () => SetProperty(useConstantProperty, true));
                menu.AddItem(new GUIContent("Use Variable"), !useConstant, () => SetProperty(useConstantProperty, false));
                menu.ShowAsContext();
            }

            //Offset position so fields don't overlap
            position.position += Vector2.right * (dropdownWidth + 5f);
            position.width -= (dropdownWidth + 5f);

            //Draw property
            if (useConstant)
            {
                DrawConstantInputField(constantValueProperty, position);
            }
            else
            {
                EditorGUI.PropertyField(position, variableProperty, GUIContent.none);
            }

            EditorGUI.EndProperty();
        }

        //Helper method to be called in Lambda expression in dropdown menu
        private void SetProperty(SerializedProperty property, bool value)
        {
            property.boolValue = value;
            property.serializedObject.ApplyModifiedProperties();
        }

        //Override this virtual class in each child class
        protected virtual void DrawConstantInputField(SerializedProperty constantValueProperty, Rect position) { }


    }
}
