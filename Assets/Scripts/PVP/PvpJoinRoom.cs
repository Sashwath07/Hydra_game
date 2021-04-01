using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;
using TMPro;

public class PvpJoinRoom : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text enterAccessCode;
    public Button Enter;

    private static string accessCode;
    private static string username = Login.username;
    private static string baseUrl = "https://223.25.69.254:10002/enter_pvp_room/username=";
    // private static string Url = "https://223.25.69.254:10002/enter_pvp_room/username=<username>&access_code=<access_code>";
    private string APIUrl = baseUrl + username + "&access_code=" + accessCode;

    IEnumerator CallAPI(){
        
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
            //retrieve world section level info
            //load selected world section level
        }
    }
}
