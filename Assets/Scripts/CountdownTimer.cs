using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class CountdownTimer : MonoBehaviour
{
    public void startGame(){
        SceneManager.LoadScene("Main Scene");
    }
}
