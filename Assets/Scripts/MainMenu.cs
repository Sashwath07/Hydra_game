using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void SelectStoryMode(){
        SceneManager.LoadScene("World Select");
    }

    public void LogOut(){
        SceneManager.LoadScene("Login Scene");
    }
}