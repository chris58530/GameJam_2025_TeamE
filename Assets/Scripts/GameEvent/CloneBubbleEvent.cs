using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CloneBubbleEvent : MonoBehaviour, IGameEvent
{

    [SerializeField] private OtherBubbleBehaviour otherBubbleBehaviourPrefab;

    [SerializeField] private Transform minSpawnPosition;
    [SerializeField] private Transform maxSpawnPosition;

    bool canSpawn = false;
    void Start()
    {
        AddGameEvent();
        canSpawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canSpawn) return;

        if (Time.time % 3f < Time.deltaTime)
        {
            for (int i = 0; i < 10; i++)
            {
                Invoke("CloneBubble", i * 0.1f);
            }
        }
    }
    void CloneBubble()
    {

        Instantiate(otherBubbleBehaviourPrefab, GetRandomPosition() + Vector3.up * 10, Quaternion.Euler(-90, 0, 0));
    }
    Vector3 GetRandomPosition()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(minSpawnPosition.position.x, maxSpawnPosition.position.x),
            Random.Range(minSpawnPosition.position.y, maxSpawnPosition.position.y),
            Random.Range(minSpawnPosition.position.z, maxSpawnPosition.position.z)
        );
        return randomPosition;
    }

    public void StartGameEvent()
    {
        canSpawn = true;
    }

    public void EndGameEvent()
    {
        canSpawn = false;
    }

    public void AddGameEvent()
    {
       GameEvent gameEventComponent = FindObjectOfType<GameEvent>();
       gameEventComponent.AddGameEvent(this);
    }
}

