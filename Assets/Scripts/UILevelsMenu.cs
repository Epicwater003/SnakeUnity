using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILevelsMenu : MonoBehaviour
{
    public void GoToScene(string gameSceneName)
    {
        SceneManager.LoadScene(gameSceneName);
    }
}
