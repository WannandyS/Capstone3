using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isPlayerAlive;
    public GameObject panel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPlayerAlive = true;

        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(this);
        }

        panel.transform.localPosition = new Vector3(0, -1500f, 0);
    }

    public void TriggerPanel()
    {
        FindAnyObjectByType<ScoreManager>().SetHighScore();
        panel.LeanMoveLocalY(0, .8f).setEaseOutExpo();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
