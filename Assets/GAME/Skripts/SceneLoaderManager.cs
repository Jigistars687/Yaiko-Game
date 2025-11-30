//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour
{
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneNames.MainMenuScene);
    }
    public void LoadFirstPart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneNames.FirstTreeLVLS);

    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
