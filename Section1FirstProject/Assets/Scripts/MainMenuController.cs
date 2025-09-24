using UnityEditor;
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
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
