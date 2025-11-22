using UnityEngine;

public class ExitButton : MonoBehaviour
{
    [SerializeField] GameObject _canvas;
    [SerializeField] GameObject _records_canvas;
    public void ExitGame()
    {
        Application.Quit(); // Закрываем игру
    }

    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
    }
    public void Records_open()
    {
        _canvas.SetActive(false);
        _records_canvas.SetActive(true);
    }

    public void Records_close()
    {
        _records_canvas.SetActive(false);
        _canvas.SetActive(true);
    }
}
