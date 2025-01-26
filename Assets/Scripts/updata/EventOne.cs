using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EventOne : MonoBehaviour
{
    [SerializeField] private GameObject brushPrefab; // 生成的泡泡預置物件
    [SerializeField] private Transform minSpawnPosition; // 最小生成位置
    [SerializeField] private Transform maxSpawnPosition; // 最大生成位置

    private bool canSpawn = false; // 控制是否可以生成
    private float spawnInterval = 0.3f; // 每次生成的間隔時間
    private Coroutine spawnCoroutine; // 儲存 Coroutine 的引用

    void Update()
    {

    }

    // 開始生成
    public void StartGameEvent()
    {
        if (!canSpawn)
        {
            canSpawn = true;
            spawnCoroutine = StartCoroutine(SpawnBubbles());
        }
    }

    // 停止生成
    public void EndGameEvent()
    {
        canSpawn = false;
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
    }

    // 生成刷子的 Coroutine
    private IEnumerator SpawnBubbles()
    {
        while (canSpawn)
        {
            for (int i = 0; i < 10; i++) // 每次生成 10 個
            {
                CloneBubble();
                yield return new WaitForSeconds(spawnInterval); // 每 0.3 秒生成一個
            }

            yield return new WaitForSeconds(3f); // 每次生成 10 個後，等待 3 秒
        }
    }

    // 生成單個刷子
    void CloneBubble()
    {
        Vector3 spawnPosition = GetRandomPosition() + Vector3.up * 10; // 生成在上方
        Instantiate(brushPrefab, spawnPosition, Quaternion.identity);
    }

    // 隨機生成位置
    Vector3 GetRandomPosition()
    {
        return new Vector3(
            Random.Range(minSpawnPosition.position.x, maxSpawnPosition.position.x),
            Random.Range(minSpawnPosition.position.y, maxSpawnPosition.position.y),
            Random.Range(minSpawnPosition.position.z, maxSpawnPosition.position.z)
        );
    }
}