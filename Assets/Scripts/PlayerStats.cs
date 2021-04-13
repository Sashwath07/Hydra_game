using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 400;

    public static int Lives;
    public int startLives = 20;

    public static int Rounds;
    public static int questionCount = 5;
    public static int GameScore = 0;

    void Start(){
        //Money = startMoney;
        Debug.Log("Player score: " + QuizHandler.Score);
        Money = (1+QuizHandler.Score) * 100 + startMoney;

        Debug.Log("Player's starting money is $" + Money);
        Lives = startLives;
        GameScore = 0;

        Rounds = 0;
    }
}
