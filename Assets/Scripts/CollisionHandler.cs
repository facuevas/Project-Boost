using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // Class properties
    [SerializeField]
    private float m_LoadLevelDelay = 1.5f;
    [SerializeField]
    private AudioClip m_Success;
    [SerializeField]
    private AudioClip m_Failure;

    // Components
    private AudioSource m_AudioSource;
    bool isTransitioning = false;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        var otherTag = other.gameObject.tag;
        switch (otherTag)
        {
            case "Friendly":
                Debug.Log("Collided with a Friendly object");
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
        if (!isTransitioning)
        {
            m_AudioSource.Stop();
            m_AudioSource.PlayOneShot(success ? m_Success : m_Failure);
            isTransitioning = true;
            GetComponent<Movement>().enabled = false;
        }
    }

    // This method is called when the Player crashes. It restarts the level.
    private void StartCrashSequence()
    {
        //TODO: Add SFX upon crash.
        //TODO: Add particle effect upon crash.
        StartTransition(false);
        Invoke("ReloadLevel", m_LoadLevelDelay);
    }

    private void ReloadLevel()
    {
        int currentActiveSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentActiveSceneIndex);
    }

    // This method is called when the Player succeeds.
    private void LoadNextLevelSequence()
    {
        //TODO: Add SFX upon crash.
        //TODO: Add particle effect upon crash.
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
