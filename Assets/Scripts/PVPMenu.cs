using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PVPMenu : MonoBehaviour
{
    public void OnSelectCreateRoom(){
        Debug.Log("Create room selected");
        SceneManager.LoadScene("PVP Create Room");
    }

    public void OnSelectJoinRoom(){
        Debug.Log("Join room selected");
        SceneManager.LoadScene("PVP Join Room");
    }

    public void OnSelectMainMenu(){
        Debug.Log("Main Menu selected");
        SceneManager.LoadScene("Main Menu");
    }
}
