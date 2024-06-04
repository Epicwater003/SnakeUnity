using UnityEngine;
using UnityEngine.SceneManagement;
public class UIMainMenu : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    public void StartGame(string gameSceneName)
    {
        SceneManager.LoadScene(gameSceneName);
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit");
    }
}