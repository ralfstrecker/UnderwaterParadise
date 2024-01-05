using GC.Variables;
using Sirenix.OdinInspector;
using UnityEngine;

public class GarbageBinWithEject : MonoBehaviour
{
    [SerializeField]
    private GarbageType garbageType;

    [SerializeField]
    private float suctionForce = 150;
    
    [MinMaxSlider(0,200, true)]
    [SerializeField]
    private Vector2 ejectionStrength = new Vector2(60,100);

    [SerializeField]
    private IntVariable scoreCount;

    [BoxGroup("Sounds")]
    [SerializeField]
    private AudioClip audioClipSwoosh;
    
    [BoxGroup("Sounds")]
    [SerializeField]
    private AudioClip audioClipFeedbackGrinder;

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
    private Transform ejectTransform;
    
    [FoldoutGroup("References")]
    [SerializeField]
    private AudioSource audioSourceSwoosh;

    [FoldoutGroup("References")]
    [SerializeField]
    private AudioSource audioSourceGrinder;

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
            audioSourceGrinder.PlayOneShot(audioClipFeedbackGrinder);
            Destroy(garbage.gameObject);
        }
        else
        {
            garbage.transform.position = ejectTransform.position;
            Rigidbody garbageRigidbody = garbage.GetComponent<Rigidbody>();

            float randomStrength = Random.Range(ejectionStrength.x, ejectionStrength.y);
            if (garbageType == GarbageType.Metal) randomStrength *= 2;
            Vector3 randomDeviation = Vector3.right * Random.Range(-.2f,.2f);
            
            garbageRigidbody.AddForce(ejectTransform.rotation * (Vector3.forward + randomDeviation) * randomStrength, ForceMode.Impulse);
        }

    }

    private enum GarbageType
    {
        Plastic,
        Metal
    }
}