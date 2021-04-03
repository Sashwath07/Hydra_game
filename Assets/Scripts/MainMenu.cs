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

    public Button AssignmentButton;
    public Button LogoutButton;

    public Text UsernameText;
    public Text ProficiencyText;
    public Text PointsText;
    public Text WorldText;
    public Image AvatarIcon;
    public Sprite FireAvatar;
    public Sprite WaterAvatar;
    public Sprite WindAvatar;
    public Sprite EarthAvatar;


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

        UsernameText.text = APIinfo["message"]["Username"];
        ProficiencyText.text = "Proficiency: " + APIinfo["message"]["Proficiency"];
        PointsText.text = "Points: " + APIinfo["message"]["Points"];
        WorldText.text = "World: " + APIinfo["message"]["CurrentWorld"];

        if (APIinfo["message"]["Avatar"] == 1) {
            AvatarIcon.sprite = FireAvatar;
        }
        else if (APIinfo["message"]["Avatar"] == 2) {
            AvatarIcon.sprite = WaterAvatar;
        }
        else if (APIinfo["message"]["Avatar"] == 3) {
            AvatarIcon.sprite = WindAvatar;
        }
        else if (APIinfo["message"]["Avatar"] == 4) {
            AvatarIcon.sprite = EarthAvatar;
        }
        else {
            Debug.Log(APIinfo["message"]["Avatar"]);
        }
        
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

        AssignmentButton.onClick.AddListener(() => {
            SceneManager.LoadScene("Assignment", LoadSceneMode.Single);
        });

        LogoutButton.onClick.AddListener(() => {
            SceneManager.LoadScene("Login Scene", LoadSceneMode.Single);
        });
    }
}