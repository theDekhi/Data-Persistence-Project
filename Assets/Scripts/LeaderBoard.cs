using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    private Text highScoresText;
    void Start()
    {
        highScoresText = GameObject.Find("Leaderboard").GetComponent<Text>();
        highScoresText.text = $"High Score : {GameManager.bestPlayerName} : {GameManager.bestScore}";
    }
    public void OnBackButtonPressed()
    {
        SceneManager.LoadScene(0);
    }
}
