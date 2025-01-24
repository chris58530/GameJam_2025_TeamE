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
        switch (partType)
        {
            case part.partType.Fusion:
                if (isTouched)
                {
                    // 將當前物件設置為 player 的子物件
                    this.transform.parent = player.transform;
                    Debug.Log("Fusion: Merged with player.");
                }
                else
                {
                    // 移除父物件，並施加向外的衝量
                    this.transform.parent = null;
                    this.GetComponent<Rigidbody>().AddForce((this.transform.position - player.transform.position).normalized * 5f, ForceMode.Impulse);
                    Debug.Log("Fusion: Object rejected and pushed away.");
                }
                break;

            case part.partType.Discrete:
                this.transform.localScale *= 0.7f;

                // 創建一個新的較小物件
                GameObject smallerPart = Instantiate(this.gameObject, this.transform.position - Vector3.right * 0.5f, Quaternion.identity);

                // 設置較小部分的體積
                smallerPart.transform.localScale = this.transform.localScale * (3f / 7f); // 使總體積一致
                smallerPart.name = "SmallerPart";

                // 確保新物件有 Rigidbody 並施加力
                Rigidbody rb = smallerPart.GetComponent<Rigidbody>();
                if (rb == null) rb = smallerPart.AddComponent<Rigidbody>();
                rb.AddForce((this.transform.position - player.transform.position).normalized * 5f, ForceMode.Impulse);

                // 將較大的原物件分配給 player
                this.transform.parent = player.transform;
                // Discrete 類型執行分離，不改變層級結構
                Debug.Log("Discrete: Object remains independent.");
                break;

            case part.partType.Rejection:
                // Rejection 類型總是拒絕融合，並施加反方向力
                this.transform.parent = null;
                this.GetComponent<Rigidbody>().AddForce((this.transform.position - player.transform.position).normalized * 10f, ForceMode.Impulse);
                Debug.Log("Rejection: Object forcefully rejected.");
                break;

            default:
                Debug.LogWarning("Unknown partType behavior.");
                break;
        }
    }



}
