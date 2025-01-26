using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : Singleton<PlayerData>
{
    public int score;
    public float moveSpeed;
    bool isWon = false;
    public void Init()
    {
        score = 0;
        EventTable.onPlayerScoreChange?.Invoke(score);

        SetMoveSpeed((int)PlayerState.NormalSpeed);
    }
    public void AddScore(int value)
    {
        if (isWon) return;
        score += value;

        if (score >= 3)
        {
            EventTable.onPlayerWin?.Invoke();
            isWon = true;
            return;
        }
        if (score < 0)
        {
            SceneManager.LoadScene("Lose");
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
