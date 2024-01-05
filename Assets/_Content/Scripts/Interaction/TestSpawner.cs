using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objectsToSpawn;

    [SerializeField]
    private float spawnEveryXSeconds;

    [SerializeField]
    private bool randomizeRotation = true;

    private void Awake()
    {
        InvokeRepeating(nameof(Spawn), 0, spawnEveryXSeconds);
    }

    private void Spawn()
    {
        GameObject clone = Instantiate(objectsToSpawn[Random.Range(0, objectsToSpawn.Length)], transform);
        if (randomizeRotation)
        {
            clone.transform.rotation = Random.rotation;
        }
    }
}