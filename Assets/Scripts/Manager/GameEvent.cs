using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    [SerializeField] List<IGameEvent> gameEvents = new List<IGameEvent>();

    IGameEvent nextGameEvent;
    public void StartGameEvent()
    {
        InvokeRepeating("DoGameEvent", 0f, 3f);
    }
    public void SetGameState(IGameEvent gameEvent)
    {
        nextGameEvent = gameEvent;
    }
    void DoGameEvent()
    {
        if (nextGameEvent != null)
        {
            nextGameEvent.StartGameEvent();
        }
        else
        {
            Debug.Log("no assign game event");
            SetGameState(gameEvents[Random.Range(0, gameEvents.Count)]);
            nextGameEvent.StartGameEvent();
        }

    }
    public void AddGameEvent(IGameEvent gameEvent)
    {
        gameEvents.Add(gameEvent);
    }
    public void EndGameEvent()
    {
        CancelInvoke();
    }
}
