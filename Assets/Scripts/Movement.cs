using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    // create variable
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotate;
    [SerializeField] float thrustStrength = 1000f;
    [SerializeField] float rotationStrength = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;

    Rigidbody rb;
    AudioSource audioSource;

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
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * Time.fixedDeltaTime * thrustStrength);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainBooster.Stop();
    }

    private void ProcessRotation()
    {
        float rotationInput = rotate.ReadValue<float>();

        //transform.Rotate(-Vector3.forward * rotationInput * rotationStrength * Time.fixedDeltaTime);

        if (rotationInput < 0)
        {
            RotateLeft();
        }
        else if (rotationInput > 0)
        {
            RotateRight();
        }
        else
        {
            leftBooster.Stop();
            rightBooster.Stop();
        }
    }
    private void RotateLeft()
    {
        ApplyRotation(rotationStrength);
        if (!leftBooster.isPlaying)
        {
            leftBooster.Play();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationStrength);
        if (!rightBooster.isPlaying)
        {
            rightBooster.Play();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
