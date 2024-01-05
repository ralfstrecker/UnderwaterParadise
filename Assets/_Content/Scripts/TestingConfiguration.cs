#if UNITY_EDITOR

using GC.Variables;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class TestingConfiguration : MonoBehaviour
{
    [InfoBox("A = 120°, No Eject\nB = 120°, Eject\nC = 360°, Eject")]
    
    [EnumToggleButtons]
    [OnValueChanged(nameof(UpdateTestCase))]
    [SerializeField]
    private TestCase testCase;

    [OnValueChanged(nameof(UpdateHealth))]
    [SerializeField]
    private int startingHealth = 50;
    
    [OnValueChanged(nameof(UpdateSpawnSpeed))]
    [SuffixLabel("s between each spawn")]
    [SerializeField]
    private float spawnInterval = 2;
    
    

    [FoldoutGroup("Scene References")]
    [SerializeField]
    private GameObject garbageBinsTestCaseA;

    [FoldoutGroup("Scene References")]
    [SerializeField]
    private GameObject garbageBinsTestCaseB;

    [FoldoutGroup("Scene References")]
    [SerializeField]
    private GameObject garbageBinsTestCaseC;

    [FoldoutGroup("Scene References")]
    [SerializeField]
    private Spawner spawner;

    [FoldoutGroup("Asset References")]
    [SerializeField]
    private FloatVariable playerHealth;

    private void UpdateTestCase()
    {
        garbageBinsTestCaseA.SetActive(testCase == TestCase.A);
        garbageBinsTestCaseB.SetActive(testCase == TestCase.B);
        garbageBinsTestCaseC.SetActive(testCase == TestCase.C);

        spawner.Angle = testCase == TestCase.C ? 360 : 120;

        SceneView.RepaintAll();
        PrefabUtility.RecordPrefabInstancePropertyModifications(spawner);
        EditorUtility.SetDirty(spawner.gameObject);
        EditorSceneManager.SaveOpenScenes();
    }

    private void UpdateHealth()
    {
        playerHealth.initialValue = startingHealth;
        playerHealth.Value = startingHealth;
    }
    
    private void UpdateSpawnSpeed()
    {
        spawner.SpawnEveryXSeconds = spawnInterval;
        PrefabUtility.RecordPrefabInstancePropertyModifications(spawner);
        EditorUtility.SetDirty(spawner.gameObject);
        EditorSceneManager.SaveOpenScenes();
    }

    private enum TestCase
    {
        A, // 120°, No Eject
        B, // 120°, Eject
        C  // 360°, Eject
    }
}

#endif