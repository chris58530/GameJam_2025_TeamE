using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerBubbleBehaviour : MonoBehaviour
{
    public bool isTouchedOtherBubble = false;

    //public OtherBubbleBehaviour[] otherBubbleBehaviour;
    public List<OtherBubbleBehaviour> otherBubbleList = new List<OtherBubbleBehaviour>();

    void OnEnable()
    {
        ActionTable.onDestroyOtherBubble += KillBubble;
    }
    void OnDisable()
    {
        ActionTable.onDestroyOtherBubble -= KillBubble;
    }
    private void Start()
    {
        
    }
    void Update()
    {
        OnTouchingOtherBubble();
    }

    void OnTouchingOtherBubble()
    {
        if(!Input.GetKeyDown(KeyCode.K)) return;
        if(otherBubbleBehaviour.Count == 0) return;
        var bubblesToRemove = new List<OtherBubbleBehaviour>();
        foreach (var otherBubble in otherBubbleList)
        {
            bubblesToRemove.Add(otherBubble);
            otherBubble.CollectionPlayer(this,false, part.partType.Fusion);
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
    private void OnCollisionEnter(Collision other)
    {
       if(other.gameObject.TryGetComponent<OtherBubbleBehaviour>(out var otherBubble))
        {
           if (otherBubble.IsTouchedPlayer) return;

           Debug.Log("isTouched" + other.gameObject.name);
           otherBubble.CollectionPlayer(this,true, part.partType.Rejection);

           if (otherBubbleList.Contains(otherBubble)) return;
           otherBubbleList.Add(otherBubble);
        }
    }
    

}
