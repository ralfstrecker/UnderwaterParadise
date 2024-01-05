using System;
using UnityEngine;

public class GarbageCollider : MonoBehaviour
{
    public event Action<Garbage> OnGarbageTriggerEnter;
    public event Action<Garbage> OnGarbageTriggerStay;
    public event Action<Garbage> OnGarbageTriggerExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Garbage garbage))
        {
            OnGarbageTriggerEnter?.Invoke(garbage);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Garbage garbage))
        {
            OnGarbageTriggerStay?.Invoke(garbage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Garbage garbage))
        {
            OnGarbageTriggerExit?.Invoke(garbage);
        }
    }
}
