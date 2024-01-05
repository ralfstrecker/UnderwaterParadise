using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class HighscoreManager : MonoBehaviour
{
    [BoxGroup("Random Name Generation")]
    [Required]
    [SerializeField]
    private TextAsset adjectiveList;
    
    [BoxGroup("Random Name Generation")]
    [Required]
    [SerializeField]
    private TextAsset nounList;
    
    private string _jsonFilepath;
    private List<HighscoreEntry> _highscores;
    private List<string> _adjectives;
    private List<string> _nouns;

    public List<HighscoreEntry> Highscores => _highscores;
    public HighscoreEntry MostRecentEntry => _highscores[_highscores.Count - 1];
    
    private void Awake()
    {
        _jsonFilepath = $"{Application.persistentDataPath}{Path.DirectorySeparatorChar}unencrypted_highscores.json";
        Debug.Log($"Storing highscores at {_jsonFilepath}");
        _highscores = ReadHighscoresFromDisk();

        _adjectives = GetStringListFromLines(adjectiveList);
        _nouns = GetStringListFromLines(nounList);
    }

    private static List<string> GetStringListFromLines(TextAsset textAsset)
    {
        string content = textAsset.text;
        string[] lines = content.Split('\n');
        return new List<string>(lines);
    }

    private List<HighscoreEntry> ReadHighscoresFromDisk()
    {
        if (!File.Exists(_jsonFilepath)) return new List<HighscoreEntry>();
        
        string json = File.ReadAllText(_jsonFilepath);
        HighscoresWrapper wrapper = JsonUtility.FromJson<HighscoresWrapper>(json);
        return wrapper.highscores;
    }

    private void WriteHighscoresToDisk(List<HighscoreEntry> highscores)
    {
        HighscoresWrapper wrapper = new HighscoresWrapper(highscores);
        string json = JsonUtility.ToJson(wrapper);
        File.WriteAllText(_jsonFilepath, json);
    }

    public void AddHighscore(int scorePlastic, int scoreMetal, string playerName, bool akimbo, bool is360)
    {
        HighscoreEntry highscore = new HighscoreEntry(scorePlastic, scoreMetal, playerName, akimbo, is360);
        _highscores.Add(highscore);
        WriteHighscoresToDisk(_highscores);
    }

    public void AddHighscoreWithRandomName(int scorePlastic, int scoreMetal, bool akimbo, bool is360)
    {
        AddHighscore(scorePlastic, scoreMetal,GenerateUniqueRandomName(),akimbo,is360);
    }

    private string GenerateUniqueRandomName()
    {
        string randomName = string.Empty;
        int tries = 0;
        
        while (tries++<50)
        {
            string randomAdjective = _adjectives[Random.Range(0, _adjectives.Count)].Trim();
            string randomNoun = _nouns[Random.Range(0, _nouns.Count)].Trim();

            randomName = $"{randomAdjective} {randomNoun}";
            bool nameAlreadyExists = _highscores.Any(entry => entry.Name.Equals(randomName));

            if (!nameAlreadyExists) break;
        }

        return tries < 50? randomName : "ALL RANDOM NAMES TAKEN";
    }

    [Button]
    private void PrintHighscores()
    {
        Debug.Log($"Total of {_highscores.Count} scores:");
        foreach (HighscoreEntry highscoreEntry in _highscores)
        {
            Debug.Log(highscoreEntry);
        }
    }

    [Button]
    private void AddRandomHighscore()
    {
        AddHighscoreWithRandomName(Random.Range(1000,100000), Random.Range(1000,100000), Random.value > .5f,Random.value > .5f);
    }

    [Button]
    private void ClearHighscores()
    {
        _highscores = new List<HighscoreEntry>();
        WriteHighscoresToDisk(_highscores);
    }

    [Serializable]
    private class HighscoresWrapper
    {
        public List<HighscoreEntry> highscores;
        
        public HighscoresWrapper(List<HighscoreEntry> highscores)
        {
            this.highscores = highscores;
        }
    }
}