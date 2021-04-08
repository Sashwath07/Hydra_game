//This script fetches questions from database and manages the quiz component

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SimpleJSON;
using TMPro;
using System.IO;

public class QuestionGenerator : MonoBehaviour
{
    public TMP_Text displayQuestion;
    public TMP_Text displayAnswer1;
    public TMP_Text displayAnswer2;
    public TMP_Text displayAnswer3;
    public TMP_Text displayAnswer4;
    public TMP_Text displayScore;

    public Button answerButton1;
    public Button answerButton2;
    public Button answerButton3;
    public Button answerButton4;

    public Button nextQuestion;

    public int numberOfQuestions = QuizHandler.numOfQns;

    IList<Question> QuestionList = new List<Question>(){
        new Question() { },
        new Question() { },
        new Question() { },
        new Question() { }
    };
    private static string worldSelected = WorldSelect.worldSelected.ToString();
    private static string sectionSelected = SectionSelect.sectionSelected.ToString();
    private static string levelSelected = LevelSelect.levelSelected.ToString();
    // private static string username = Login.username;
    // private static string worldSelected = "1";
    // private static string sectionSelected = "1";
    // private static string levelSelected = "1";
    private static string username = "SHAFIQ002";
    private static string baseUrl = "https://223.25.69.254:10002/retrieve_questions/world=";
    private string Url = baseUrl + worldSelected + "&section=" + sectionSelected + "&level=" + levelSelected + "&username=" + username;


    void Start(){
        QuizHandler.Score = 0;
        displayQuestion.text = "Loading Question...";
        StartCoroutine(CallAPI());
    }

    public void OnNextQuestion(){
        if (numberOfQuestions-1 > 0){
            numberOfQuestions--;
            SetQuestions(numberOfQuestions-1);
            ResetButton();
        } else {
            SceneManager.LoadScene("Quiz Ended");
        }
        
    }

    IEnumerator CallAPI(){
        
        UnityWebRequest APIRequest = UnityWebRequest.Get(Url);
        APIRequest.certificateHandler = new WebRequestCert();   //force accept certificate

        yield return APIRequest.SendWebRequest();

        if (APIRequest.result == UnityWebRequest.Result.ConnectionError || APIRequest.result == UnityWebRequest.Result.ProtocolError){
            Debug.LogError(APIRequest.error);
            yield break;
        }

        JSONNode file = JSON.Parse(APIRequest.downloadHandler.text);

        for (int i = 0; i < QuestionList.Count; i++)
        {
            QuestionList[i].question = file["message"][i]["Question"];
            QuestionList[i].answer1 = file["message"][i]["Option1"];
            QuestionList[i].answer2 = file["message"][i]["Option2"];
            QuestionList[i].answer3 = file["message"][i]["Option3"];
            QuestionList[i].answer4 = file["message"][i]["Option4"];
            QuestionList[i].correctAnswer = file["message"][i]["Answer"];
        }
        SetQuestions(3);

   }

    void SetQuestions(int questionNumber){
        displayQuestion.text = QuestionList[questionNumber].question;
        displayAnswer1.text = QuestionList[questionNumber].answer1;
        displayAnswer2.text = QuestionList[questionNumber].answer2;
        displayAnswer3.text = QuestionList[questionNumber].answer3;
        displayAnswer4.text = QuestionList[questionNumber].answer4;  
   }

    void ResetButton(){
        answerButton1.GetComponent<Image>().color = Color.white;
        answerButton2.GetComponent<Image>().color = Color.white;
        answerButton3.GetComponent<Image>().color = Color.white;
        answerButton4.GetComponent<Image>().color = Color.white;

        answerButton1.interactable = true;
        answerButton2.interactable = true;
        answerButton3.interactable = true;
        answerButton4.interactable = true;
    }

    void SetButtonFalse(){
        answerButton1.interactable = false;
        answerButton2.interactable = false;
        answerButton3.interactable = false;
        answerButton4.interactable = false;
    }

    public void OnSelectAnswer1(){

        if (CheckAnswer(1)){
            answerButton1.GetComponent<Image>().color = Color.green;
        } else{
            answerButton1.GetComponent<Image>().color = Color.red;
        }

        SetButtonFalse();
    }

    public void OnSelectAnswer2(){

        if (CheckAnswer(2)){
            answerButton2.GetComponent<Image>().color = Color.green;
        } else {
            answerButton2.GetComponent<Image>().color = Color.red;
        }

        SetButtonFalse();
    }

    public void OnSelectAnswer3(){

        if (CheckAnswer(3)){
            answerButton3.GetComponent<Image>().color = Color.green;
        } else {
            answerButton3.GetComponent<Image>().color = Color.red;
        }

        SetButtonFalse();
    }

    public void OnSelectAnswer4(){

        if (CheckAnswer(4)){
            answerButton4.GetComponent<Image>().color = Color.green;
        } else{
            answerButton4.GetComponent<Image>().color = Color.red;
        }

        SetButtonFalse();
    }
    
    bool CheckAnswer(int selectedAnswer){
        int correctAnswer = QuestionList[numberOfQuestions-1].correctAnswer;
        
        if (correctAnswer == selectedAnswer){
            QuizHandler.Score += 1;
            displayScore.text = "Score: " + QuizHandler.Score.ToString();
            return true;

        } else{
            return false;
    }

    
    
}


public class Question
{
    public string question {get; set; }
    public string answer1 {get; set; }
    public string answer2 {get; set; }
    public string answer3 {get; set; }
    public string answer4 {get; set; }
    public int correctAnswer {get; set; }
}
}