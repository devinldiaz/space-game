using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    // create variable
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotate;
    AudioSource audioSource;
    [SerializeField] float thrustStrength = 1000f;
    [SerializeField] float rotationStrength = 100f;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        thrust.Enable();
        rotate.Enable();
    }

    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * Time.fixedDeltaTime * thrustStrength);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void ProcessRotation()
    {
        float rotationInput = rotate.ReadValue<float>();

        //transform.Rotate(-Vector3.forward * rotationInput * rotationStrength * Time.fixedDeltaTime);

        if (rotationInput < 0)
        {
            ApplyRotation(rotationStrength);
        }
        else if (rotationInput > 0)
        {
            ApplyRotation(-rotationStrength);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
