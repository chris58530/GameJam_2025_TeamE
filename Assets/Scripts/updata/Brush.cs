using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class Brush: MonoBehaviour
{
    public GameObject prefab; // �n�ͦ�������
    public float spawnInterval = 1f; // �C���ͦ������j�ɶ�
    public List<Transform> spawnPoints; // �ͦ��I�M��
    public int maxObjects = 10; // ���W�̤j����ƶq
    public UnityEvent onSpawningComplete; // �ͦ��������ƥ�

    private float timer;
    private float totalSpawnTime = 10f; // �`�ͦ��ɶ�
    private float elapsedTime = 0f; // �g�L���ɶ�
    private List<GameObject> activeObjects = new List<GameObject>();

    void Update()
    {
        // �p�G�ͦ��ɶ��w�g�����A����ͦ�
        if (elapsedTime >= totalSpawnTime)
        {

            return;
        }

        // �p�ɾ�����ͦ�
        timer += Time.deltaTime;
        elapsedTime += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObjectsAtAllPoints();
            timer = 0f;
        }

        // �ˬd�O�_�W�L�`�ͦ��ɶ�
        if (elapsedTime >= totalSpawnTime)
        {
            onSpawningComplete.Invoke(); // Ĳ�o�ͦ������ƥ�
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

            // �b�ͦ��I��m�ͦ�����
            GameObject spawnedObject = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            activeObjects.Add(spawnedObject);

            // �K�[����ñҥέ��O
            Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = spawnedObject.AddComponent<Rigidbody>();
            }
            rb.useGravity = true; // �ҥέ��O
            rb.constraints = RigidbodyConstraints.None; // �T�O����w��m�α���
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
            // player.OnCollisionProps();
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
