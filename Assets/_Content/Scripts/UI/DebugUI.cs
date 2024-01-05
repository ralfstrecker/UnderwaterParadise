using GC.Variables;
using TMPro;
using UnityEngine;

public class DebugUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text healthText;
    
    [SerializeField]
    private TMP_Text scoreText;

    [SerializeField]
    private FloatVariable playerHealth;

    private int _scorePlastic;
    private int _scoreMetal;
    
    private void Awake()
    {
        UpdateHealthDisplay(playerHealth.currentValue);
    }

    public void UpdateHealthDisplay(float health)
    {
        healthText.text = $"Health: {health:0.##}";
    }

    public void UpdateScoreDisplayPlastic(int score)
    {
        _scorePlastic = score;
        UpdateScoreDisplay();
    }
    
    public void UpdateScoreDisplayMetal(int score)
    {
        _scoreMetal = score;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        scoreText.text = $"{_scorePlastic} Plastic\n{_scoreMetal} Metal";
    }
}
