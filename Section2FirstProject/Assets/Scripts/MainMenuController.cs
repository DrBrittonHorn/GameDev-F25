using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
            return;
        }
        Application.Quit();
    }
}
