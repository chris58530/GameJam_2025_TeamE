using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : MonoBehaviour
{
    public GameObject brush;
    public float spawnInterval = 1f; // �C���ͦ������j�ɶ�
    public float spawnHeight = 10f; // �ͦ�����
    public float spawnAreaWidth = 5f; // �ͦ��ϰ쪺�e�ס]���k�d��^
    public int maxObjects = 10; // ���W�̤j����ƶq

    private float timer;
    private List<GameObject> activeObjects = new List<GameObject>(); // �x�s��e���W������

    void Update()
    {
        // �p�ɾ�����ͦ�
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnObject();
            timer = 0f;
        }
    }

    void SpawnObject()
    {
        // �p�G���W����W�L����A�R���̦��ͦ�������
        if (activeObjects.Count >= maxObjects)
        {
            DestroyOldestObject();
        }

        // �H���p��ͦ���m
        float randomX = Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2);
        Vector3 spawnPosition = new Vector3(randomX, spawnHeight, 0);

        // �ͦ�����
        GameObject spawnedObject = Instantiate(brush, spawnPosition, Quaternion.identity);
        activeObjects.Add(spawnedObject); // �K�[��M�椤

        // �T�O���󦳭���
        if (spawnedObject.GetComponent<Rigidbody>() == null)
        {
            spawnedObject.AddComponent<Rigidbody>();
        }

        // �K�[�a���˴��欰
        if (spawnedObject.GetComponent<ObjectBehavior>() == null)
        {
            spawnedObject.AddComponent<ObjectBehavior>().SetSpawner(this);
        }
    }

    // �R���̦��ͦ�������
    void DestroyOldestObject()
    {
        if (activeObjects.Count > 0)
        {
            GameObject oldestObject = activeObjects[0];
            activeObjects.RemoveAt(0);
            Destroy(oldestObject);
        }
    }

    // ���󸨦a��q�M�椤����
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
