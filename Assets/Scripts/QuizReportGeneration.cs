using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SimpleJSON;

public class QuizReportGeneration : MonoBehaviour
{
    
    public Text Section1Topic1;
    public Text Section2Topic1;
    public Text Section3Topic1;
    public Text Section1Topic2;
    public Text Section2Topic2;
    public Text Section3Topic2;
    public Text FeedbackText;
    public Button BackButton;

    public string generatereportAPIURL = "https://223.25.69.254:10002/generate_quiz_report";

    IEnumerator GenerateReport(string URL) {

        UnityWebRequest APIRequest = UnityWebRequest.Get(URL);
        APIRequest.certificateHandler = new WebRequestCert();   //force accept certificate

        yield return APIRequest.SendWebRequest();

        if (APIRequest.result == UnityWebRequest.Result.ConnectionError || APIRequest.result == UnityWebRequest.Result.ProtocolError){
            Debug.LogError(APIRequest.error);
            FeedbackText.text = "Server is down. Please try again later.";
            FeedbackText.gameObject.SetActive(true);
            yield break;
        }

        JSONNode APIinfo = JSON.Parse(APIRequest.downloadHandler.text);

        Section1Topic1.text = APIinfo["message"][0]["Performance"] + "\n Section " + APIinfo["message"][0]["Section"] + " Topic " + APIinfo["message"][0]["Topic"];
        Section2Topic1.text = APIinfo["message"][0]["Performance"] + "\n Section " + APIinfo["message"][0]["Section"] + " Topic " + APIinfo["message"][0]["Topic"];
        Section3Topic1.text = APIinfo["message"][0]["Performance"] + "\n Section " + APIinfo["message"][0]["Section"] + " Topic " + APIinfo["message"][0]["Topic"];
        Section1Topic2.text = APIinfo["message"][0]["Performance"] + "\n Section " + APIinfo["message"][0]["Section"] + " Topic " + APIinfo["message"][0]["Topic"];
        Section2Topic2.text = APIinfo["message"][0]["Performance"] + "\n Section " + APIinfo["message"][0]["Section"] + " Topic " + APIinfo["message"][0]["Topic"];
        Section3Topic2.text = APIinfo["message"][0]["Performance"] + "\n Section " + APIinfo["message"][0]["Section"] + " Topic " + APIinfo["message"][0]["Topic"];

    }

    
    void Start()
    {
        StartCoroutine(GenerateReport(generatereportAPIURL));

        BackButton.onClick.AddListener(() => {
            SceneManager.LoadScene("Teacher Menu", LoadSceneMode.Single);
        });
    }
}