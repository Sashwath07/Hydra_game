using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SimpleJSON;

public class MainMenu : MonoBehaviour
{
    public Button StoryModeButton;
    public Button PVPButton;
    public Button LeaderboardButton;
    public Button LogoutButton;

    public Text UsernameText;
    public Text LevelText;
    public Text WorldText;

    IEnumerator RetrieveAcct(string URL){
        
        UnityWebRequest APIRequest = UnityWebRequest.Get(URL);
        APIRequest.certificateHandler = new WebRequestCert();   //force accept certificate

        yield return APIRequest.SendWebRequest();

        if (APIRequest.isNetworkError || APIRequest.isHttpError){
            Debug.LogError(APIRequest.error);
            yield break;
        }

        JSONNode APIinfo = JSON.Parse(APIRequest.downloadHandler.text);
        string APIinfoMessage = APIinfo["message"];
        string APIinfoCode = APIinfo["status_code"];

        Debug.Log(APIinfoMessage);
        APIinfoMessage = APIinfoMessage.Trim(new Char[] { '(', ')' });
        Debug.Log(APIinfoMessage);
        string[] AccountInfo = APIinfoMessage.Split(',');

        UsernameText.text = AccountInfo[0].Replace("'", "");
        LevelText.text = "Level " + AccountInfo[1];
        WorldText.text = "Current World: " + AccountInfo[2];
    }

    void Start()
    {
        string APIurl = "https://223.25.69.254:10002/retrieve_account/username=" + Login.username;
        StartCoroutine(RetrieveAcct(APIurl));



        StoryModeButton.onClick.AddListener(() => {
            SceneManager.LoadScene("World Select", LoadSceneMode.Single);
        });

        PVPButton.onClick.AddListener(() => {
            //add pvp scene
        });

        LeaderboardButton.onClick.AddListener(() => {
            SceneManager.LoadScene("Leaderboard", LoadSceneMode.Single);
        });

        LogoutButton.onClick.AddListener(() => {
            SceneManager.LoadScene("Login Scene", LoadSceneMode.Single);
        });
    }
}