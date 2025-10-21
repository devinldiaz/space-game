using UnityEditor.Callbacks;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float dropTime = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > dropTime)
        {
            rb.useGravity = true;
        }
    }
}
