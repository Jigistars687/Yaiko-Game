using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField] private int _currentLevel = 0; 
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);            
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void LoadNextScene()
    {
        _currentLevel++;
        SceneManager.LoadScene(_currentLevel);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(_currentLevel);
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
