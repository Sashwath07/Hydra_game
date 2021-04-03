using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PVPMenu : MonoBehaviour
{
    public void OnSelectCreateRoom(){
        SceneManager.LoadScene("PVP Create Room");
    }

    public void OnSelectJoinRoom(){
        SceneManager.LoadScene("PVP Join Room");
    }

    public void OnSelectMainMenu(){
        SceneManager.LoadScene("Main Menu");
    }
}
