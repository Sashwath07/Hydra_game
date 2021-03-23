using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public static int levelSelected = 0;

    public static string LevelToLoad(){
        return "Section " + levelSelected.ToString();
    }

    public void OnSelectSection1(){
        levelSelected = 1;
        SceneManager.LoadScene("Quiz");
    }

    public void OnSelectSection2(){
        levelSelected = 2;
        SceneManager.LoadScene("Quiz");
    }

    public void OnSelectSection3(){
        levelSelected = 3;
        SceneManager.LoadScene("Quiz");
    }

    public void OnSelectMainMenu(){

        SceneManager.LoadScene("Main Menu");
    }
}
