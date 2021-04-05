using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;
using SimpleJSON;

public class AssignmentReportGeneration : MonoBehaviour
{
    public List<AssignmentReportClass> ACList = new List<AssignmentReportClass>();
    public Button BackButton;
    public TMP_Dropdown AssignmentAccessCodes;
    public Text StartDateText;
    public Text EndDateText;
    public Text AvgScoreText;
    public Text StandardDText;
    public string AvailACUrl = "https://223.25.69.254:10002/generate_assignment_report";

    
    void Start()
    {
        StartCoroutine(CheckAvailAccessCodes());
        BackButton.onClick.AddListener(() => {
            SceneManager.LoadScene("Teacher Menu", LoadSceneMode.Single);
        });
        
    }

    IEnumerator CheckAvailAccessCodes(){
        UnityWebRequest request = UnityWebRequest.Get(AvailACUrl);
        request.certificateHandler = new WebRequestCert();   //force accept certificate
        yield return request.SendWebRequest();

        JSONNode APIinfo = JSON.Parse(request.downloadHandler.text);
        int APIstatus = APIinfo["status_code"];
        Debug.Log(APIstatus);

        // fills up ACList with all current exisiting access codes 
        if(APIstatus == 200){
            for(int i =0; i<APIinfo["message"].Count; i++){
                AssignmentReportClass tempAC = new AssignmentReportClass();
                tempAC.AccessCode = APIinfo["message"][i]["AccessCode"];
                tempAC.AverageScore = APIinfo["message"][i]["AverageScore"];
                tempAC.StandardDeviation = APIinfo["message"][i]["StandardDeviation"];
                tempAC.StartTime = APIinfo["message"][i]["StartTime"];
                tempAC.EndTime = APIinfo["message"][i]["EndTime"];

                ACList.Add(tempAC);
            }
        }
        else{
            // Pop up no assignmnets created in database
        }
        FillDropdown();
    }

    // Fills dropdown with the latest 5 access codes
    public void FillDropdown(){
        int j =0;
        foreach(AssignmentReportClass ac in ACList){
            // create another condition to account for ACList less than 5 to prevent index errors
            if (j==5){break;}
            AssignmentAccessCodes.options.Add(new TMP_Dropdown.OptionData(){text=ac.AccessCode.ToString()});
            j++;
        }
        AssignmentAccessCodes.RefreshShownValue();

        // initialise first(latest) assignment report
        StartDateText.text = ACList[0].StartTime;
        EndDateText.text = ACList[0].EndTime;
        AvgScoreText.text = ACList[0].AverageScore.ToString();
        StandardDText.text = ACList[0].StandardDeviation.ToString();
    }

    // Updates All Fields when Access Code selected changes
    public void ChangeAccessCode(){
        string selected = AssignmentAccessCodes.options[AssignmentAccessCodes.value].text;
        for(int i = 0; i<ACList.Count;i++){
            if(ACList[i].AccessCode.ToString()==selected){
                StartDateText.text = ACList[i].StartTime;
                EndDateText.text = ACList[i].EndTime;
                AvgScoreText.text = ACList[i].AverageScore.ToString();
                StandardDText.text = ACList[i].StandardDeviation.ToString();
            }
        }
    }
}

[System.Serializable]
public class AssignmentReportClass{
    public int AccessCode;
    public float AverageScore;
    public float StandardDeviation;
    //need check if time is in string
    public string StartTime;
    public string EndTime;
}