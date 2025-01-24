using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Singleton<PlayerData>
{
   public int score;
   public int moveSpeed;
   public void Init()
   {
       score = 0;
       moveSpeed = 0;
   }
   public void AddScore(int value)
   {
       score += value;
   }
   public void AddMoveSpeed(int value)
   {
       moveSpeed += value;
   }

}
