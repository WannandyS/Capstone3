using UnityEngine;

public class Follow : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * player.moveSpeed);
    }
}
