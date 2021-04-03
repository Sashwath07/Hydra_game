using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TeacherMainMenu : MonoBehaviour
{
        public Button ReportGenerationButton;
        // public Button LeaderboardButton;
        public Button LogoutButton;
    
    void Start()
    {
        ReportGenerationButton.onClick.AddListener(() => {
            SceneManager.LoadScene("Report Generation", LoadSceneMode.Single);
        });

        // LeaderboardButton.onClick.AddListener(() => {
        //     SceneManager.LoadScene("Leaderboard", LoadSceneMode.Single);
        // });

        LogoutButton.onClick.AddListener(() => {
            SceneManager.LoadScene("Login Scene", LoadSceneMode.Single);
        });
    }

}
