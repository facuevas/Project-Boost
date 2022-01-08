using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCheats : MonoBehaviour
{
    public bool m_CheatsEnabled = false;
    public bool m_IsCollisionOn = false;

    private CollisionHandler collisionHandler;

    // Start is called before the first frame update
    void Start()
    {
        collisionHandler = GetComponent<CollisionHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_CheatsEnabled) return;
        StartNextLevel();
        ToggleCollision();
    }

    private void ToggleCollision()
    {
        if (Input.GetKeyUp(KeyCode.C))
            m_IsCollisionOn = !m_IsCollisionOn;
    }

    private void StartNextLevel()
    {
        if (Input.GetKeyUp(KeyCode.L))
            collisionHandler.LoadNextLevelSequence();
    }
}
