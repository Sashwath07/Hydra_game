using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;
using TMPro;

public class PvpJoinRoom : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text enterAccessCode;

    public static string accessCode;
    private static string username = Login.username;   
    private static string baseUrl = "https://223.25.69.254:10002/enter_pvp_room/username=";
    // private static string Url = "https://223.25.69.254:10002/enter_pvp_room/username=<username>&access_code=<access_code>";
    public void OnJoinRoom(){
        Debug.Log("Join room selected");
        accessCode = inputField.text;
        StartCoroutine(CallAPI());
    }

    IEnumerator CallAPI(){
        string APIUrl = baseUrl + username + "&access_code=" + accessCode;

        UnityWebRequest APIRequest = UnityWebRequest.Get(APIUrl);
        APIRequest.certificateHandler = new WebRequestCert();   //force accept certificate

        yield return APIRequest.SendWebRequest();

        if (APIRequest.result == UnityWebRequest.Result.ConnectionError || APIRequest.result == UnityWebRequest.Result.ProtocolError){
            Debug.LogError(APIRequest.error);
            yield break;
        }

        JSONNode APIinfo = JSON.Parse(APIRequest.downloadHandler.text);
        
        if (APIinfo["status_code"] == 400){
            Debug.Log("Access code invalid");
            enterAccessCode.text = "Invalid code, please try again";
        }
        if (APIinfo["status_code"] == 200){
            Debug.Log("Access code valid, joining room");
            enterAccessCode.text = "Joining room...";
            WorldSelect.worldSelected = APIinfo["message"]["Phase"];
            SectionSelect.sectionSelected = APIinfo["message"]["Section"];
            LevelSelect.levelSelected = APIinfo["message"]["Level"];
            PVP.isPvp = true;
            SceneManager.LoadScene("Quiz");

        }
    }
}
