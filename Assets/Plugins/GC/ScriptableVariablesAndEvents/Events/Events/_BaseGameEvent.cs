using System.Collections.Generic;
using UnityEngine;

namespace GC.Events
{
    public class BaseGameEvent<T> : ScriptableObject
    {

#if UNITY_EDITOR
        // Description to be used in custom inspector
        [HideInInspector]
        public string description = "What is this event used for?";

        // Debug variable to be used in custom inspector
        [HideInInspector]
        public T debugValue;
#endif


        // List to store all event listeners
        private readonly List<IBaseGameEventListener<T>> _listeners = new List<IBaseGameEventListener<T>>();


        public void Invoke(T item)
        {
            foreach (IBaseGameEventListener<T> t in _listeners)
            {
                t.OnEventInvoked(item);
            }
        }

        public void AddListener(IBaseGameEventListener<T> listener)
        {
            if (_listeners.Contains(listener)) return;
            _listeners.Add(listener);
        }

        public void RemoveListener(IBaseGameEventListener<T> listener)
        {
            if (!_listeners.Contains(listener)) return;
            _listeners.Remove(listener);
        }
    }
}