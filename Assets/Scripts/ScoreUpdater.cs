using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUpdater : MonoBehaviour
{
    public GameObject SnakeHead;
    public GameObject Text;
    private int score;
    void Start()
    {
        SnakeHead.GetComponent<SnakeController>().ScoreIncreased += SetCounter;
    }

    // Update is called once per frame
    void OnGUI()
    {
        Text.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }
    void SetCounter(int n)
    {
        score = n;
    }
}
