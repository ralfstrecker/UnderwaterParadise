using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objsToSpawn;

    [SerializeField]
    private int angle;

    [SerializeField]
    private int maxDistance;

    [SerializeField]
    private int minDistance;

    [SerializeField]
    private float height;

    [SerializeField]
    private float spawnEveryXSeconds;

    public int Angle
    {
        get => angle;
        set => angle = value;
    }

    public float SpawnEveryXSeconds
    {
        get => spawnEveryXSeconds;
        set => spawnEveryXSeconds = value;
    }

    private bool _gameHasStarted;
    private bool _gameIsOver;

    public void StartSpawning()
    {
        if (_gameHasStarted) return;

        _gameHasStarted = true;
        InvokeRepeating(nameof(Spawn), 0, spawnEveryXSeconds);
    }

    public void StopSpawning()
    {
        if (_gameIsOver) return;

        _gameIsOver = true;
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        float randAngle = Random.Range(-angle / 2, angle / 2);
        float randDistance = Random.Range(minDistance, maxDistance);
        float x = Mathf.Cos((Mathf.PI / 180) * randAngle) * randDistance;
        float z = Mathf.Sin((Mathf.PI / 180) * randAngle) * randDistance;

        int randomIndex = Random.Range(0, objsToSpawn.Length);
        Vector3 randomSpawnPosition = new Vector3(x, height, z);

        Instantiate(objsToSpawn[randomIndex], randomSpawnPosition, Random.rotation);
    }
}