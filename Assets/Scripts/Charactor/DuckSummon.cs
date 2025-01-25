using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckSummon : MonoBehaviour
{
    [SerializeField] private GameObject duckPrefab;
[SerializeField]private float summonTime = 5f;
    void Start()
    {
        StartCoroutine(SummonDuck());
    }
    IEnumerator SummonDuck()
    {

        yield return new WaitForSeconds(summonTime);
        Instantiate(duckPrefab, transform.position+ Vector3.up * 10, Quaternion.identity);
    }
}
