using TMPro;
using UnityEngine;

public class PlayerCollectable : MonoBehaviour
{
    public int currentDiamonds;
    public TMP_Text diamondText;

    private void Start()
    {
        currentDiamonds = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Diamond")
        {
            currentDiamonds++;
            diamondText.text = currentDiamonds.ToString();
            collision.gameObject.GetComponent<Animator>().SetTrigger("Collect");
            Destroy(collision.gameObject, .3f);
        }
    }
}
