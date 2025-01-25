using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerBubbleBehaviour : MonoBehaviour
{
    public bool isTouchedOtherBubble = false;

    public List<OtherBubbleBehaviour> otherBubbleList = new List<OtherBubbleBehaviour>();

    bool isCarryOtherBubble = false;
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
        if (otherBubbleList.Count == 0) return;

        PlayerData.Instance.SetMoveSpeed((int)PlayerData.PlayerState.TouchedBubbleSpeed);

        if (!Input.GetKeyDown(KeyCode.K)) return;

        var bubblesToRemove = new List<OtherBubbleBehaviour>();
        foreach (var otherBubble in otherBubbleList)
        {
            bubblesToRemove.Add(otherBubble);

            otherBubble.CollisionPlayer(this, false);

            PlayerData.Instance.SetMoveSpeed((int)PlayerData.PlayerState.NormalSpeed);

        }
        foreach (var bubble in bubblesToRemove)
        {
            otherBubbleList.Remove(bubble);
        }
    }
    void KillBubble(OtherBubbleBehaviour otherBubble)
    {
        foreach (var bubble in otherBubbleList)
        {
            bubble.CollisionPlayer(this, false);
        }
        otherBubbleList.Clear();
        PlayerData.Instance.SetMoveSpeed((int)PlayerData.PlayerState.NormalSpeed);

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
            if (otherBubbleList.Contains(otherBubble)) return;
            otherBubbleList.Add(otherBubble);
            otherBubble.CollisionPlayer(this, true);

        }
    }


}
