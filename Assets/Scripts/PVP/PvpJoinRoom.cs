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
    public Button Enter;

    private static string accessCode;
    private static string username = Login.username.ToUpper();     //use this later
    // private static string username = "SHAFIQ002";
    private static string baseUrl = "https://223.25.69.254:10002/enter_pvp_room/username=";
    // private static string Url = "https://223.25.69.254:10002/enter_pvp_room/username=<username>&access_code=<access_code>";
    public void OnJoinRoom(){
        accessCode = inputField.text;
        StartCoroutine(CallAPI());
    }

    IEnumerator CallAPI(){
        string APIUrl = baseUrl + username + "&access_code=" + accessCode;

        UnityWebRequest APIRequest = UnityWebRequest.Get(APIUrl);
        APIRequest.certificateHandler = new WebRequestCert();   //force accept certificate

        yield return APIRequest.SendWebRequest();

        if (APIRequest.isNetworkError || APIRequest.isHttpError){
            Debug.LogError(APIRequest.error);
            yield break;
        }

        JSONNode APIinfo = JSON.Parse(APIRequest.downloadHandler.text);
        
        if (APIinfo["status_code"] == 400){
            enterAccessCode.text = "Invalid code, please try again";
        }
        if (APIinfo["status_code"] == 200){
            enterAccessCode.text = "Joining room...";
            // WorldSelect.worldSelected = APIinfo["message"]["World"];
            if (APIinfo["message"]["Phase"] == "Topic 1"){
                WorldSelect.worldSelected = 1;
            }
            if (APIinfo["message"]["Phase"] == "Topic 2"){
                WorldSelect.worldSelected = 2;
            }

            // SectionSelect.sectionSelected = APIinfo["message"]["Section"];
            if (APIinfo["message"]["Section"] == "Section 1"){
                SectionSelect.sectionSelected = 1;
            }
            if (APIinfo["message"]["Section"] == "Section 2"){
                SectionSelect.sectionSelected = 2;
            }
            if (APIinfo["message"]["Section"] == "Section 3"){
                SectionSelect.sectionSelected = 3;
            }
            LevelSelect.levelSelected = APIinfo["message"]["Level"];
            SceneManager.LoadScene("Quiz");

        }
    }
}
