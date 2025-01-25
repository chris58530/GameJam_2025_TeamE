using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{

    [SerializeField] private TMP_Text scoreText;
    void OnEnable()
    {
        EventTable.onPlayerScoreChange += UpdateScore;
    }
    void OnDisable()
    {
        EventTable.onPlayerScoreChange -= UpdateScore;
    }

    void UpdateScore(int score)
    {

        scoreText.text = score.ToString();
    }
}
