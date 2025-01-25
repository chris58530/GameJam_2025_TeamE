using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleFloatEffect : MonoBehaviour
{

    void Update()
    {
        float floatStrength = 0.5f; 
        transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Sin(Time.time) * floatStrength, transform.localPosition.z);
    }
}
