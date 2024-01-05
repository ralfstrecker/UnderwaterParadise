using DG.Tweening;
using GC.Variables;
using Sirenix.OdinInspector;
using UnityEngine;

public class GarbageBinCombinedFeedback : MonoBehaviour
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
    
    [BoxGroup("Sounds")]
    [SerializeField]
    private AudioClip audioClipFeedbackEject;
    
    [BoxGroup("Sounds")]
    [SerializeField]
    private AudioClip audioClipFeedbackPositive;
    
    [BoxGroup("Sounds")]
    [SerializeField]
    private AudioClip audioClipFeedbackNegative;

    [BoxGroup("Light")]
    [SerializeField]
    private Color lightColorFeedbackPositive;
    
    [BoxGroup("Light")]
    [SerializeField]
    private Color lightColorFeedbackNegative;

    [BoxGroup("Light")]
    [SerializeField]
    private float lightMaxIntensity = 1f;
    
    [BoxGroup("Light")]
    [SerializeField]
    private float lightBlinkDuration = .4f;

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
    
    [FoldoutGroup("References")]
    [SerializeField]
    private AudioSource audioSourceFeedback;

    [FoldoutGroup("References")]
    [SerializeField]
    private Light feedbackLight;

    private void Awake()
    {
        suctionCollider.OnGarbageTriggerStay += Suction;
        destructionCollider.OnGarbageTriggerEnter += Destruction;
        feedbackLight.intensity = 0;
    }

    private void Suction(Garbage garbage)
    {
        Vector3 force = suctionPoint.position - garbage.transform.position;
        garbage.GetComponent<Rigidbody>().AddForce(force * suctionForce);
    }

    private void Destruction(Garbage garbage)
    {
        if (!garbage.IsActive) return;

        bool isCorrectKindOfGarbage = garbageType switch
        {
            GarbageType.Plastic => garbage.TryGetComponent(out PlasticGarbage _),
            GarbageType.Metal => garbage.TryGetComponent(out MetalGarbage _),
            _ => false
        };

        ProcessGarbage(garbage, isCorrectKindOfGarbage);
        PlayAuditoryFeedback(isCorrectKindOfGarbage);
        PlayVisualFeedback(isCorrectKindOfGarbage);
    }

    private void ProcessGarbage(Garbage garbage, bool isCorrectKindOfGarbage)
    {
        // Destroy and count points if correct garbage otherwise eject from back
        if (isCorrectKindOfGarbage)
        {
            garbage.IsActive = false;
            scoreCount.Value += garbage.Points;
            
            Destroy(garbage.gameObject);
        }
        else
        {
            garbage.transform.position = ejectTransform.position;
            Rigidbody garbageRigidbody = garbage.GetComponent<Rigidbody>();
            
            float randomStrength = Random.Range(ejectionStrength.x, ejectionStrength.y);
            Vector3 randomDeviation = Vector3.right * Random.Range(-.2f,.2f);
            Vector3 ejectTo = ejectTransform.rotation * (Vector3.forward + randomDeviation) * randomStrength;

            garbageRigidbody.DOMove(ejectTo, randomStrength/8).SetEase(Ease.OutCubic);
        }
    }

    private void PlayVisualFeedback(bool isCorrectKindOfGarbage)
    {
        feedbackLight.intensity = 0;
        feedbackLight.color = isCorrectKindOfGarbage ? lightColorFeedbackPositive : lightColorFeedbackNegative;

        float halfBlinkDuration = lightBlinkDuration / 2;
        
        DOTween.Sequence()
            .Append(feedbackLight.DOIntensity(lightMaxIntensity, halfBlinkDuration))
            .Append(feedbackLight.DOIntensity(0, halfBlinkDuration));
    }

    private void PlayAuditoryFeedback(bool isCorrectKindOfGarbage)
    {
        audioSourceSwoosh.PlayOneShot(audioClipSwoosh);
        audioSourceGrinder.PlayOneShot(isCorrectKindOfGarbage? audioClipFeedbackGrinder : audioClipFeedbackEject);
        audioSourceFeedback.PlayOneShot(isCorrectKindOfGarbage? audioClipFeedbackPositive : audioClipFeedbackNegative);
    }

    private enum GarbageType
    {
        Plastic,
        Metal
    }
}