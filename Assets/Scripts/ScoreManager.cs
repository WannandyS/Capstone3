using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public float currentScore;
    public float amount = .5f;
    public TMP_Text currentScoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPlayerAlive == false)
        {
            return;
        }
        currentScore += .5f * Time.deltaTime;
        currentScoreText.text = currentScore.ToString("0");
    }
}
