using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckWeapon : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (TryGetComponent<PlayerBubbleBehaviour>(out var player))
        {
            player.OnCollisionWeapon();
        }


    }
}
