using System;
using UnityEngine;

[Serializable]
public class HighscoreEntry
{
    [SerializeField]
    private int score;

    [SerializeField]
    private string name;

    [SerializeField]
    private bool akimbo;

    [SerializeField]
    private bool is360Degrees;

    public int Score => score;
    public string Name => name;
    public bool Akimbo => akimbo;
    public bool Is360Degrees => is360Degrees;

    public HighscoreEntry(int scorePlastic, int scoreMetal, string name, bool akimbo, bool is360Degrees)
    {
        this.score = (int) ((scorePlastic + scoreMetal) * 1.337f);
        this.name = name;
        this.akimbo = akimbo;
        this.is360Degrees = is360Degrees;
    }

    public override string ToString()
    {
        return
            $"{name} has {score} points with {(akimbo ? "2 Guns" : "1 Gun")} in {(is360Degrees ? "360 Degree" : "Front Only")} mode";
    }
}