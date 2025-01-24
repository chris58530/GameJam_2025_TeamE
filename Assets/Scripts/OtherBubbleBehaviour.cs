using System.Collections;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OtherBubbleBehaviour : MonoBehaviour
{
    public float touchTimeToDestroy = 5f; 
    public bool isTouched = false;
  
   
    


    void Update()
    {
        if (isTouched)
        {
            touchTimeToDestroy -= Time.deltaTime;
            if (touchTimeToDestroy <= 0)
            {
                Destroy(gameObject);
                ActionTable.onDestroyOtherBubble?.Invoke(1);
            }
        }else isTouched = false;
    }
}
