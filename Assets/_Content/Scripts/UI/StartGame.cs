using GC.Events;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField]
    private GameMode gameMode;
    
    [SerializeField]
    private VoidEvent startGameEvent;

    [SerializeField]
    private Spawner spawner;

    [SerializeField]
    private GameObject garbageBins120;

    [SerializeField]
    private GameObject garbageBins360;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GrabberTool"))
        {
            GrabberToolSocket grabberToolSocket = other.GetComponent<GrabberToolSocket>();
            if (grabberToolSocket == null || !grabberToolSocket.GrabberTool.IsShootingOutwards) return;
            
            // Destroy all multi tools that are not grabbed
            MultiTool[] multiTools = (MultiTool[]) FindObjectsOfType(typeof(MultiTool));
            if (multiTools.Length < 1) return;
            foreach (MultiTool multiTool in multiTools)
            {
                multiTool.DeleteIfNotGrabbed();
            }
            
            // Configure spawner and bins according to game mode
            bool gameModeIs360 = gameMode == GameMode.Mode360;
            spawner.Angle = gameModeIs360 ? 360 : 120;
            garbageBins120.SetActive(!gameModeIs360);
            garbageBins360.SetActive(gameModeIs360);

            // Start game
            startGameEvent.Invoke();
        }
    }

    private enum GameMode
    {
        Mode120,
        Mode360
    }
}