using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isPlayerAlive;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
