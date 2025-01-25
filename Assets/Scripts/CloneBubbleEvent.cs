using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneBubbleEvent : MonoBehaviour, IGameEvent
{

    [SerializeField] private OtherBubbleBehaviour otherBubbleBehaviourPrefab;

    [SerializeField] private Transform minSpawnPosition;
    [SerializeField] private Transform maxSpawnPosition;

    bool canSpawn = false;
    void Start()
    {
        canSpawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canSpawn) return;

        if (Time.time % 3 < Time.deltaTime)
        {
            Instantiate(otherBubbleBehaviourPrefab, GetRandomPosition(), Quaternion.identity);
        }
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
}

