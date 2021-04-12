using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SimpleJSON;
using TMPro;

public class PVPEnded : MonoBehaviour
{
    public TMP_Text display;
    public Button mainMenu;

    private static string username = Login.username;
    private static string accessCode = PvpJoinRoom.accessCode;
    // public static string score = PlayerStats.GameScore.ToString();

    private static string endPvpBase = "https://223.25.69.254:10002/end_pvp/username=";
    private static string endPvpUrl = endPvpBase + username + "&access_code=" + accessCode + "&score=" + PlayerStats.GameScore.ToString();

    private static string getWinnerBase = "https://223.25.69.254:10002/pvp_winner/access_code=";
    private static string getWinnerUrl = getWinnerBase + accessCode;

    void Start(){
        mainMenu.gameObject.SetActive(false);
        StartCoroutine(EndPvp(endPvpUrl));
        StartCoroutine(GetWinner(getWinnerUrl));
    }

    IEnumerator EndPvp(string APIUrl){
        
        UnityWebRequest APIRequest = UnityWebRequest.Get(APIUrl);
        APIRequest.certificateHandler = new WebRequestCert();   //force accept certificate

        yield return APIRequest.SendWebRequest();

        if (APIRequest.result == UnityWebRequest.Result.ConnectionError || APIRequest.result == UnityWebRequest.Result.ProtocolError){
            Debug.LogError(APIRequest.error);
            yield break;
        }

        JSONNode APIinfo = JSON.Parse(APIRequest.downloadHandler.text);
        Debug.Log(APIinfo["message"]);  //end pvp message

        
    }

    IEnumerator GetWinner(string APIUrl){
        
        UnityWebRequest APIRequest = UnityWebRequest.Get(APIUrl);
        APIRequest.certificateHandler = new WebRequestCert();   //force accept certificate

        yield return APIRequest.SendWebRequest();

        if (APIRequest.result == UnityWebRequest.Result.ConnectionError || APIRequest.result == UnityWebRequest.Result.ProtocolError){
            Debug.LogError(APIRequest.error);
            yield break;
        }

        JSONNode APIinfo = JSON.Parse(APIRequest.downloadHandler.text);
        if (APIinfo["status_code"] == 200){
            Debug.Log("Winner determined");
            display.text = APIinfo["message"];
            mainMenu.gameObject.SetActive(true);
        } else{
            StartCoroutine(GetWinner(getWinnerUrl));
            // Debug.LogError("Error getting winner");
        }
    }

    public void OnSelectMainMenu(){
        Debug.Log("Main Menu selected");
        PlayerStats.GameScore = 0;
        QuizHandler.Score = 0;
        SceneManager.LoadScene("Main Menu");
    }
}
