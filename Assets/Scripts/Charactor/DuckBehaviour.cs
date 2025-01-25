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

}

public class DuckBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 2.0f;

    [SerializeField] public GameObject preHyperBeam;
    [SerializeField] private GameObject hyperBeam;

    DuckAttackState currentState = DuckAttackState.Idle;

   public Transform player;
    bool attacking = false;
    void Start(){
        player = GameObject.FindGameObjectWithTag("Player").transform;
        AudioManager.current.PlayBattleBGM();
    }


    // Update is called once er frame
    void Update()
    {
          if (transform.position.y > 0)
        {
            StartCoroutine(MoveToZero());
        }
        if (attacking) return;
        AttackState(currentState);

    }
     private IEnumerator MoveToZero()
    {
        while (transform.position.y > 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 0, transform.position.z), 0.25f);
            yield return new WaitForSeconds(0.01f);
        }
    }


    public void SwithState(DuckAttackState state)
    {
        currentState = state;
    }
    public void AttackState(DuckAttackState state)
    {
        if (player == null) return;

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
                case DuckAttackState.SinkAttack:
                    Debug.Log("SinkAttack");
                StartCoroutine(HyperBeam());
                    // StartCoroutine(SinkAttack());
                    break;
        }
    }
    IEnumerator Idle()
    {
        attacking = true;
        yield return new WaitForSeconds(.5f);
        
        int random = Random.Range(0, DuckAttackState.GetValues(typeof(DuckAttackState)).Length);
        Debug.Log(random);
        SwithState((DuckAttackState)random);
        attacking = false;
        yield return null;
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
            direction.y = 0;
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
        attacking = false;

        yield return null;
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
            direction.y = 0;

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, elapsedTime / 3f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Deactivate preHyperBeam
        preHyperBeam.SetActive(false);
        yield return new WaitForSeconds(1);

        // Activate hyperBeam immediately
        hyperBeam.SetActive(true);

        // Keep hyperBeam active for 1 second
        yield return new WaitForSeconds(1);

        // Deactivate hyperBeam
        hyperBeam.SetActive(false);

        // Switch back to Idle state
        SwithState(DuckAttackState.Idle);
        attacking = false;

        yield return null;

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

        attacking = false;
        yield return null;
    }


}
