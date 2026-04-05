using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    
    [SerializeField] float FlightForce;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        thrust.Enable();
    }

    private void FixedUpdate() {
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up* FlightForce* Time.fixedDeltaTime);
            Debug.Log("wingardium leviosa");
        }
    }
    }
}