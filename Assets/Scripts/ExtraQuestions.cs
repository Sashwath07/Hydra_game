using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExtraQuestions : MonoBehaviour
{
    public static bool gameIsPaused = false;
    string currentScene;
    public Button extraQuestions;
    public Text questionCount;

    public void Update()
    {
        if (PlayerStats.questionCount != 0)
        {
            extraQuestions.interactable = WaveSpawner.isWaveDone;
            questionCount.text = "Extra Questions ("+ PlayerStats.questionCount.ToString() + ")";
        }
    }
    public void StartQuestions()
    {

        PlayerStats.questionCount--;
        Time.timeScale = 0;
        gameIsPaused = true;

        QuizHandler.numOfQns = 1;
        currentScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("scene", currentScene);

        foreach (GameObject g in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            g.SetActive(false);
        }

        SceneManager.LoadScene("Quiz", LoadSceneMode.Additive);
    }

}
