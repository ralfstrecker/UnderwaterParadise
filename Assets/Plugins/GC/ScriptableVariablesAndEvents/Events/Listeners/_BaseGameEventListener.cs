using UnityEngine;
using UnityEngine.Events;

namespace GC.Events
{
    public abstract class BaseGameEventListener<T, TGameEvent, TUnityEvent> : MonoBehaviour, IBaseGameEventListener<T> where TGameEvent : BaseGameEvent<T> where TUnityEvent : UnityEvent<T>
    {
        [SerializeField] private TGameEvent gameEvent;
        public TGameEvent GameEvent
        {
            get => gameEvent;
            set => gameEvent = value;
        }

        [SerializeField] private TUnityEvent unityEvent;

        private void OnEnable()
        {
            if (gameEvent != null)
            {
                GameEvent.AddListener(this);
            }
        }

        private void OnDisable()
        {
            if (gameEvent != null)
            {
                GameEvent.RemoveListener(this);
            }
        }

        public void OnEventInvoked(T item)
        {
            if (unityEvent != null)
            {
                unityEvent.Invoke(item);
            }
        }
    }
}