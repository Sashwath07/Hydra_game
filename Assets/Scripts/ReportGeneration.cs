using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReportGeneration : MonoBehaviour
{
    
    public Button BackButton;
    
    void Start()
    {
        BackButton.onClick.AddListener(() => {
            SceneManager.LoadScene("Teacher Menu", LoadSceneMode.Single);
        });
        
    }
}
