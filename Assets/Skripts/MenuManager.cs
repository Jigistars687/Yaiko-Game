using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("yiakoGAME");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

