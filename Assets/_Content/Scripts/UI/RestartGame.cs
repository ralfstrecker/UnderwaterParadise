using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GrabberTool"))
        {
            if (!other.GetComponent<GrabberToolSocket>().GrabberTool.IsShootingOutwards) return;
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}