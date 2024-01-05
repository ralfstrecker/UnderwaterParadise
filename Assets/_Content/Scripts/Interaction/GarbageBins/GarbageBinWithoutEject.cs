using GC.Variables;
using Sirenix.OdinInspector;
using UnityEngine;

public class GarbageBinWithoutEject : MonoBehaviour
{
    [SerializeField]
    private GarbageType garbageType;

    [SerializeField]
    private float suctionForce = 150;

    [SerializeField]
    private IntVariable scoreCount;

    [BoxGroup("Sounds")]
    [SerializeField]
    private AudioClip audioClipSwoosh;
    
    [BoxGroup("Sounds")]
    [SerializeField]
    private AudioClip audioClipFeedbackPositive;
    
    [BoxGroup("Sounds")]
    [SerializeField]
    private AudioClip audioClipFeedbackNegative;

    [FoldoutGroup("References")]
    [SerializeField]
    private GarbageCollider suctionCollider;

    [FoldoutGroup("References")]
    [SerializeField]
    private GarbageCollider destructionCollider;

    [FoldoutGroup("References")]
    [SerializeField]
    private Transform suctionPoint;
    
    [FoldoutGroup("References")]
    [SerializeField]
    private AudioSource audioSourceSwoosh;

    [FoldoutGroup("References")]
    [SerializeField]
    private AudioSource audioSourceFeedback;

    private void Awake()
    {
        suctionCollider.OnGarbageTriggerStay += Suction;
        destructionCollider.OnGarbageTriggerEnter += Destruction;
    }

    private void Suction(Garbage garbage)
    {
        Vector3 force = suctionPoint.position - garbage.transform.position;
        garbage.GetComponent<Rigidbody>().AddForce(force * suctionForce);
    }

    private void Destruction(Garbage garbage)
    {
        if (!garbage.IsActive) return;

        audioSourceSwoosh.PlayOneShot(audioClipSwoosh);

        bool isCorrectKindOfGarbage = garbageType switch
        {
            GarbageType.Plastic => garbage.TryGetComponent(out PlasticGarbage _),
            GarbageType.Metal => garbage.TryGetComponent(out MetalGarbage _),
            _ => false
        };
        
        if (isCorrectKindOfGarbage)
        {
            
            garbage.IsActive = false;
            scoreCount.Value += garbage.Points;
        }

        audioSourceFeedback.PlayOneShot(isCorrectKindOfGarbage ? audioClipFeedbackPositive : audioClipFeedbackNegative);
        Destroy(garbage.gameObject);
    }

    private enum GarbageType
    {
        Plastic,
        Metal
    }
}