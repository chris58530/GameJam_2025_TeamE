using System.Collections;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OtherBubbleBehaviour : MonoBehaviour
{
    public float touchTimeToDestroy = 5f; 
    public bool isTouched = false;
  
   public void CollectionPlayer(PlayerBubbleBehaviour player,bool isTouched)
    {
        if(isTouched)
        this.transform.parent = player.transform;
        else {
            this.transform.parent = null;
            this.GetComponent<Rigidbody>().AddForce(player.transform.position,ForceMode.Impulse);      
            }
    }
    


   
}
