using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class Brush: MonoBehaviour
{
    public GameObject prefab; // 要生成的物件
    public float spawnInterval = 1f; // 每次生成的間隔時間
    public List<Transform> spawnPoints; // 生成點清單
    public int maxObjects = 10; // 場上最大物件數量
    public UnityEvent onSpawningComplete; // 生成結束的事件

    private float timer;
    private float totalSpawnTime = 10f; // 總生成時間
    private float elapsedTime = 0f; // 經過的時間
    private List<GameObject> activeObjects = new List<GameObject>();

    void Update()
    {
        // 如果生成時間已經結束，停止生成
        if (elapsedTime >= totalSpawnTime)
        {

            return;
        }

        // 計時器控制生成
        timer += Time.deltaTime;
        elapsedTime += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObjectsAtAllPoints();
            timer = 0f;
        }

        // 檢查是否超過總生成時間
        if (elapsedTime >= totalSpawnTime)
        {
            onSpawningComplete.Invoke(); // 觸發生成結束事件
        }
    }

    void SpawnObjectsAtAllPoints()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            if (activeObjects.Count >= maxObjects)
            {
                DestroyOldestObject();
            }

            // 在生成點位置生成物件
            GameObject spawnedObject = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            activeObjects.Add(spawnedObject);

            // 添加剛體並啟用重力
            Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = spawnedObject.AddComponent<Rigidbody>();
            }
            rb.useGravity = true; // 啟用重力
            rb.constraints = RigidbodyConstraints.None; // 確保不鎖定位置或旋轉
        }
    }

    void DestroyOldestObject()
    {
        if (activeObjects.Count > 0)
        {
            GameObject oldestObject = activeObjects[0];
            activeObjects.RemoveAt(0);
            Destroy(oldestObject);
        }
    }

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
