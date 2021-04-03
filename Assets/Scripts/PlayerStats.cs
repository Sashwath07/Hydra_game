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

    public static int GameScore = 0;

    void Start(){
        //Money = startMoney;
        Money = (1+QuizHandler.Score) * 100 + startMoney;
        Lives = startLives;
        GameScore = 1 + QuizHandler.Score;

        Rounds = 0;
    }
}
