using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class UIPause : MonoBehaviour
{
    public GameObject _pausePanel;
    public GameObject SnakeHead;
    public GameObject ResumeButton;
    private bool gameEnd = false;
    private int score = 0;
    public event Action<bool> Paused;
    private void Start()
    {
        ResumeGame();
        SnakeHead.GetComponent<SnakeController>().GameEnd += StopGame;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameEnd)
        {
            _pausePanel.SetActive(!_pausePanel.activeSelf);
            Paused?.Invoke(_pausePanel.activeSelf);
            if (_pausePanel.activeSelf)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }
    public void StopGame(bool a)
    {
        gameEnd = true;
        _pausePanel.SetActive(true);
        ResumeButton.SetActive(false);
        PauseGame();
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ResumeGame();
    }
    public void ExitToMenu(string menuSceneName)
    {
        SceneManager.LoadSceneAsync(menuSceneName);
    }
    public void PauseGame()
    {
       
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        Paused?.Invoke(false);
        _pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
