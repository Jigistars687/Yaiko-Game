using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(SceneNames.MainMenuScene);
    }
    public void LoadFirstPart()
    {
        SceneManager.LoadScene(SceneNames.FirstTreeLVLS);

    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
