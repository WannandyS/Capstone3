using UnityEngine;

public class Backgrounds : MonoBehaviour
{
    public GameObject mainCamera;
    public float parallexEffect;
    private float startPos;
    private float length;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position.x;
        length = this.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // beri efek parallex
    void Update()
    {
        float distance = mainCamera.transform.position.x * parallexEffect;
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        float temp = mainCamera.transform.position.x * (1 - parallexEffect);
        if (temp >= startPos + length)
        {
            startPos += length;
        }
    }
}
