using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public float currentScore;
    public float amount = 1f;
    public TMP_Text currentScoreText;
    public TMP_Text highScoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentScore = 0;
        highScoreText.text = PlayerPrefs.GetFloat("Highscore", 0f).ToString("0");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            DeleteSaveData();
        }

        if (GameManager.instance.isPlayerAlive == false)
        {
            return;
        }
        currentScore += 1f * Time.deltaTime;
        currentScoreText.text = currentScore.ToString("0");
    }

    public void SetHighScore()
    {
        if (currentScore > PlayerPrefs.GetFloat("Highscore"))
        {
            PlayerPrefs.SetFloat("Highscore", currentScore);
            highScoreText.text = currentScore.ToString("0");
        }
    }

    void DeleteSaveData()
    {
        PlayerPrefs.DeleteKey("Highscore");
        highScoreText.text = "0";
    }
}
