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
            Debug.Log("World 1");
            WorldSelected = 1;
        }
        if (val == 2){
            Debug.Log("World 2");       
            WorldSelected  = 2;    
        }
    }

    public void SelectSection(int val){
        if (val == 1){
            Debug.Log("Section 1");
            SectionSelected = 1;
        }
        if (val == 2){
            Debug.Log("Section 2");
            SectionSelected = 2;           
        }
        if (val == 3){
            Debug.Log("Section 3");
            SectionSelected = 3;           
        }
    }

    public void SelectLevel(int val){
        if (val == 1){
            Debug.Log("Level 1");
            LevelSelected = 1;
        }
        if (val == 2){
            Debug.Log("Level 2");   
            LevelSelected = 2;        
        }
        if (val == 3){
            Debug.Log("Level 3");    
            LevelSelected = 3;       
        }
    }

    public void OnSelectCreateGame(){
        SceneManager.LoadScene("PVP Access Code");
    }
}
