using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("yiakoGAME"); // Загружаем игровую сцену
    }

    public void ExitGame()
    {
        Application.Quit(); // Закрываем игру
    }
}

