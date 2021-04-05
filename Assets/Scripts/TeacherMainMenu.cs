using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TeacherMainMenu : MonoBehaviour
{
        public Button QuizReportButton;
        public Button ReportGenerationButton;
        public Button LogoutButton;
    
    void Start()
    {
        QuizReportButton.onClick.AddListener(() => {
            SceneManager.LoadScene("Quiz Report Generation", LoadSceneMode.Single);
        });

        ReportGenerationButton.onClick.AddListener(() => {
            SceneManager.LoadScene("Report Generation", LoadSceneMode.Single);
        });

        LogoutButton.onClick.AddListener(() => {
            SceneManager.LoadScene("Login Scene", LoadSceneMode.Single);
        });
    }

}
