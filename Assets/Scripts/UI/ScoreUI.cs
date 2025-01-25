using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ScoreUI : MonoBehaviour
{

    [SerializeField] private TMP_Text scoreText;

    [SerializeField]private Image duckHealthImage;
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
