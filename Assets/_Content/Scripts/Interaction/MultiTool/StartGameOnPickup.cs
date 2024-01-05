using System.Collections;
using System.Collections.Generic;
using GC.Events;
using UnityEngine;

public class StartGameOnPickup : MonoBehaviour
{
    [SerializeField]
    private VoidEvent startGameEvent;

    public void StartGame()
    {
        startGameEvent.Invoke();
    }
}