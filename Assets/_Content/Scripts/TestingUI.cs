using GC.Variables;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class TestingUI : MonoBehaviour
{
    [FoldoutGroup("Scene References")]
    [SerializeField]
    private TMP_Text healthText;

    [FoldoutGroup("Scene References")]
    [SerializeField]
    private TMP_Text scoreText;

    [FoldoutGroup("Scene References")]
    [SerializeField]
    private TMP_Text timeText;

    [FoldoutGroup("Variable References")]
    [SerializeField]
    private FloatVariable playerHealth;

    [FoldoutGroup("Variable References")]
    [SerializeField]
    private IntVariable scorePlastic;

    [FoldoutGroup("Variable References")]
    [SerializeField]
    private IntVariable scoreMetal;


    private void Update()
    {
        healthText.text = $"{playerHealth.Value}/{playerHealth.initialValue} Health";
        scoreText.text = $"{scorePlastic.Value} Plastic\n{scoreMetal.Value} Metal";
        timeText.text = $"{Time.timeSinceLevelLoad:0.0} s";
    }
}