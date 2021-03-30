using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TeacherMenu : MonoBehaviour
{
    public Button ReportGeneration;
    public Button LeaderboardButton;
    public Button LogoutButton;

    void Start()
    {
        ReportGeneration.onClick.AddListener(() => {
            SceneManager.LoadScene("Report Generation", LoadSceneMode.Single);
        });

        LeaderboardButton.onClick.AddListener(() => {
            SceneManager.LoadScene("Leaderboard", LoadSceneMode.Single);
        });

        LogoutButton.onClick.AddListener(() => {
            SceneManager.LoadScene("Login Scene", LoadSceneMode.Single);
        });
    }
}
