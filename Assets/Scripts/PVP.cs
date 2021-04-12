using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PVP : MonoBehaviour
{
    public static int WorldSelected = 0;
    public static int SectionSelected = 0;
    public static int LevelSelected = 0;

    public static bool isPvp;

    public TMP_Dropdown WorldDropdown;
    public TMP_Dropdown SectionDropdown;

    public Button CreateGame;

    public TMP_Dropdown WorldDropdown;
    public TMP_Dropdown SectionDropdown;

    void Start(){
        CreateGame.gameObject.SetActive(false);
    }

    void Update(){
        if (WorldSelected!=0 && SectionSelected!=0 && LevelSelected!=0){
            CreateGame.gameObject.SetActive(true);
        }

        if (WorldSelected == 1){
            TMP_Dropdown.OptionData section1 = new TMP_Dropdown.OptionData("Requirement Elicitation Techniques");
            SectionDropdown.options.RemoveAt(1);
            SectionDropdown.options.Insert(1, section1);

            TMP_Dropdown.OptionData section2 = new TMP_Dropdown.OptionData("Software Engineering basics");
            SectionDropdown.options.RemoveAt(2);
            SectionDropdown.options.Insert(2, section2);

            TMP_Dropdown.OptionData section3 = new TMP_Dropdown.OptionData("Requirement Engineering");
            SectionDropdown.options.RemoveAt(3);
            SectionDropdown.options.Insert(3, section3);
        }

        if (WorldSelected == 2){
            TMP_Dropdown.OptionData section1 = new TMP_Dropdown.OptionData("UML basics");
            SectionDropdown.options.RemoveAt(1);
            SectionDropdown.options.Insert(1, section1);

            TMP_Dropdown.OptionData section2 = new TMP_Dropdown.OptionData("UML Diagrams 1");
            SectionDropdown.options.RemoveAt(2);
            SectionDropdown.options.Insert(2, section2);

            TMP_Dropdown.OptionData section3 = new TMP_Dropdown.OptionData("UML Diagrams 2");
            SectionDropdown.options.RemoveAt(3);
            SectionDropdown.options.Insert(3, section3);
        }
    }

    public void SelectWorld(int val){
        if (val == 1){
            WorldSelected = 1;
            Debug.Log("World " + WorldSelected.ToString() + " selected");
        }
        if (val == 2){
            WorldSelected  = 2;    
            Debug.Log("World " + WorldSelected.ToString() + " selected");
        }
    }

    public void SelectSection(int val){
        if (val == 1){
            SectionSelected = 1;
            Debug.Log("Section " + SectionSelected.ToString() + " selected");
        }
        if (val == 2){
            SectionSelected = 2;   
            Debug.Log("Section " + SectionSelected.ToString() + " selected");        
        }
        if (val == 3){
            SectionSelected = 3;
            Debug.Log("Section " + SectionSelected.ToString() + " selected");           
        }
    }

    public void SelectLevel(int val){
        if (val == 1){
            LevelSelected = 1;
            Debug.Log("Level " + LevelSelected.ToString() + " selected");
        }
        if (val == 2){
            LevelSelected = 2;      
            Debug.Log("Level " + LevelSelected.ToString() + " selected");  
        }
        if (val == 3){
            LevelSelected = 3; 
            Debug.Log("Level " + LevelSelected.ToString() + " selected");      
        }
    }

    public void OnSelectCreateGame(){
        Debug.Log("Create game selected");
        SceneManager.LoadScene("PVP Access Code");
        Debug.Log("Create game selected");
    }

    public void OnSelectMainMenu(){
        Debug.Log("Main Menu selected");
        CreateGame.gameObject.SetActive(false);
        SceneManager.LoadScene("Main Menu");
    }
}
