using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class MainMenu : MonoBehaviour
{
    public void LoadLevel()
    {
        AudioManager.Instance.Play("MainMenuClick");

        // Delay the actual scene load
        StartCoroutine(LoadLevelAfterDelay(0.25f));  // 0.5 second delay

    }

    private IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Level-1");
    }

}

