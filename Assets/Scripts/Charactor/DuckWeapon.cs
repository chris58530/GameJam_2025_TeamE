using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckWeapon : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerBubbleBehaviour>(out var player))
        {
            player.OnCollisionWeapon();
            Debug.Log("Player hit the weapon");
        }
    }
}
