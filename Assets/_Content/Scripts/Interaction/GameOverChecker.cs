using UnityEngine;
using GC.Events;
public class GameOverChecker : MonoBehaviour
{
    [SerializeField]
    private VoidEvent gameOverEvent;

    private bool _isGameOver;

    public void CheckForGameOver(float health)
    {
        if (_isGameOver) return;
        
        if (health <= 0)
        {
            _isGameOver = true;
            gameOverEvent.Invoke();
            
            Garbage[] remainingGarbage = (Garbage[]) FindObjectsOfType(typeof(Garbage));
            foreach (Garbage garbage in remainingGarbage)
            {
                garbage.IsActive = false;
                garbage.GetComponent<Collider>().enabled = false;
            }
        }
    }
}
