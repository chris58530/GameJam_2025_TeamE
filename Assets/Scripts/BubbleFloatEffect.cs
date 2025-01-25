using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BubbleFloatEffect : MonoBehaviour
{
    float floatStrength;
    void Start()
    {
        InvokeRepeating("RandomFloat", 0, 10);
    }
    void RandomFloat()
    {
        floatStrength = Random.Range(0, 2);
    }
    void Update()
    {

        transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Sin(Time.time) * floatStrength, transform.localPosition.z);
    }

    void OnDisable()
    {
        CancelInvoke();
    }
}
