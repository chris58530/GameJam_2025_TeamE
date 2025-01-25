using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerBubbleBehaviour : MonoBehaviour
{
    public bool isTouchedOtherBubble = false;

    public List<OtherBubbleBehaviour> otherBubbleList = new List<OtherBubbleBehaviour>();
    void OnEnable()
    {
        ActionTable.onDestroyOtherBubble += KillBubble;
    }
    void OnDisable()
    {
        ActionTable.onDestroyOtherBubble -= KillBubble;
    }
    void Update()
    {
        OnTouchingOtherBubble();
    }

    void OnTouchingOtherBubble()
    {
        if (!Input.GetKeyDown(KeyCode.K)) return;
        if (otherBubbleList.Count == 0) return;

        var bubblesToRemove = new List<OtherBubbleBehaviour>();
        foreach (var otherBubble in otherBubbleList)
        {
            bubblesToRemove.Add(otherBubble);
            otherBubble.CollisionPlayer(this, false);
        }
        foreach (var bubble in bubblesToRemove)
        {
            otherBubbleList.Remove(bubble);
        }
    }
    void KillBubble(OtherBubbleBehaviour otherBubble)
    {
        otherBubbleList.Remove(otherBubble);
        PlayerData.Instance.score += 1;
    }
    public void OnCollisionProps()
    {
       // Destroy(gameObject);
       Debug.Log("player collision to die");
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<OtherBubbleBehaviour>(out var otherBubble))
        {
            if (otherBubble.IsTouchedPlayer) return;

            otherBubble.CollisionPlayer(this, true);
            Debug.Log("isTouched" + other.gameObject.name);

            if (otherBubbleList.Contains(otherBubble)) return;
            otherBubbleList.Add(otherBubble);
        }
    }


}
