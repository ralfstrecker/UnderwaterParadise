using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TestingSceneLoader : MonoBehaviour
{
    private void Update()
    {
        if (Keyboard.current[Key.F1].wasPressedThisFrame) SceneManager.LoadScene(0);
        if (Keyboard.current[Key.F2].wasPressedThisFrame) SceneManager.LoadScene(1);
        if (Keyboard.current[Key.F3].wasPressedThisFrame) SceneManager.LoadScene(2);
    }
}