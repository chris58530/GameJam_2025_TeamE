using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerBubbleBehaviour : MonoBehaviour
{
    public bool isTouchedOtherBubble = false;

    public OtherBubbleBehaviour[] otherBubbleBehaviour;

    public part unit;
    private void Start()
    {
        unit = GetComponent<part>();
        
    }
    void Update()
    {

    }

    void OnTouchingOtherBubble()
    {
        if(!isTouchedOtherBubble) return;
        if(!Input.GetKeyDown(KeyCode.K)) return;
        if(otherBubbleBehaviour.Length == 0) return;
          
        foreach(var otherBubble in otherBubbleBehaviour)
        { 
            otherBubble.CollectionPlayer(this,false, part.partType.Fusion);
        }
    }
   private void OnCollisionEnter(Collision other)
    {
       if(other.gameObject.TryGetComponent<OtherBubbleBehaviour>(out var otherBubble))
        {
           Debug.Log("isTouched" + other.gameObject.name);
           otherBubble.CollectionPlayer(this,true, part.partType.Rejection);
        }
    }
    

}
