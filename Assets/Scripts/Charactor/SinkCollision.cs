using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkCollision : MonoBehaviour
{
   void OnCollisionEnter(Collision collision)
   {
       if(collision.gameObject.TryGetComponent<ISinkable>(out var sinkable))
       {
           sinkable.OnCollisionProps();
       }
   }
   void OnTriggerEnter(Collider other)
   {
       if(other.gameObject.TryGetComponent<ISinkable>(out var sinkable))
       {
           sinkable.OnCollisionProps();
       }
   } 
}
