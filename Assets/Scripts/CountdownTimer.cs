using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class CountdownTimer : MonoBehaviour
{
    private string sceneToLoad;

    public void startGame(){
        sceneToLoad = WorldSelect.WorldToLoad();
        SceneManager.LoadScene(sceneToLoad);
    }
}
