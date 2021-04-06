using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text roundsText;
    public Text gameOverText;

    void OnEnable(){
        SetMessage();
        roundsText.text = PlayerStats.Rounds.ToString() + ":" +  PlayerStats.GameScore;
    }

    void SetMessage()
    {
        if (GameManager.LevelCompleted) {
            gameOverText.text = "Level Completed!";
        }
        else {
            gameOverText.text = "Game Over!";
        }
    }

    public void Retry(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu(){
        QuizHandler.Score = 0;
        SceneManager.LoadScene("Main Menu");
    }
}
