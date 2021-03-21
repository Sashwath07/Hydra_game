using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnSelectStoryMode(){
        SceneManager.LoadScene("World Select");
    }

    public void OnSelectLogOut(){
        SceneManager.LoadScene("Login Scene");
    }

    public void OnSelectLeaderboard(){
        SceneManager.LoadScene("Leaderboard");
    }
}