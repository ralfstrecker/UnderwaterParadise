using UnityEngine;

namespace GC.Events
{
    [CreateAssetMenu(fileName = "VoidEvent", menuName = "GC/Events/VoidEvent")]
    public class VoidEvent : BaseGameEvent<Void>
    {
        // Overloading Base Invoke(T item) to be able to send no data (void)
        public void Invoke()
        {
            Invoke(new Void());
        }
    }
}