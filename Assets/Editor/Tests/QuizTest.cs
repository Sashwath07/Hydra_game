using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class QuizTest
{
    
    [Test]
    public void CheckIfQuizScoreIsReset(){
        var score = QuizHandler.Score;
        Assert.AreEqual(score, 0);
    }

    [Test]
    public void CheckNumOfQns(){
        var numOfQns = QuizHandler.numOfQns;
        Assert.AreEqual(numOfQns, 4);
    }

    // [UnityTest]
    // public IEnumerator CheckButtonsReset(){
    //     GameObject gameObject = new GameObject();
    //     gameObject.AddComponent<QuestionGenerator>();
    //     gameObject.GetComponent<QuestionGenerator>().OnNextQuestion();
    //     yield return new WaitForEndOfFrame();
    //     yield return new WaitForEndOfFrame();
    //     Assert.AreEqual(Color.white, gameObject.GetComponent<QuestionGenerator>().answerButton1.GetComponent<Image>().color);
    //     Assert.AreEqual(Color.white, gameObject.GetComponent<QuestionGenerator>().answerButton2.GetComponent<Image>().color);
    //     Assert.AreEqual(Color.white, gameObject.GetComponent<QuestionGenerator>().answerButton3.GetComponent<Image>().color);
    //     Assert.AreEqual(Color.white, gameObject.GetComponent<QuestionGenerator>().answerButton4.GetComponent<Image>().color);

    // }

    // [Test]
    // public void CheckButtonsReset2(){
    //     var gameObject = new GameObject();
    //     var questionGenerator = gameObject.AddComponent<QuestionGenerator>();
    //     questionGenerator.OnNextQuestion();
    //     Assert.AreEqual(Color.white, questionGenerator.answerButton1.GetComponent<Image>().color);
    //     Assert.AreEqual(Color.white, questionGenerator.answerButton2.GetComponent<Image>().color);
    //     Assert.AreEqual(Color.white, questionGenerator.answerButton3.GetComponent<Image>().color);
    //     Assert.AreEqual(Color.white, questionGenerator.answerButton4.GetComponent<Image>().color);

    // }
}
