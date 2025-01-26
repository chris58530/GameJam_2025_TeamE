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
    [SerializeField]private Animator duckHealthAnimator;
    void OnEnable()
    {
        EventTable.onPlayerScoreChange += UpdateScore;
        EventTable.onDuckHealthChange += UpdateDuckHealth;
    }
    void OnDisable()
    {
        EventTable.onPlayerScoreChange -= UpdateScore;
        EventTable.onDuckHealthChange -= UpdateDuckHealth;
    }
    void UpdateDuckHealth(int health)
    {
        duckHealthImage.fillAmount = health / 100f;
    }
    void UpdateScore(int score)
    {

        scoreText.text = score.ToString();
    }
    public void PlayDuckHealthAnimation()
    {
        duckHealthAnimator.Play("DuckHpBarIntro");
    }
}
