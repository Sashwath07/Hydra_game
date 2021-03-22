using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SectionSelect : MonoBehaviour
{
    public static int sectionSelected = 0;

    public static string SectionToLoad(){
        return "Section " + sectionSelected.ToString();
    }

    public void OnSelectSection1(){
        sectionSelected = 1;
        SceneManager.LoadScene("Level Select");
    }

    public void OnSelectSection2(){
        sectionSelected = 2;
        SceneManager.LoadScene("Level Select");
    }

    public void OnSelectSection3(){
        sectionSelected = 3;
        SceneManager.LoadScene("Level Select");
    }        

    public void OnSelectMainMenu(){

        SceneManager.LoadScene("Main Menu");
    }
}
