using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using SimpleJSON;

public class PvpGetAccessCode : MonoBehaviour
{
    public TMP_Text displayAccessCode;
    public TMP_Text enterAccessCode;

    private static string WorldSelected = PVP.WorldSelected.ToString();
    private static string SectionSelected = PVP.SectionSelected.ToString();
    private static string LevelSelected = PVP.LevelSelected.ToString();

    // private string Url = "https://223.25.69.254:10002/create_pvp_room";
    private static string baseUrl = "https://223.25.69.254:10002/create_pvp_room/world=";
    string url = "https://223.25.69.254:10002/create_pvp_room/world=1&section=1&level=1";
    private static string APIUrl = baseUrl + WorldSelected + "&section=" + SectionSelected + "&level=" + LevelSelected;
    void Start(){
        StartCoroutine(CallAPI());
    }

    IEnumerator CallAPI(){
        
        UnityWebRequest APIRequest = UnityWebRequest.Get(APIUrl);
        APIRequest.certificateHandler = new WebRequestCert();   //force accept certificate

        yield return APIRequest.SendWebRequest();

        if (APIRequest.isNetworkError || APIRequest.isHttpError){
            Debug.LogError(APIRequest.error);
            yield break;
        }

        JSONNode APIinfo = JSON.Parse(APIRequest.downloadHandler.text);
        if (APIinfo["status_code"] == 200){
            string accessCode = APIinfo["message"];
            displayAccessCode.text = "Access code: " + accessCode;
            enterAccessCode.gameObject.SetActive(true);
        }
        Debug.Log(APIUrl);
        Debug.Log(APIinfo["message"]);

        
    }
}
