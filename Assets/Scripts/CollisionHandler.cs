using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]
    private float m_loadLevelDelay = 1.5f;

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

    private void StartCrashSequence()
    {
        //TODO: Add SFX upon crash.
        //TODO: Add particle effect upon crash.
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", m_loadLevelDelay);
    }

    private void ReloadLevel()
    {
        int currentActiveSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentActiveSceneIndex);
    }
    private void LoadNextLevelSequence()
    {
        //TODO: Add SFX upon crash.
        //TODO: Add particle effect upon crash.
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", m_loadLevelDelay);
    }

    private void LoadNextLevel()
    {
        int currentActiveSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentActiveSceneIndex + 1) >= SceneManager.sceneCountInBuildSettings ? 0 : (currentActiveSceneIndex + 1);
        SceneManager.LoadScene(nextSceneIndex);
    }
}
