using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NewTestScript
{
    // A Test behaves as an ordinary method
    [Test]
    public void NewTestScriptSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }

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
    // public IEnumerator CheckQuestionsSet(){
    //     var gameObject = new GameObject();
    //     var questionGenerator = gameObject.AddComponent<QuestionGenerator>();
    //     yield return new WaitForSeconds(5f);
    //     Assert.IsNotEmpty(questionGenerator.displayQuestion.ToString());
    //     Assert.IsNotEmpty(questionGenerator.displayAnswer1.ToString());
    //     Assert.IsNotEmpty(questionGenerator.displayAnswer2.ToString());
    //     Assert.IsNotEmpty(questionGenerator.displayAnswer3.ToString());
    //     Assert.IsNotEmpty(questionGenerator.displayAnswer4.ToString());

    // }
}
