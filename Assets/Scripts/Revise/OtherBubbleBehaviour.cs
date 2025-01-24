using System.Collections;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OtherBubbleBehaviour : MonoBehaviour
{
    public float touchTimeToDestroy = 5f; 
    public bool isTouched = false;
  
   //public void CollectionPlayer(PlayerBubbleBehaviour player,bool isTouched)//�ĦX�}��
   // {
   //     if(isTouched)
   //     this.transform.parent = player.transform;//player�ǤJ��e����(������)
   //     else {
   //         //������e���󪺤�����A����q�h�ŵ��c�������A�ܬ��W�ߪ�����
   //         this.transform.parent = null;
   //         //������I�[�@�ӽĶq�O�A�O����V�O���V player.transform.position
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
                    // �N��e����]�m�� player ���l����
                    this.transform.parent = player.transform;
                    Debug.Log("Fusion: Merged with player.");
                }
                else
                {
                    // ����������A�ìI�[�V�~���Ķq
                    this.transform.parent = null;
                    this.GetComponent<Rigidbody>().AddForce((this.transform.position - player.transform.position).normalized * 5f, ForceMode.Impulse);
                    Debug.Log("Fusion: Object rejected and pushed away.");
                }
                break;

            case part.partType.Discrete:
                this.transform.localScale *= 0.7f;

                // �Ыؤ@�ӷs�����p����
                GameObject smallerPart = Instantiate(this.gameObject, this.transform.position - Vector3.right * 0.5f, Quaternion.identity);

                // �]�m���p��������n
                smallerPart.transform.localScale = this.transform.localScale * (3f / 7f); // ���`��n�@�P
                smallerPart.name = "SmallerPart";

                // �T�O�s���� Rigidbody �ìI�[�O
                Rigidbody rb = smallerPart.GetComponent<Rigidbody>();
                if (rb == null) rb = smallerPart.AddComponent<Rigidbody>();
                rb.AddForce((this.transform.position - player.transform.position).normalized * 5f, ForceMode.Impulse);

                // �N���j���쪫����t�� player
                this.transform.parent = player.transform;
                // Discrete ������������A�����ܼh�ŵ��c
                Debug.Log("Discrete: Object remains independent.");
                break;

            case part.partType.Rejection:
                // Rejection �����`�O�ڵ��ĦX�A�ìI�[�Ϥ�V�O
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
