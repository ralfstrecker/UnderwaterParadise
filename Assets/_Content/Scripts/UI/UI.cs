using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using GC.Variables;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [BoxGroup("Start Panel")]
    [SerializeField]
    private CanvasGroup startPanel;
    
    [BoxGroup("Go Panel")]
    [SerializeField]
    private CanvasGroup goPanel;

    [BoxGroup("Game Over Panel")]
    [SerializeField]
    private CanvasGroup gameOverPanel;

    [BoxGroup("Game Over Panel")]
    [SerializeField]
    private TMP_Text scoreText;

    [BoxGroup("Game Over Panel")]
    [SerializeField]
    private TMP_Text scorePlasticText;

    [BoxGroup("Game Over Panel")]
    [SerializeField]
    private TMP_Text scoreMetalText;

    [BoxGroup("Highscore Panel")]
    [SerializeField]
    private CanvasGroup highscorePanel;
    
    [BoxGroup("Highscore Panel")]
    [SerializeField]
    private TMP_Text highscoresTextLeftColumn;
    
    [BoxGroup("Highscore Panel")]
    [SerializeField]
    private TMP_Text highscoresTextRightColumn;
    
    [BoxGroup("Score References")]
    [SerializeField]
    private HighscoreManager highscoreManager;
    
    [BoxGroup("Score References")]
    [SerializeField]
    private IntVariable scorePlastic;

    [BoxGroup("Score References")]
    [SerializeField]
    private IntVariable scoreMetal;

    private void Start()
    {
        gameOverPanel.alpha = 0;
        gameOverPanel.gameObject.SetActive(false);

        startPanel.alpha = 0;
        startPanel.gameObject.SetActive(true);

        highscorePanel.alpha = 0;
        highscorePanel.gameObject.SetActive(true);

        goPanel.alpha = 0;
        goPanel.gameObject.SetActive(true);
        
        startPanel.DOFade(1, 1);
        highscorePanel.DOFade(1, 1);

        scorePlastic.Value = 0;
        scoreMetal.Value = 0;

        UpdateHighscoresDisplay();
    }

    public void StartGame()
    {
        DOTween.Sequence()
            .Append(startPanel.DOFade(0, 1))
            .AppendCallback(() => startPanel.gameObject.SetActive(false))
            .Append(goPanel.DOFade(1, .5f))
            .AppendInterval(1)
            .Append(goPanel.DOFade(0, .5f));
        highscorePanel.DOFade(0, 1);
    }

    public void GameOver()
    {
        // detect what mode was played (120 or 360, single or akimbo) and enter score accordingly
        bool akimbo = FindObjectsOfType(typeof(MultiTool)).Length == 2;
        Spawner spawner = (Spawner) FindObjectOfType(typeof(Spawner));
        bool modeIs360 = spawner.Angle == 360;
        highscoreManager.AddHighscoreWithRandomName(scorePlastic.Value,scoreMetal.currentValue,akimbo,modeIs360);
        HighscoreEntry highscoreEntry = highscoreManager.MostRecentEntry;
        
        scoreText.text = $"You scored\n<color=orange>{highscoreEntry.Score:N0}</color>!\nWell done,\n<b><u>{highscoreEntry.Name}</u></b>";
        scorePlasticText.text = $"Plastic\n{scorePlastic.Value}";
        scoreMetalText.text = $"Metal\n{scoreMetal.Value}";
        
        UpdateHighscoresDisplay();

        gameOverPanel.gameObject.SetActive(true);
        gameOverPanel.DOFade(1, 1);
        highscorePanel.DOFade(1, 1);
    }

    private void UpdateHighscoresDisplay()
    {
        // Sort highscores by descending score and get first eight entries
        IEnumerable<HighscoreEntry> sortedHighscoreEntries = highscoreManager.Highscores.OrderBy(entry => entry.Score).Reverse().Take(8);
        
        string highscoresStringLeft = " ";
        string highscoresStringRight = "";
        int index = 1;
        foreach (HighscoreEntry entry in sortedHighscoreEntries)
        {
            highscoresStringLeft += $"{index++}." +
                                (entry.Is360Degrees?Constants.TMPSprites.Mode360:Constants.TMPSprites.Mode120) +
                                (entry.Akimbo?Constants.TMPSprites.GunAkimbo:Constants.TMPSprites.Gun) +
                                $" <b>{entry.Name}</b>\n";
            highscoresStringRight += $"<color=orange>{entry.Score:N0}</color>\n";
        }

        highscoresTextLeftColumn.text = highscoresStringLeft;
        highscoresTextRightColumn.text = highscoresStringRight;
    }
}