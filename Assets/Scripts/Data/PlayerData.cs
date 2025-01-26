using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : Singleton<PlayerData>
{
   public int score;
   public float moveSpeed;
   public void Init()
   {
       score = 0;
       EventTable.onPlayerScoreChange?.Invoke(score);

       SetMoveSpeed((int)PlayerState.NormalSpeed);
   }
   public void AddScore(int value)
   {

       score += value;
       if (score < 0)
       {
           SceneManager.LoadScene("Intro");
       }
       EventTable.onPlayerScoreChange?.Invoke(score);
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
