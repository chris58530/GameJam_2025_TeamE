using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : MonoBehaviour
{
    public GameObject brush;
    public float spawnInterval = 1f; // 每次生成的間隔時間
    public float spawnHeight = 10f; // 生成高度
    public float spawnAreaWidth = 5f; // 生成區域的寬度（左右範圍）
    public int maxObjects = 10; // 場上最大物件數量

    private float timer;
    private List<GameObject> activeObjects = new List<GameObject>(); // 儲存當前場上的物件

    void Update()
    {
        // 計時器控制生成
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnObject();
            timer = 0f;
        }
    }

    void SpawnObject()
    {
        // 如果場上物件超過限制，刪除最早生成的物件
        if (activeObjects.Count >= maxObjects)
        {
            DestroyOldestObject();
        }

        // 隨機計算生成位置
        float randomX = Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2);
        Vector3 spawnPosition = new Vector3(randomX, spawnHeight, 0);

        // 生成物件
        GameObject spawnedObject = Instantiate(brush, spawnPosition, Quaternion.identity);
        activeObjects.Add(spawnedObject); // 添加到清單中

        // 確保物件有剛體
        if (spawnedObject.GetComponent<Rigidbody>() == null)
        {
            spawnedObject.AddComponent<Rigidbody>();
        }

        // 添加地面檢測行為
        if (spawnedObject.GetComponent<ObjectBehavior>() == null)
        {
            spawnedObject.AddComponent<ObjectBehavior>().SetSpawner(this);
        }
    }

    // 刪除最早生成的物件
    void DestroyOldestObject()
    {
        if (activeObjects.Count > 0)
        {
            GameObject oldestObject = activeObjects[0];
            activeObjects.RemoveAt(0);
            Destroy(oldestObject);
        }
    }

    // 當物件落地後從清單中移除
    public void RemoveObjectFromList(GameObject obj)
    {
        if (activeObjects.Contains(obj))
        {
            activeObjects.Remove(obj);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<OtherBubbleBehaviour>(out var bubble))
        {
            bubble.OnCollisionProps();
        }
        if (other.gameObject.TryGetComponent<PlayerBubbleBehaviour>(out var player))
        {
            player.OnCollisionProps();
        }
    }
    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.TryGetComponent<OtherBubbleBehaviour>(out var bubble))
    //    {
    //        bubble.OnCollisionProps();
    //    }
    //    if (other.gameObject.TryGetComponent<PlayerBubbleBehaviour>(out var player))
    //    {
    //        player.OnCollisionProps();
    //    }
    //}
}
