using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Class Properties
    [SerializeField]
    private float m_ThrustSpeed = 1250f;
    [SerializeField]
    private float m_RotationSpeed = 1f;
    [SerializeField]
    private AudioClip m_MainEngine;
    [SerializeField]
    private ParticleSystem m_MainThrusterParticle;
    [SerializeField]
    private ParticleSystem m_LeftThrusterParticle;
    [SerializeField]
    private ParticleSystem m_RightThrusterParticle;

    // Components
    private Rigidbody m_RigidBody;
    private Transform m_Transform;
    private AudioSource m_AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_Transform = GetComponent<Transform>();
        m_AudioSource = GetComponent<AudioSource>();
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
        {
            ApplyThrust();
        }
        else
        {
            StopThrust();
        }
    }

    private void StopThrust()
    {
        m_MainThrusterParticle.Stop();
        m_AudioSource.Stop();
    }

    private void ApplyThrust()
    {
        m_RigidBody.AddRelativeForce(Vector3.up * m_ThrustSpeed * Time.deltaTime);

        if (!m_AudioSource.isPlaying)
            m_AudioSource.PlayOneShot(m_MainEngine);

        if (m_MainThrusterParticle.isStopped)
            m_MainThrusterParticle.Play();
    }

    private void ProcessRotationInput()
    {
        // Turn left
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        // Turn right
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotate();
        }
    }

    private void StopRotate()
    {
        m_RightThrusterParticle.Stop();
        m_LeftThrusterParticle.Stop();
    }

    private void RotateRight()
    {
        if (m_RightThrusterParticle.isStopped)
            m_RightThrusterParticle.Play();

        ApplyRotation(-1f);
    }

    private void RotateLeft()
    {
        if (m_LeftThrusterParticle.isStopped)
            m_LeftThrusterParticle.Play();

        ApplyRotation(1f);
    }

    private void ApplyRotation(float direction)
    {
        // How much rotation to apply to turn our rocket
        Vector3 rotationToApply = Vector3.forward * m_RotationSpeed * Time.deltaTime;

        m_RigidBody.freezeRotation = true; // Freeze the rotation so we can manually rotate.
        m_Transform.Rotate(direction * rotationToApply); // Apply the rotation.
        m_RigidBody.constraints =
            RigidbodyConstraints.FreezeRotationX | // freezing rotation on the X
            RigidbodyConstraints.FreezeRotationY | // freezing rotation on the Y
            RigidbodyConstraints.FreezePositionZ; // freezing position on the Z
    }
}
