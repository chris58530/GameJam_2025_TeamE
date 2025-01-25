using System.Collections;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OtherBubbleBehaviour : MonoBehaviour
{
    public float touchTimeToDestroy = 5f; 
    public bool isTouched = false;
  
   //public void CollectionPlayer(PlayerBubbleBehaviour player,bool isTouched)//融合腳色
   // {
   //     if(isTouched)
   //     this.transform.parent = player.transform;//player傳入當前物件(父物件)
   //     else {
   //         //移除當前物件的父物件，讓其從層級結構中脫離，變為獨立的物件
   //         this.transform.parent = null;
   //         //給物件施加一個衝量力，力的方向是指向 player.transform.position
   //         this.GetComponent<Rigidbody>().AddForce(player.transform.position,ForceMode.Impulse);      
   //         }
   // }
    public void CollectionPlayer(PlayerBubbleBehaviour player, bool isTouched, part.partType partType)
    {
        currentTouchTime = touchToDestroyTime;
        switch (partType)
        {
            case part.partType.Fusion:
                if (isTouched)
                {
                    isTouchedPlayer = true;
                    // 將當前物件設置為 player 的子物件
                    this.transform.parent = player.transform;
                    StopCoroutine(RemoveRigidbody1());
                    Destroy(this.GetComponent<Rigidbody>());
                    Debug.Log("Fusion: Merged with player.");
                }
                else
                {
                    HandleRejection(player);
                    Debug.Log("Fusion: Object rejected and pushed away.");
                }
                break;

            case part.partType.Discrete:
                // 分裂物件邏輯
                this.transform.localScale *= 0.7f;
                GameObject smallerPart = Instantiate(this.gameObject, this.transform.position - Vector3.right * 0.5f, Quaternion.identity);
                smallerPart.transform.localScale = this.transform.localScale * (3f / 7f);

                Rigidbody rb = smallerPart.GetComponent<Rigidbody>();
                if (rb == null) rb = smallerPart.AddComponent<Rigidbody>();
                rb.AddForce((this.transform.position - player.transform.position).normalized * 5f, ForceMode.Impulse);
                rb.freezeRotation = true;
                rb.constraints = RigidbodyConstraints.FreezePositionY;
                rb.useGravity = false;

                StartCoroutine(RemoveRigidbody1(1));
                Debug.Log("Discrete: Split into two objects.");
                break;

            case part.partType.Rejection:
                // Rejection 類型總是拒絕融合，並施加反方向力
                HandleRejection(player);
                Debug.Log("Rejection: Forcefully rejected.");
                break;

            default:
                Debug.LogWarning("Unknown partType behavior.");
                break;
        }
    }
    private void Update()
    {
        if (isTouchedPlayer)
        {
            currentTouchTime -= Time.deltaTime;
            if (currentTouchTime <= 0)
            {
                Destroy(this.gameObject);
                //ActionTable.onDestroyOtherBubble?.Invoke(this);
            }
        }
    }

    private void HandleRejection(PlayerBubbleBehaviour player)
    {
        this.transform.parent = null;
        Rigidbody rigidbody = this.GetComponent<Rigidbody>() ?? this.gameObject.AddComponent<Rigidbody>();
        Vector3 direction = (this.transform.position - player.transform.position).normalized;
        rigidbody.AddForce(direction * 5, ForceMode.Impulse);
        rigidbody.freezeRotation = true;
        rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
        rigidbody.useGravity = false;
        StartCoroutine(RemoveRigidbody1(1));
    }
    IEnumerator RemoveRigidbody1(float timeToDestroy = 0)
    {
        yield return new WaitForSeconds(timeToDestroy);
        isTouchedPlayer = false;
        Destroy(this.GetComponent<Rigidbody>());
    }

}
