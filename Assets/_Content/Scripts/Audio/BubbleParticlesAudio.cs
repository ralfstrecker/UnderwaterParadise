using Sirenix.OdinInspector;
using UnityEngine;

public class BubbleParticlesAudio : MonoBehaviour
{
    [FoldoutGroup("References")]
    [Required]
    [SerializeField]
    private AudioSource audioSource;

    [FoldoutGroup("References")]
    [Required]
    [SerializeField]
    private ParticleSystem particles;

    private void Update()
    {
        audioSource.volume = Mathf.Lerp(.5f, 1f, (float) particles.particleCount / particles.main.maxParticles);
    }
}