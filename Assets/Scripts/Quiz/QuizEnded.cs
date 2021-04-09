//Manage the quiz end screen and send quiz score to database

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;

public class QuizEnded : MonoBehaviour
{
    public TMP_Text Score;
    public Button Proceed;

    private static int QuizScore = QuizHandler.Score;

    private static int worldSelected = WorldSelect.worldSelected;
    private static int sectionSelected = SectionSelect.sectionSelected;
    private static int levelSelected = LevelSelect.levelSelected;
    // private static string username = Login.username;     //use this when log in page is set up
    private static string username = "SHAFIQ002";
    private static string baseUrl = "https://223.25.69.254:10002/update_performance/username=";
    private string Url = baseUrl + username + "&world=" + worldSelected.ToString() + "&section=" + sectionSelected.ToString() + "&no_of_correct=" + QuizScore.ToString();

    void Start(){
        Score.text = "Your Score: " + QuizHandler.Score.ToString();
        Proceed.interactable = false;
        StartCoroutine(UpdateQuizScore(Url));
        Proceed.interactable = true;
    }

    IEnumerator UpdateQuizScore(string APIUrl){
        UnityWebRequest APIRequest = UnityWebRequest.Get(APIUrl);
        APIRequest.certificateHandler = new WebRequestCert();   //force accept certificate

        yield return APIRequest.SendWebRequest();

        if (APIRequest.result == UnityWebRequest.Result.ConnectionError || APIRequest.result == UnityWebRequest.Result.ProtocolError){
            Debug.LogError(APIRequest.error);
            yield break;
        }
    }  

    public void OnSelectProceed(){
        SceneManager.LoadScene("World " + worldSelected + "-" + levelSelected);
    }
}
