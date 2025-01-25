using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    [SerializeField] List<IGameEvent> gameEvents = new List<IGameEvent>();

    IGameEvent nextGameEvent;
    public void StartGameEvent()
    {

    }
    void DoGameEvent()
    {
        if (nextGameEvent != null)
        {
            nextGameEvent.StartGameEvent();
        }
    }
    public void EndGameEvent()
    {

    }
}
