//This script is a template on how to perform API calls

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using TMPro;


public class APICall : MonoBehaviour
{
    public TMP_Text APIresult;

    private readonly string APIUrl = "https://223.25.69.254:10002/";

    public void SelectUpdate(){
        
        StartCoroutine(CallAPI());
    }

    IEnumerator CallAPI(){
        
        UnityWebRequest APIRequest = UnityWebRequest.Get(APIUrl);
        APIRequest.certificateHandler = new WebRequestCert();   //force accept certificate

        yield return APIRequest.SendWebRequest();

        if (APIRequest.result == UnityWebRequest.Result.ConnectionError || APIRequest.result == UnityWebRequest.Result.ProtocolError){
            Debug.LogError(APIRequest.error);
            yield break;
        }

        JSONNode APIinfo = JSON.Parse(APIRequest.downloadHandler.text);
        string APIinfoWorking = APIinfo["message"];

        APIresult.text = APIinfoWorking;
    }

    
}
