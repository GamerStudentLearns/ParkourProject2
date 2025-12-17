using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [Tooltip("Name of the scene to load after delay")]
    public string sceneName;

    [Tooltip("Name of the scene where the mouse should unlock")]
    public string unlockMouseScene;

    [Tooltip("Time in seconds before switching")]
    public float delay = 5f;

    private void Start()
    {
        StartCoroutine(SwitchSceneAfterDelay());
    }

    private System.Collections.IEnumerator SwitchSceneAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        // Load the target scene
        SceneManager.LoadScene(sceneName);

        // If the loaded scene matches the unlockMouseScene, unlock the cursor
        if (sceneName == unlockMouseScene)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
