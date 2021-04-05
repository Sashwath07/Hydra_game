using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using SimpleJSON;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public static bool GameIsOver;
    public static bool LevelCompleted;

    private string scoreAPIURL = "https://223.25.69.254:10002/update_gamescore/";

    private string currentWorld;
    private string currentLevel;
    private string username;
    private string currentSection;

    void Start(){
        GameIsOver = false;
        LevelCompleted = false;

        //data to return to database
        currentWorld = WorldSelect.worldSelected.ToString();
        currentLevel = LevelSelect.levelSelected.ToString();
        currentSection = SectionSelect.sectionSelected.ToString();
        username = Login.username;
    }

    void Update()
    {
        if (GameIsOver){
            return;
        }

        if (LevelCompleted && WaveSpawner.EnemiesAlive == 0)
        {
            EndGame();
            return;
        }

        if (PlayerStats.Lives <= 0){
            EndGame();
        }
    }

    void EndGame(){
        GameIsOver = true;
        


        if (!PVP.isPvp){
            gameOverUI.SetActive(true);
            string fullScoreAPIUrl = scoreAPIURL + "username=" + this.username + "&points=" + PlayerStats.GameScore.ToString() + "&world=" + this.currentWorld + "&section=" + this.section + "&level=" + currentLevel;
            StartCoroutine(UpdateScore(fullScoreAPIUrl));
        } else{
            SceneManager.LoadScene("PVP Game Ended");
        }
        
    }

    IEnumerator UpdateScore(string URL)
    {
        UnityWebRequest APIRequest = UnityWebRequest.Get(URL);
        APIRequest.certificateHandler = new WebRequestCert();

        yield return APIRequest.SendWebRequest();

        if (APIRequest.result == UnityWebRequest.Result.ConnectionError || APIRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(APIRequest.error);
            Debug.LogError("Error at: " + URL);
            yield break;
        }

        JSONNode APIinfo = JSON.Parse(APIRequest.downloadHandler.text);
        string APIInfoMessage = APIinfo["message"];
        string APIInfoCode = APIinfo["status_code"];




        if(int.Parse(APIInfoCode)>=200 && int.Parse(APIInfoCode) <= 299)
        {
            Debug.Log("Successful update: " + APIInfoMessage + "|" + APIInfoCode);
            yield break;
        }
        else
        {
            Debug.LogError("Error in updating score");
            Debug.LogError("Message: " + APIInfoMessage);
            Debug.LogError("Status Code: " + APIInfoCode);

        }
    }
}
