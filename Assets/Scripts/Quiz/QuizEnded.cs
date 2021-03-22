using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuizEnded : MonoBehaviour
{
    public TMP_Text Score;

    void Start(){
        Score.text = "Your Score: " + QuizHandler.Score.ToString();
    }     
}
