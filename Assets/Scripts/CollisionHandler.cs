using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        var otherTag = other.gameObject.tag;
        switch (otherTag)
        {
            case "Fuel":
                Debug.Log("Collided with a Fuel object");
                break;
            case "Friendly":
                Debug.Log("Collided with a Friendly object");
                break;
            case "Finished":
                LoadNextLevel();
                break;
            default:
                ReloadLevel();
                break;
        }
    }

    private void ReloadLevel()
    {
        int currentActiveSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentActiveSceneIndex);
    }

    private void LoadNextLevel()
    {
        int currentActiveSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentActiveSceneIndex + 1) >= SceneManager.sceneCountInBuildSettings ? 0 : (currentActiveSceneIndex + 1);
        SceneManager.LoadScene(nextSceneIndex);
    }
}
