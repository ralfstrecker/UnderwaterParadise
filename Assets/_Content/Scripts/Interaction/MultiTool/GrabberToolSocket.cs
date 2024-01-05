using System;
using UnityEngine;

public class GrabberToolSocket : MonoBehaviour
{
    public event Action<Garbage> OnHitGarbage;
    public GrabberTool GrabberTool { get; private set; }


    private void Awake()
    {
        GrabberTool = GetComponentInParent<GrabberTool>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Garbage garbage))
        {
            OnHitGarbage?.Invoke(garbage);
        }
    }
}