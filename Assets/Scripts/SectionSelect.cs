using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SectionSelect : MonoBehaviour
{
    public static int sectionSelected = 0;

    public TMP_Text section1;
    public TMP_Text section2;
    public TMP_Text section3;
    private int worldSelected = WorldSelect.worldSelected;

    void Start(){
        if (worldSelected == 1){
            section1.text = "Software Engineering basics";
            section2.text = "Requirement Engineering";
            section3.text = "Requirement Elicitation Techniques";
        }
        if (worldSelected == 2){
            section1.text = "UML basics";
            section2.text = "UML Diagrams 1";
            section3.text = "UML Diagrams 2";
        }
    }

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
