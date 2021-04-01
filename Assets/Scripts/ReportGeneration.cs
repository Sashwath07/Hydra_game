using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SimpleJSON;

public class ReportGeneration : MonoBehaviour
{
    public Text Topic1Section1;
    public Button BackButton;

    public string generatereportAPIURL = "https://223.25.69.254:10002/generate_quiz_report";

    IEnumerator GenerateReport(string URL) {

        UnityWebRequest APIRequest = UnityWebRequest.Get(URL);
        APIRequest.certificateHandler = new WebRequestCert();   //force accept certificate

        yield return APIRequest.SendWebRequest();

        if (APIRequest.isNetworkError || APIRequest.isHttpError){
            Debug.LogError(APIRequest.error);
            yield break;
        }

        JSONNode APIinfo = JSON.Parse(APIRequest.downloadHandler.text);

        Topic1Section1.text = APIinfo["message"][0]["Performance"] + "\n" + APIinfo["message"][0]["Section"] + " " + APIinfo["message"][0]["Topic"];
    }

    
    void Start()
    {
        StartCoroutine(GenerateReport(generatereportAPIURL));

        BackButton.onClick.AddListener(() => {
            SceneManager.LoadScene("Teacher Menu", LoadSceneMode.Single);
        });
        
    }
}
