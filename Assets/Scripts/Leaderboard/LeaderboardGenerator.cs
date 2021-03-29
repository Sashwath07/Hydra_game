using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using SimpleJSON;
using TMPro;
using System.IO;

public class LeaderboardGenerator : MonoBehaviour
{
    public TMP_Text firstPlace;
    public TMP_Text secondPlace;
    public TMP_Text thirdPlace;
    public TMP_Text fourthPlace;
    public TMP_Text fifthPlace;
    public TMP_Text sixthPlace;
    public TMP_Text seventhPlace;
    public TMP_Text eightPlace;
    public TMP_Text ninthPlace;
    public TMP_Text tenthPlace;
    public TMP_Text PlayerPosition;

    // private static string playerName = Login.username;
    private static string playerName = "SHAFIQ002";
    private static string baseUrl = "https://223.25.69.254:10002/get_leaderboard/username=";
    private string Url = baseUrl + playerName;

    void Start()
    {
        StartCoroutine(GenerateLeaderboard());
    }

    IEnumerator GenerateLeaderboard(){
        int playerIndex = 0;
        UnityWebRequest APIRequest = UnityWebRequest.Get(Url);
        APIRequest.certificateHandler = new WebRequestCert();   //force accept certificate

        yield return APIRequest.SendWebRequest();

        if (APIRequest.isNetworkError || APIRequest.isHttpError){
            Debug.LogError(APIRequest.error);
            yield break;
        }

        JSONNode file = JSON.Parse(APIRequest.downloadHandler.text);
        for (int i = 0; i < file["message"].Count; i++)
        {
            if ((string)file["message"][i]["Username"] == playerName){

                playerIndex = i;
                break;
            }
        }

        firstPlace.text     = "1. " + (string)file["message"][0]["Username"] + " " + (string)file["message"][0]["Points"];
        secondPlace.text    = "2. " + (string)file["message"][1]["Username"] + " " + (string)file["message"][1]["Points"];
        thirdPlace.text     = "3. " + (string)file["message"][2]["Username"] + " " + (string)file["message"][2]["Points"];
        fourthPlace.text    = "4. " + (string)file["message"][3]["Username"] + " " + (string)file["message"][3]["Points"];
        fifthPlace.text     = "5. " + (string)file["message"][4]["Username"] + " " + (string)file["message"][4]["Points"];
        sixthPlace.text     = "6. " + (string)file["message"][5]["Username"] + " " + (string)file["message"][5]["Points"];
        seventhPlace.text   = "7. " + (string)file["message"][6]["Username"] + " " + (string)file["message"][6]["Points"];
        eightPlace.text     = "8. " + (string)file["message"][7]["Username"] + " " + (string)file["message"][7]["Points"];
        ninthPlace.text     = "9. " + (string)file["message"][8]["Username"] + " " + (string)file["message"][8]["Points"];
        tenthPlace.text     = "10. " + (string)file["message"][9]["Username"] + " " + (string)file["message"][9]["Points"];
        PlayerPosition.text = (string)file["message"][playerIndex]["Position"] + ". "+ (string)file["message"][playerIndex]["Username"] + " " + (string)file["message"][playerIndex]["Points"];

    }

    public void OnSelectMainMenu(){
        SceneManager.LoadScene("Main Menu");
    }

}
