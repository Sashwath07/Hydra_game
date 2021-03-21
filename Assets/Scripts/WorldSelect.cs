using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSelect : MonoBehaviour
{
    public static int worldSelected = 0;

    public static string WorldToLoad(){
        return "World " + worldSelected.ToString();
    }
    public void SelectWorld1(){
        worldSelected = 1;
        SceneManager.LoadScene("Game Loading");
    }

    public void SelectWorld2(){
        worldSelected = 2;
        SceneManager.LoadScene("Game Loading");
    }

    public void SelectWorld3(){
        worldSelected = 3;
        SceneManager.LoadScene("Game Loading");
    }

    public void SelectWorld4(){
        worldSelected = 4;
        SceneManager.LoadScene("Game Loading");
    }

    public void SelectWorld5(){
        worldSelected = 5;
        SceneManager.LoadScene("Game Loading");
    }

    public void OnSelectMainMenu(){

        SceneManager.LoadScene("Main Menu");
    }
}
