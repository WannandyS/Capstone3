using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();

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

    public void Shake()
    {
        animator.SetTrigger("Shake");
    }
}
