using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public void OnCollisionProps()
    {
        Debug.Log("Wall OnCollisionProps");
    }
    void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.TryGetComponent<DuckBehaviour>(out var duck))
       {
          duck.SwithState(DuckAttackState.GoMiddle);
       }
    }
}
