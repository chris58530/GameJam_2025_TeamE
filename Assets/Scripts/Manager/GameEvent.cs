using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    [SerializeField] List<IGameEvent> gameEvents = new List<IGameEvent>();
    IGameEvent nextGameEvent;
    IGameEvent currentGameEvent;
    public void StartGameEvent()
    {
        InvokeRepeating("DoGameEvent", 0f, 10f);
    }
    public void SetGameState(IGameEvent gameEvent)
    {
        nextGameEvent = gameEvent;
    }
    void DoGameEvent()
    {
        if (currentGameEvent != null)
        {
            currentGameEvent.EndGameEvent();
        }
        if (nextGameEvent != null)
        {
            nextGameEvent.StartGameEvent();
        }
        else
        {
            SetGameState(gameEvents[Random.Range(0, gameEvents.Count)]);
            nextGameEvent.StartGameEvent();
        }


        currentGameEvent = nextGameEvent;
        nextGameEvent = null;
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
