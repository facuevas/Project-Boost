using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // Class properties
    [SerializeField]
    private float m_LoadLevelDelay = 1.5f;
    [SerializeField]
    private AudioClip m_SuccessAudio;
    [SerializeField]
    private AudioClip m_FailureAudio;
    [SerializeField]
    private ParticleSystem m_SuccessParticle;
    [SerializeField]
    private ParticleSystem m_FailureParticle;

    // Components
    private AudioSource m_AudioSource;
    private DebugCheats m_DebugCheats;
    private Transform m_Transform;
    bool m_IsTransitioning = false;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_DebugCheats = GetComponent<DebugCheats>();
        m_Transform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (m_Transform.position.y < 0 && !m_IsTransitioning)
        {
            // They died, fell off the platform.
            StartCrashSequence();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // If we are transitioning, ignore all collisions.
        if (m_IsTransitioning || (m_DebugCheats.m_CheatsEnabled && !m_DebugCheats.m_IsCollisionOn)) return;

        var otherTag = other.gameObject.tag;
        switch (otherTag)
        {
            case "Friendly":
                break;
            case "Finished":
                LoadNextLevelSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    // This method is called when the level is transition.
    // The parameter takes a boolean if the player succeeds or fails.
    private void StartTransition(bool success)
    {
        m_IsTransitioning = true;
        m_AudioSource.Stop();
        m_AudioSource.PlayOneShot(success ? m_SuccessAudio : m_FailureAudio);

        GetComponent<Movement>().enabled = false;
    }

    // This method is called when the Player crashes. It restarts the level.
    private void StartCrashSequence()
    {
        m_FailureParticle.Play();
        StartTransition(false);
        Invoke("ReloadLevel", m_LoadLevelDelay);
    }

    private void ReloadLevel()
    {
        int currentActiveSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentActiveSceneIndex);
    }

    // This method is called when the Player succeeds.
    public void LoadNextLevelSequence()
    {
        m_SuccessParticle.Play();
        StartTransition(true);
        Invoke("LoadNextLevel", m_LoadLevelDelay);
    }

    private void LoadNextLevel()
    {
        int currentActiveSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentActiveSceneIndex + 1) >= SceneManager.sceneCountInBuildSettings ? 0 : (currentActiveSceneIndex + 1);
        SceneManager.LoadScene(nextSceneIndex);
    }
}
