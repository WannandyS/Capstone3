using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] groundPrefab;
    public Vector2 spawnPosition;

    void Start()
    {
        for (int i = 0; i < 3;  i++)
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        int groundIndex = Random.Range(0, groundPrefab.Length);
        GameObject tempGround = Instantiate(groundPrefab[groundIndex], spawnPosition, Quaternion.identity);
        float nextX = tempGround.transform.GetChild(1).position.x;
        spawnPosition = new Vector2(nextX, spawnPosition.y);
    }
}
