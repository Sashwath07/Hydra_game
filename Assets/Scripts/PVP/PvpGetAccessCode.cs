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

    private static string accessCode;
    private string APIUrl = "https://223.25.69.254:10002/create_pvp_room";

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
        accessCode = APIinfo["message"];
        displayAccessCode.text = "Access code: " + accessCode;
        enterAccessCode.gameObject.SetActive(true);
        
    }
}
