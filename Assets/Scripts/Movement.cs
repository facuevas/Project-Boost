using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Components
    private Rigidbody rb;
    private Transform t;

    // Class Properties
    [SerializeField]
    private float thrustSpeed = 1250f;
    [SerializeField]
    private float rotationSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        t = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        ProcessThrustInput();
        ProcessRotationInput();
    }

    private void ProcessThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
            rb.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
    }

    private void ProcessRotationInput()
    {
        // Turn left
        if (Input.GetKey(KeyCode.A))
            ApplyRotation(1f);
        // Turn right
        else if (Input.GetKey(KeyCode.D))
            ApplyRotation(-1f);
    }

    private void ApplyRotation(float direction)
    {
        // How much rotation to apply to turn our rocket
        Vector3 rotationToApply = Vector3.forward * rotationSpeed * Time.deltaTime;

        rb.freezeRotation = true; // Freeze the rotation so we can manually rotate.
        t.Rotate(direction * rotationToApply); // Apply the rotation.
        rb.constraints =
            RigidbodyConstraints.FreezeRotationX | // freezing rotation on the X
            RigidbodyConstraints.FreezeRotationY | // freezing rotation on the Y
            RigidbodyConstraints.FreezePositionZ; // freezing position on the Z
    }
}
