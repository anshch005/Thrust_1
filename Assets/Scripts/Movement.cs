using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction Rotation;
    [SerializeField] float FlightForce = 750f;
    [SerializeField] float rotationStrength = 100f;

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
        Rotation.Enable();
    }

    private void FixedUpdate() {
        {
            UpwardForce();
            RotationalForce();
        }
    }

    private void UpwardForce()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * FlightForce * Time.fixedDeltaTime);
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
        Debug.Log("wingardium leviosa");
    }

    private void RotationalForce()
    {
        float rotationInput = Rotation.ReadValue<float>();
        Debug.Log("here is our rotation value: " + rotationInput);
        if(rotationInput < 0)
        {
            ApplyRotation(rotationStrength);
        }
        else if(rotationInput > 0)
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