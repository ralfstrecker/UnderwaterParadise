using Sirenix.OdinInspector;
using UnityEngine;

public abstract class Garbage : MonoBehaviour
{
    [FoldoutGroup("References")]
    [Required]
    [SerializeField]
    private Collider[] colliders;

    [FoldoutGroup("References")]
    [Required]
    [SerializeField]
    private Rigidbody rb;

    [FoldoutGroup("References")]
    [Required]
    [SerializeField]
    private Outline outline;
    
    [field: SerializeField]
    public float HealthCost { get; private set; }

    [field: SerializeField]
    public int Points { get; private set; }

    public bool IsActive
    {
        get => _isActive;
        set
        {
            if (value != _isActive)
            {
                // foreach (Collider col in colliders)
                // {
                //     col.enabled = value;
                // }
                rb.isKinematic = !value;
                outline.enabled = value;
            }
            _isActive = value;
        }
    }
    private bool _isActive = true;
}