using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;

public class HIScoreUpdater : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Text;
    private int HiScore = 0;
    void OnGUI()
    {
        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "Score"))
        {
            HiScore = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "Score");
            Text.GetComponent<TextMeshProUGUI>().text = HiScore.ToString();
        }
    }
}
