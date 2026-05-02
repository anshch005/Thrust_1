using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction Rotation;
    [SerializeField] float FlightForce = 750f;
    [SerializeField] float rotationStrength = 100f;
    [SerializeField] AudioClip MainEngineSFX;
    [SerializeField] ParticleSystem MainEngineParticle;
    [SerializeField] ParticleSystem RightEngineThrust;
    [SerializeField] ParticleSystem LeftEngineThrust;

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
                audioSource.PlayOneShot(MainEngineSFX); //PlayOneShot= plays a clip without interrupting current audio, allowing mull\ltiple sounds to overlap.
                //meanwhile AudioSource.Play() plays the assigned clip (replacing anything already playing)
            }
            if (!MainEngineParticle.isPlaying)
            {
                 MainEngineParticle.Play();
            }
        }
        else
        {
            audioSource.Stop();
            MainEngineParticle.Stop();
        }
    }

    private void RotationalForce()
    {
        float rotationInput = Rotation.ReadValue<float>();
        if(rotationInput < 0)
        {
            ApplyRotation(rotationStrength);
             if (!RightEngineThrust.isPlaying)
            {
                LeftEngineThrust.Stop();
                 RightEngineThrust.Play();
            }
        }
        else if(rotationInput > 0)
        {
            ApplyRotation(-rotationStrength);
            if (!LeftEngineThrust.isPlaying)
            {
                RightEngineThrust.Stop();
                LeftEngineThrust.Play();
            }
        }
        else
        {
            RightEngineThrust.Stop();
            LeftEngineThrust.Stop();
        }

    }

     private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}