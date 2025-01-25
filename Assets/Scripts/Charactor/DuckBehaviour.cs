using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public enum DuckAttackState
{
    Idle,
    Rush,
    HyperBeam,
    SinkAttack,

    Dead
}

public class DuckBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 2.0f;

    [SerializeField] public GameObject preHyperBeam;
    [SerializeField] private GameObject hyperBeam;

    DuckAttackState currentState = DuckAttackState.Idle;

    [SerializeField] private Transform player;
    bool attacking = false;


    // Update is called once er frame
    void Update()
    {
        if(attacking)return;
        AttackState(currentState);
    }

    public void SwithState(DuckAttackState state)
    {
        currentState = state;
    }
    public void AttackState(DuckAttackState state)
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        switch (state)
        {
            case DuckAttackState.Idle:
                Debug.Log("Idle");
                StartCoroutine(Idle());
                break;
            case DuckAttackState.Rush:
                Debug.Log("Rush");
                StartCoroutine(Rush());
                break;
            case DuckAttackState.HyperBeam:
                StartCoroutine(HyperBeam());
                Debug.Log("HyperBeam");
                break;
            // case DuckAttackState.SinkAttack:
            //     Debug.Log("SinkAttack");
            //     StartCoroutine(SinkAttack());
            //     break;
        }
    }
    IEnumerator Idle()
    {
        attacking = true;
        int random = Random.Range(0, DuckAttackState.GetValues(typeof(DuckAttackState)).Length);
        SwithState((DuckAttackState)random);
        yield return new WaitForSeconds(1);
        attacking = false;
    }
    IEnumerator Rush()
    {
        attacking = true;
        // Rotate to face the player over 3 seconds
        float elapsedTime = 0f;
        while (elapsedTime < 3f)
        {
            // Assuming you have a reference to the player
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, elapsedTime / 3f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Pause for 1 second
        yield return new WaitForSeconds(1);

        // Rush forward
        Vector3 rushDirection = transform.forward;
        float rushSpeed = 10f; // Adjust the rush speed as needed
        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            transform.position += rushDirection * rushSpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        SwithState(DuckAttackState.Idle);

        yield return null;
        attacking = false;
    }

    IEnumerator HyperBeam()
    {
        attacking = true;
        // Activate preHyperBeam and face the player for 3 seconds
        preHyperBeam.SetActive(true);
        float elapsedTime = 0f;
        while (elapsedTime < 3f)
        {
            // Assuming you have a reference to the player
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, elapsedTime / 3f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Deactivate preHyperBeam
        preHyperBeam.SetActive(false);

        // Activate hyperBeam immediately
        hyperBeam.SetActive(true);

        // Keep hyperBeam active for 1 second
        yield return new WaitForSeconds(1);

        // Deactivate hyperBeam
        hyperBeam.SetActive(false);

        // Switch back to Idle state
        SwithState(DuckAttackState.Idle);

        yield return null;
        attacking = false;

    }

    IEnumerator SinkAttack()
    {
        attacking = true;
        // Sink into the ground for 3 seconds
        float elapsedTime = 0f;
        while (elapsedTime < 3f)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 0, transform.position.z), 0.25f);
            yield return new WaitForSeconds(0.01f);
        }

        // Pause for 1 second
        yield return new WaitForSeconds(1);

        // Rise from the ground for 3 seconds
        elapsedTime = 0f;
        while (elapsedTime < 3f)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 1, transform.position.z), 0.25f);
            yield return new WaitForSeconds(0.01f);
        }

        // Switch back to Idle state
        SwithState(DuckAttackState.Idle);

        yield return null;
        attacking = false;
    }


}
