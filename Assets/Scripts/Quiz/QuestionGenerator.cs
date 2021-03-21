//This script fetches the question and checks if it is correct

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public Button answerButton1;
    public Button answerButton2;
    public Button answerButton3;
    public Button answerButton4;


    
    private static string SampleQuestions = File.ReadAllText ("Assets/Scripts/Quiz/SampleQuestions.json"); 
    JSONNode file = JSON.Parse(SampleQuestions);

    private int questionNo; //current question

    void Start()
    {
        GenerateQuestion(0);

    }

    void GenerateQuestion(int questionNumber){
        
        string question = file["message"][questionNumber]["Question"];

        string answer1 = file["message"][questionNumber]["Option1"];
        string answer2 = file["message"][questionNumber]["Option2"];
        string answer3 = file["message"][questionNumber]["Option3"];
        string answer4 = file["message"][questionNumber]["Option4"];

        displayQuestion.text = question;
        questionNo = questionNumber;
        displayAnswer1.text = answer1;
        displayAnswer2.text = answer2;
        displayAnswer3.text = answer3;
        displayAnswer4.text = answer4;     

        
    }

    bool CheckAnswer(int selectedAnswer){
        int correctAnswer = file["message"][questionNo]["Answer"];
        
        if (correctAnswer == selectedAnswer){
            return true;

        } else{
            return false;
        }
    }

    public void OnSelectAnswer1(){

        if (CheckAnswer(1)){
            answerButton1.GetComponent<Image>().color = Color.green;
        } else{
            answerButton1.GetComponent<Image>().color = Color.red;
        }
    }

    public void OnSelectAnswer2(){

        if (CheckAnswer(2)){
            answerButton2.GetComponent<Image>().color = Color.green;
        } else {
            answerButton2.GetComponent<Image>().color = Color.red;
        }
    }

    public void OnSelectAnswer3(){

        if (CheckAnswer(3)){
            answerButton3.GetComponent<Image>().color = Color.green;
        } else {
            answerButton3.GetComponent<Image>().color = Color.red;
        }
    }

    public void OnSelectAnswer4(){

        if (CheckAnswer(4)){
            answerButton4.GetComponent<Image>().color = Color.green;
        } else{
            answerButton4.GetComponent<Image>().color = Color.red;
        }
    }

    

    
}
