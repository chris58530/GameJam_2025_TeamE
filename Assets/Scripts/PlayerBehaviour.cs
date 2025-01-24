using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
   void OnColliderEnter(Collider other)
    {
       if(other.gameObject.TryGetComponent<OtherBubbleBehaviour>(out var otherBubbleBehaviour))
        {
            Debug.Log("isTouched" + other.gameObject.name);

            otherBubbleBehaviour.isTouched = true;
        }
    }
    void OnColliderExit(Collider other)
    {
        if(other.gameObject.TryGetComponent<OtherBubbleBehaviour>(out var otherBubbleBehaviour))
        {
            otherBubbleBehaviour.isTouched = false;
        }
    }
}
