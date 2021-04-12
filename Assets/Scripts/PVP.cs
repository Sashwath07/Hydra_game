using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PVP : MonoBehaviour
{
    public static int WorldSelected = 0;
    public static int SectionSelected = 0;
    public static int LevelSelected = 0;

    public static bool isPvp;

    public Button CreateGame;

    void Start(){
        CreateGame.gameObject.SetActive(false);
    }

    void Update(){
        if (WorldSelected!=0 && SectionSelected!=0 && LevelSelected!=0){
            CreateGame.gameObject.SetActive(true);
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
        SceneManager.LoadScene("PVP Access Code");
        Debug.Log("Create game selected");
    }
}
