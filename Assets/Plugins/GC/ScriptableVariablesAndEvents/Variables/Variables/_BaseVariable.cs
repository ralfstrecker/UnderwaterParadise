using UnityEngine;
using GC.Events;

namespace GC.Variables
{
    public class BaseVariable<T, TGameEvent> : ScriptableObject where TGameEvent : BaseGameEvent<T>
    {

        [HideInInspector] public string description = "What is this variable used for?";

        public T initialValue;

        public T currentValue;

        public T Value
        {
            get => currentValue;
            set
            {
                currentValue = value;
                if ((eventEnabled && !onlyTriggerOnStrictValueChange) || (eventEnabled && !currentValue.Equals(value)))
                {
                    valueChangedEvent.Invoke(currentValue);
                }
            }
        }

        [Header("Event to be invoked when value is set")]
        [SerializeField] private bool eventEnabled;
        [SerializeField] private bool onlyTriggerOnStrictValueChange;
        public TGameEvent valueChangedEvent;



        private void OnEnable()
        {
            ResetToInitialValue();
        }        

        public void ResetToInitialValue()
        {
            currentValue = initialValue;
        }
    }
}