using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public int min = 1;
    public int max = 101;
    public TextMeshPro textMeshPro;

    void Start()
    {
        int randomNumber = Random.Range(min, max);

        textMeshPro.text = randomNumber.ToString();
        Destroy(this.gameObject, 1f);
    }
}
