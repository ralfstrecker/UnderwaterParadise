using DG.Tweening;
using UnityEngine;
using GC.Variables;
using Sirenix.OdinInspector;

public class WorldHealthBar : MonoBehaviour
{
    [SerializeField]
    private FloatVariable playerHealth;

    [BoxGroup("Materials")]
    [SerializeField]
    private Material dynamicBaseMaterial;

    [BoxGroup("Materials")]
    [SerializeField]
    private Material dynamicGradientMaterial;

    [BoxGroup("Fog")]
    [SerializeField]
    private Gradient fogGradient;

    [BoxGroup("Fog")]
    [SerializeField]
    private Vector2 fogDensity;

    [BoxGroup("Floating Particles")]
    [SerializeField]
    private ParticleSystem floatingParticles;

    [BoxGroup("Floating Particles")]
    [SerializeField]
    private Vector2 floatingParticlesEmissionRate;

    private bool _isGameOver;

    private void Awake()
    {
        playerHealth.Value = playerHealth.initialValue;
        FadeIn();
    }

    private void OnDisable()
    {
        ResetHealthBar();
    }

    private void OnValidate()
    {
        ResetHealthBar();
    }

    public void UpdateHealthBar(float currentHealth)
    {
        if (_isGameOver) return;

        float healthPercentage = currentHealth / playerHealth.initialValue;

        // Materials
        Color materialTintColor = Color.Lerp(Color.black, Color.white, healthPercentage);
        dynamicGradientMaterial.color = materialTintColor;
        dynamicBaseMaterial.color = materialTintColor;

        // Fog
        Color fogColor = fogGradient.Evaluate(healthPercentage);
        RenderSettings.fogColor = fogColor;
        RenderSettings.fogDensity = Mathf.Lerp(fogDensity.x, fogDensity.y, healthPercentage);

        // Floating particles
        if (floatingParticles != null)
        {
            ParticleSystem.EmissionModule floatingParticlesEmission = floatingParticles.emission;
            floatingParticlesEmission.rateOverTime = Mathf.Lerp(floatingParticlesEmissionRate.x,
                floatingParticlesEmissionRate.y, healthPercentage);
        }
    }

    private void ResetHealthBar()
    {
        UpdateHealthBar(playerHealth.initialValue);
    }

    public void GameOver()
    {
        _isGameOver = true;
        DOTween.To(() => RenderSettings.fogDensity, x => RenderSettings.fogDensity = x, .6f, 2);
    }

    private void FadeIn()
    {
        RenderSettings.fogDensity = 1;
        DOTween.To(() => RenderSettings.fogDensity, x => RenderSettings.fogDensity = x, fogDensity.y, 2);
    }

    public void FadeOut()
    {
        DOTween.To(() => RenderSettings.fogDensity, x => RenderSettings.fogDensity = x, 1, 2);
        DOTween.To(() => RenderSettings.fogColor, x => RenderSettings.fogColor = x, fogGradient.Evaluate(1), 2);
    }
}