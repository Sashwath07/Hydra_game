using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.SceneManagement;

public class AccessCodePanel : MonoBehaviour
{
    // Start is called before the first frame update
    public Button SubmitAccessCode;
    public Button BacktoMainMenu;
    public Button EnterAssignment;
    public GameObject AssignmentFirstPanel;
    public GameObject AssignmentPanel;
    public GameObject EndAssignmentPanel;
    public TMP_InputField AccessCodeField;
    public static int CurrentQuestionNumber;
    public static int Selectedanswer;
    public TMP_Text QuestionText;
    public TMP_Text Option1Text;
    public TMP_Text Option2Text;
    public TMP_Text Option3Text;
    public TMP_Text Option4Text;
    public Button Option1Button;
    public Button Option2Button;
    public Button Option3Button;
    public Button Option4Button;
    public List<int> AnswerList = new List<int>();
    public Button NextQuestionButton;
    public Button ReturnMainMenu;
    public List<AssignmentQuestion> AssignmentQuestionList = new List<AssignmentQuestion>();

    public string BaseURL = "https://223.25.69.254:10002/get_assignment/access_code=";
    
    void Start()
    {
        SubmitAccessCode.onClick.AddListener(() => {
            AssignmentFirstPanel.SetActive(true);
        });

        EnterAssignment.onClick.AddListener(() => {
            AssignmentFirstPanel.SetActive(false);
            ImportFirstQuestion();
            AssignmentPanel.SetActive(true);
        });

        NextQuestionButton.onClick.AddListener(() => {
            string temp = NextQuestionButton.GetComponentInChildren<TMP_Text>().text;
            if (temp=="Next Question"){
                UpdateQuestion();
            }
            else{
                SubmitAssignment();
            }
            
        });

        Option1Button.onClick.AddListener(() => {
            QuestionAnswer(1);
        });

        Option2Button.onClick.AddListener(() => {
            QuestionAnswer(2);
        });

        Option3Button.onClick.AddListener(() => {
            QuestionAnswer(3);
        });

        Option4Button.onClick.AddListener(() => {
            QuestionAnswer(4);
        });

        ReturnMainMenu.onClick.AddListener(() => {
            SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
        });

        BacktoMainMenu.onClick.AddListener(() => {
            SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
        });

    }

    public void ImportFirstQuestion(){
        StartCoroutine(CallAPI());
    }
    // Calls API, takes the assignment with the corresponding access code and creates questions
    IEnumerator CallAPI(){
        Debug.Log(AccessCodeField.text);
        string AccessCode = AccessCodeField.text;
        Debug.Log(AccessCode);
        string AccessCodeUrl = BaseURL + AccessCode;
        Debug.Log(AccessCodeUrl);
        UnityWebRequest APIRequest = UnityWebRequest.Get(AccessCodeUrl);
        APIRequest.certificateHandler = new WebRequestCert();   //force accept certificate

        yield return APIRequest.SendWebRequest();

        // reads json file, adds to current AssignmentList for easier reference
        JSONNode file = JSON.Parse(APIRequest.downloadHandler.text);
        int AssignmentQuestionsCount = file["message"].Count;
        Debug.Log(AssignmentQuestionsCount);
        for(int i = 0; i<AssignmentQuestionsCount; i++){
            AssignmentQuestion tempQuestion = new AssignmentQuestion();
            Debug.Log("Loading Question Number " + i);
            tempQuestion.answer = file["message"][i]["answer"];
            tempQuestion.option1 = file["message"][i]["option1"];
            tempQuestion.option2 = file["message"][i]["option2"];
            tempQuestion.option3 = file["message"][i]["option3"];
            tempQuestion.option4 = file["message"][i]["option4"];
            tempQuestion.question = file["message"][i]["question"];
            Debug.Log(file["message"][i]["question"]);

            AssignmentQuestionList.Add(tempQuestion);
        }

        // Display first question
        QuestionText.text = AssignmentQuestionList[0].question;
        Option1Text.text = AssignmentQuestionList[0].option1;
        Option2Text.text = AssignmentQuestionList[0].option2;
        Option3Text.text = AssignmentQuestionList[0].option3;
        Option4Text.text = AssignmentQuestionList[0].option4;

        CurrentQuestionNumber = 1;
    }

    // temporary stores the answer selected for the question
    public void QuestionAnswer(int qnAnswer){
        Selectedanswer = qnAnswer;
        Debug.Log("Selected Answer: " + Selectedanswer);
    }

    // Update relevant fields for the next question
    public void UpdateQuestion(){
        Debug.Log("You have selected answer " + Selectedanswer + " for Question " + CurrentQuestionNumber);
        CurrentQuestionNumber++;
        // Stores the current answer in a list for that question
        AnswerList.Add(Selectedanswer);

        // Also updates all text in the panel for the next question
        QuestionText.text = AssignmentQuestionList[CurrentQuestionNumber-1].question;
        Option1Text.text = AssignmentQuestionList[CurrentQuestionNumber-1].option1;
        Option2Text.text = AssignmentQuestionList[CurrentQuestionNumber-1].option2;
        Option3Text.text = AssignmentQuestionList[CurrentQuestionNumber-1].option3;
        Option4Text.text = AssignmentQuestionList[CurrentQuestionNumber-1].option4;

        // If next question is final question, change from 'next question' button to 'submint assignment' 
        if(CurrentQuestionNumber==AssignmentQuestionList.Count){
            string temp = "Submit Assignment";
            NextQuestionButton.GetComponentInChildren<TMP_Text>().text = temp;
            NextQuestionButton.GetComponentInChildren<TMP_Text>().enableAutoSizing = true;
        }
    }

    // when student clicks on the submit assignment button
    public void SubmitAssignment(){
        string tempAC = AccessCodeField.text;
        Debug.Log("Access code is " +tempAC);
        int AccessCode = int.Parse(tempAC);
        Debug.Log(AccessCode + "is type" + AccessCode.GetType());
        string Username = Login.username; // need find later
        Debug.Log("Username is "+ Username);
        float AssignmentScore = FindScore();
        Debug.Log("Score is "+ AssignmentScore);
        string UpdateUrl = "https://223.25.69.254:10002/update_assignment_performance/access_code=";
        UpdateUrl += AccessCode + "&username=" + Username + "&score=" + AssignmentScore;
        Debug.Log(UpdateUrl);
        StartCoroutine(SubmitAssignmentAPI(UpdateUrl));

    }

    // Calculate total score of assignment
    public int FindScore(){
        int score = 0;
        Debug.Log("AnswerList length is "+AnswerList.Count);
        Debug.Log("AssignmentQuestionList length is "+AssignmentQuestionList.Count);
        for(int i = 0; i<AnswerList.Count;i++){
            if(AnswerList[i]==AssignmentQuestionList[i].answer){
                score++;
            }
        }
        return score/AnswerList.Count;
    }

    // API call to submit assignment results to the database for a student
    IEnumerator SubmitAssignmentAPI(string url){
        Debug.Log("Calling API");
        UnityWebRequest APIRequest = UnityWebRequest.Get(url);
        APIRequest.certificateHandler = new WebRequestCert();   //force accept certificate

        yield return APIRequest.SendWebRequest();

        JSONNode outcome = JSON.Parse(APIRequest.downloadHandler.text);
        if (outcome["status_code"]==200){
            Debug.Log("SubmitAssignmentAPI called successfully!");
            AssignmentPanel.SetActive(false);
            EndAssignmentPanel.SetActive(true);
        }
        else{
            Debug.Log("SubmitAssignmentAPI not called!");
        }

    }


    
    


}

[SerializeField]
public class AssignmentQuestion
{
    public string question {get; set; }
    public string option1 {get; set; }
    public string option2 {get; set; }
    public string option3 {get; set; }
    public string option4 {get; set; }
    public int answer {get; set; }
}