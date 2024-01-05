using System;
using UnityEngine;

public class GrabberToolTerrainCollider : MonoBehaviour
{
    public event Action OnHitTerrain;
    public GrabberTool GrabberTool { get; private set; }


    private void Awake()
    {
        GrabberTool = GetComponentInParent<GrabberTool>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Garbage garbage) && !other.isTrigger)
        {
            OnHitTerrain?.Invoke();
        }
    }
}