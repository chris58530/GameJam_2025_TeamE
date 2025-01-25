using System.Collections;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OtherBubbleBehaviour : MonoBehaviour
{
    public float touchToDestroyTime = 2f;
    private float currentTouchTime = 0;
    private bool isTouchedPlayer = false;

    public bool IsTouchedPlayer
    {
        get { return isTouchedPlayer; }
        set { isTouchedPlayer = value; }
    }
    void Start()
    {
        if (transform.position.y > 0)
        {
            StartCoroutine(MoveToZero());
        }
    }

    private IEnumerator MoveToZero()
    {
        while (transform.position.y > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 0, transform.position.z), 0.1f);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void CollisionPlayer(PlayerBubbleBehaviour player, bool isTouched)
    {
        currentTouchTime = touchToDestroyTime;
        if (isTouched)
        {
            isTouchedPlayer = true;
            this.transform.parent = player.transform;
            StopCoroutine(RemoveRigidbody());
            Destroy(this.GetComponent<Rigidbody>());
        }
        else
        {
            this.transform.parent = null;
            Rigidbody rigidbody = this.AddComponent<Rigidbody>();
            Vector3 direction = (this.transform.position - player.transform.position).normalized;
            rigidbody.AddForce(direction * 5, ForceMode.Impulse);
            rigidbody.freezeRotation = true;
            rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
            rigidbody.useGravity = false;
            StartCoroutine(RemoveRigidbody(1));
        }
    }
    public void OnCollisionProps()
    {
        Destroy(gameObject);
    }


    private void Update()
    {
        if (isTouchedPlayer)
        {
            currentTouchTime -= Time.deltaTime;
            if (currentTouchTime <= 0)
            {
                Destroy(this.gameObject);
                ActionTable.onDestroyOtherBubble?.Invoke(this);
            }
        }
    }

    IEnumerator RemoveRigidbody(float timeToDestroy = 0)
    {
        yield return new WaitForSeconds(timeToDestroy);
        isTouchedPlayer = false;
        Destroy(this.GetComponent<Rigidbody>());

    }




}
