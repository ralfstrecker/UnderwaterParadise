using UnityEngine;
using UnityEditor;
using GC.Events;

namespace GC.Variables
{
    public class BaseVariableEditor<T, TVariable, TGameEvent> : Editor where TVariable : BaseVariable<T, TGameEvent> where TGameEvent : BaseGameEvent<T>
    {
        public override void OnInspectorGUI()
        {
            ////Draw base information
            base.OnInspectorGUI();

            //Get game event from inspected object
            TVariable variable = target as TVariable;

            //Draw description header
            GUILayout.Label("Description");

            //Get description from text field
            variable!.description = GUILayout.TextArea(variable.description);
        }
    }
}