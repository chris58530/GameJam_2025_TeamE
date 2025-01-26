using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckSummon : MonoBehaviour
{
    [SerializeField] private GameObject duckPrefab;
    [SerializeField] private float summonTime = 5f;
    public void Summon()
    {
        StartCoroutine(SummonDuck());
    }
    IEnumerator SummonDuck()
    {

        yield return new WaitForSeconds(summonTime);
        GameObject hpBG = GameObject.Find("HpBG");
        if (hpBG != null)
        {
            Animator animator = hpBG.GetComponent<Animator>();
            if (animator != null)
            {
                animator.Play("DuckHpBarIntro");
            }
        }
        Instantiate(duckPrefab, transform.position + Vector3.up * 10, Quaternion.identity);
    }
}
