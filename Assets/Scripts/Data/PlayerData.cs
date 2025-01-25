using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Singleton<PlayerData>
{
   public int score;
   public float moveSpeed;
   public void Init()
   {
       score = 0;
       SetMoveSpeed((int)PlayerState.NormalSpeed);
   }
   public void AddScore(int value)
   {
       score += value;
   }
   public void SetMoveSpeed(float value)
   {
       moveSpeed = value;
   }
   public enum PlayerState
   {
      NormalSpeed = 5,
      TouchedBubbleSpeed = 2
   }

}
