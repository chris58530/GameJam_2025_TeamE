using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DuckBehaviour : MonoBehaviour, ISinkable
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float shakeHead = 1.0f;


    public bool isReset = false;
    private float trueDuration = 3f;  // 為 true 的時間
    private float falseDuration = 7f; // 為 false 的時間

    void Start()
    {
        StartCoroutine(ToggleBooleanLoop());
    }

    private IEnumerator ToggleBooleanLoop()
    {
        while (true) // 無限循環
        {
            // 設為 true，並等待 trueDuration 的時間
            isReset = true;
            yield return new WaitForSeconds(trueDuration);

            // 設為 false，並等待 falseDuration 的時間
            isReset = false;
            yield return new WaitForSeconds(falseDuration);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isReset)
        {
            MoveToPlayer();
        }
    }
    void MoveToPlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * speed);
        }
        Vector3 direction = (player.transform.position - transform.position).normalized;
        direction.y += Mathf.Sin(Time.time) * shakeHead;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public void OnCollisionProps()
    {
        Destroy(gameObject);
    }

}
