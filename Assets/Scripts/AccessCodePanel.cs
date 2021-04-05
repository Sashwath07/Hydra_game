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
    // public Button SubmitButton;
    public Button BacktoMainMenu;
    public Button EnterAssignment;
    public GameObject AssignmentFirstPanel;
    public GameObject AssignmentPanel;
    public GameObject EndAssignmentPanel;
    public TMP_InputField AccessCodeField;
    public static int CurrentQuestionNumber;
    public static int Selectedanswer;
    public TMP_Text DoneBeforeText;
    public TMP_Text QuestionText;
    public TMP_Text Option1Text;
    public TMP_Text Option2Text;
    public TMP_Text Option3Text;
    public TMP_Text Option4Text;
    // public Button Option1Button;
    // public Button Option2Button;
    // public Button Option3Button;
    // public Button Option4Button;
    public List<int> AnswerList = new List<int>();
    public Button NextQuestionButton;
    public Button ReturnMainMenu;
    static public List<AssignmentQuestion> AssignmentQuestionList;

    public string BaseURL = "https://223.25.69.254:10002/get_assignment/access_code=";
    
    void Start()
    {   
        AssignmentQuestionList = new List<AssignmentQuestion>();
        EnterAssignment.onClick.AddListener(() => {
            if (DoneBeforeText.isActiveAndEnabled){
                DoneBeforeText.gameObject.SetActive(false);
            }
            AssignmentFirstPanel.SetActive(false);
            // ImportFirstQuestion();
            AssignmentPanel.SetActive(true);
        });

        // NextQuestionButton.onClick.AddListener(() => {
        //     string temp = NextQuestionButton.GetComponentInChildren<TMP_Text>().text;
        //     AddtoList();
        //     if (temp=="Next Question"){
        //         UpdateQuestion();
        //     }
        //     else{
        //         SubmitAssignment();
        //     }
        // });

        // Option1Button.onClick.AddListener(() => {
        //     QuestionAnswer(1);
        // });

        // Option2Button.onClick.AddListener(() => {
        //     QuestionAnswer(2);
        // });

        // Option3Button.onClick.AddListener(() => {
        //     QuestionAnswer(3);
        // });

        // Option4Button.onClick.AddListener(() => {
        //     QuestionAnswer(4);
        // });

        ReturnMainMenu.onClick.AddListener(() => {
            SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
        });

        BacktoMainMenu.onClick.AddListener(() => {
            SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
        });

    }
    
    public void CheckDoneAssignment(){
        StartCoroutine(CheckDoneAssignmentBeforeAPI());
    }
    // Calls API, takes the assignment with the corresponding access code and creates questions
    IEnumerator CheckDoneAssignmentBeforeAPI(){
        Debug.Log(AccessCodeField.text);
        string AccessCode = AccessCodeField.text;
        Debug.Log(AccessCode);
        string AccessCodeUrl = BaseURL + AccessCode + "&username=" + Login.username;
        Debug.Log(AccessCodeUrl);
        UnityWebRequest APIRequest = UnityWebRequest.Get(AccessCodeUrl);
        APIRequest.certificateHandler = new WebRequestCert();   //force accept certificate

        yield return APIRequest.SendWebRequest();

        // reads json file
        JSONNode file = JSON.Parse(APIRequest.downloadHandler.text);

        // checks if access code is valid or if student done before and displays corresponding message
        
        if (AccessCodeField.text==""){
            DoneBeforeText.text = "Access Code is required before proceeding";
            DoneBeforeText.gameObject.SetActive(true);
        }
        else if (file["status_code"] != 200){
            string temp = file["message"];
            DoneBeforeText.text = temp;
            Debug.Log(file["message"]);
            DoneBeforeText.gameObject.SetActive(true);
        }
        else{
            if (DoneBeforeText.isActiveAndEnabled){
                DoneBeforeText.gameObject.SetActive(false);
            }
            // adds to current AssignmentList for easier reference
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
            // If next question is final question, change from 'next question' button to 'submint assignment' 
            if(CurrentQuestionNumber==AssignmentQuestionList.Count){
                string temp = "Submit Assignment";
                NextQuestionButton.GetComponentInChildren<TMP_Text>().text = temp;
                NextQuestionButton.GetComponentInChildren<TMP_Text>().enableAutoSizing = true;
            }

            AssignmentFirstPanel.SetActive(true);
        }
    }

    // temporary stores the answer selected for the question
    public void QuestionAnswer(int qnAnswer){
        Selectedanswer = qnAnswer;
        Debug.Log("Selected Answer: " + Selectedanswer);
    }

    public void NextQuestionClick(){
        string temp = NextQuestionButton.GetComponentInChildren<TMP_Text>().text;
        AddtoList();
        if (temp=="Next Question"){
            UpdateQuestion();
        }
        else{
            SubmitAssignment();
        }
    }
    // stores answer to list before checking if next question/submit assignment
    public void AddtoList(){
        Debug.Log("You have selected answer " + Selectedanswer + " for Question " + CurrentQuestionNumber);
        CurrentQuestionNumber++;
        // Stores the current answer in a list for that question
        AnswerList.Add(Selectedanswer);
    }

    // Update relevant fields for the next question
    public void UpdateQuestion(){
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
        Debug.Log(AccessCode + " is type" + AccessCode.GetType());
        string Username = Login.username; // need find later
        Debug.Log("Username is "+ Username);
        double AssignmentScore = FindScore();
        Debug.Log("Score is "+ AssignmentScore);
        string UpdateUrl = "https://223.25.69.254:10002/update_assignment_performance/access_code=";
        UpdateUrl += AccessCode + "&username=" + Username + "&score=" + AssignmentScore.ToString("#0.00");
        Debug.Log(UpdateUrl);
        StartCoroutine(SubmitAssignmentAPI(UpdateUrl));

    }

    // Calculate total score of assignment
    public double FindScore(){
        double score = 0;
        Debug.Log("AnswerList length is "+AnswerList.Count);
        Debug.Log("AssignmentQuestionList length is "+AssignmentQuestionList.Count);
        for(int i = 0; i<AnswerList.Count;i++){
            Debug.Log("For question " + (i+1) + ", the answer selected is " + AnswerList[i] 
            + " of type " + AnswerList[i].GetType() + ", with real answer of " + AssignmentQuestionList[i].answer
            + " of type " + AssignmentQuestionList[i].answer.GetType());
            Debug.Log(AnswerList[i]==AssignmentQuestionList[i].answer);
            if(AnswerList[i]==AssignmentQuestionList[i].answer){
                score++;
            }
        }
        Debug.Log("Final Score is "+score);
        double TotalScore = AnswerList.Count;
        // double APIScore = 1.0000;
        double APIScore = score/TotalScore;
        Debug.Log(APIScore);
        Debug.Log(APIScore.ToString("F2"));
        return APIScore;
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