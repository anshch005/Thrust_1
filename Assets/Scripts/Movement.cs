using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction Rotation;
    [SerializeField] float FlightForce;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            Debug.Log("wingardium leviosa");
        }
    }

    private void RotationalForce()
    {
        float rotationInput = Rotation.ReadValue<float>();
        Debug.Log("here is our rotation value: " + rotationInput);
    }


}