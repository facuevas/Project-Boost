using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    Vector3 m_StartingPos;
    [SerializeField] Vector3 m_MovementVector;
    [SerializeField] [Range(0, 1)] float m_MovementFactor;
    [SerializeField] float m_Period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        m_StartingPos = transform.position;
        Debug.Log(m_StartingPos);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Period <= Mathf.Epsilon) return;

        float cycles = Time.time / m_Period; // continually grows over time.

        const float tau = Mathf.PI * 2; // constant value of 6.283
        float rawSineWave = Mathf.Sin(cycles * tau); // goes from -1 to 1

        m_MovementFactor = (rawSineWave + 1f) / 2; // transform the wave to go to 0 to 1

        Vector3 offset = m_MovementVector * m_MovementFactor;
        transform.position = m_StartingPos + offset;
    }
}
