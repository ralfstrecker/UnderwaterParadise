using UnityEngine;
using UnityEditor;

namespace GC.Events
{
    public class BaseEventEditor<T, TGameEvent> : Editor where TGameEvent : BaseGameEvent<T>
    {
        public override void OnInspectorGUI()
        {
            ////Draw base information
            base.OnInspectorGUI();

            //Get game event from inspected object
            TGameEvent gameEvent = target as TGameEvent;

            //Draw description header
            GUILayout.Label("Description");

            //Get description from text field
            gameEvent!.description = GUILayout.TextArea(gameEvent.description);
            
            
            GUILayout.Space(30);
            //Draw debugging header
            GUILayout.Label("Debugging");

            //Get debug value from input field
            gameEvent.debugValue = DrawDebugInputField("Debug Value", gameEvent.debugValue);


            if (GUILayout.Button("Raise Event with Debug Value"))
            {
                gameEvent.Invoke(gameEvent.debugValue);
            }
        }

        //Override this virtual class in each child class
        protected virtual T DrawDebugInputField(string label, T debugValue) { T val = default; return val; }
    }
}