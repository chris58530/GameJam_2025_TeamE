using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EventOne : MonoBehaviour
{
    [SerializeField] private GameObject brushPrefab; // �ͦ����w�w�w�m����
    [SerializeField] private Transform minSpawnPosition; // �̤p�ͦ���m
    [SerializeField] private Transform maxSpawnPosition; // �̤j�ͦ���m

    private bool canSpawn = false; // ����O�_�i�H�ͦ�
    private float spawnInterval = 0.3f; // �C���ͦ������j�ɶ�
    private Coroutine spawnCoroutine; // �x�s Coroutine ���ޥ�

    void Update()
    {

    }

    // �}�l�ͦ�
    public void StartGameEvent()
    {
        if (!canSpawn)
        {
            canSpawn = true;
            spawnCoroutine = StartCoroutine(SpawnBubbles());
        }
    }

    // ����ͦ�
    public void EndGameEvent()
    {
        canSpawn = false;
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
    }

    // �ͦ���l�� Coroutine
    private IEnumerator SpawnBubbles()
    {
        while (canSpawn)
        {
            for (int i = 0; i < 10; i++) // �C���ͦ� 10 ��
            {
                CloneBubble();
                yield return new WaitForSeconds(spawnInterval); // �C 0.3 ��ͦ��@��
            }

            yield return new WaitForSeconds(3f); // �C���ͦ� 10 �ӫ�A���� 3 ��
        }
    }

    // �ͦ���Ө�l
    void CloneBubble()
    {
        Vector3 spawnPosition = GetRandomPosition() + Vector3.up * 10; // �ͦ��b�W��
        Instantiate(brushPrefab, spawnPosition, Quaternion.identity);
    }

    // �H���ͦ���m
    Vector3 GetRandomPosition()
    {
        return new Vector3(
            Random.Range(minSpawnPosition.position.x, maxSpawnPosition.position.x),
            Random.Range(minSpawnPosition.position.y, maxSpawnPosition.position.y),
            Random.Range(minSpawnPosition.position.z, maxSpawnPosition.position.z)
        );
    }
}