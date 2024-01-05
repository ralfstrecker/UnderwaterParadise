using GC.Variables;
using UnityEngine;

public class TerrainGarbageDetector : MonoBehaviour
{
    [SerializeField]
    private FloatVariable playerHealth;
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent(out Garbage garbage)) return;
        
        playerHealth.Value -= garbage.HealthCost;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent(out Garbage garbage)) return;
        
        playerHealth.Value += garbage.HealthCost;
    }
}