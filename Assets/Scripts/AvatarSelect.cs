using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;

public class AvatarSelect : MonoBehaviour
{
    public Image LoginImage;
    public Image CharacterSelection;
    public Button Character1;
    public Button Character2;
    public Button Character3;
    public Button Character4;
    public Text Feedback;

    
    public string avatarAPIURL = "https://223.25.69.254:10002/create_ingame_account/";
    public string avatar;

    IEnumerator RegisterAvatar(string URL){
        
        UnityWebRequest APIRequest = UnityWebRequest.Get(URL);
        APIRequest.certificateHandler = new WebRequestCert();   //force accept certificate

        yield return APIRequest.SendWebRequest();

        if (APIRequest.result == UnityWebRequest.Result.ConnectionError || APIRequest.result == UnityWebRequest.Result.ProtocolError){
            Debug.LogError(APIRequest.error);
            Feedback.text = "Server is down. Please try again later.";
            Feedback.gameObject.SetActive(true);
            yield break;
        }

        JSONNode APIinfo = JSON.Parse(APIRequest.downloadHandler.text);
        string APIinfoMessage = APIinfo["message"];
        string APIinfoCode = APIinfo["status_code"];

        if (APIinfoMessage == "in game account created") {
            CharacterSelection.gameObject.SetActive(false);
            LoginImage.gameObject.SetActive(true);
        }
        else {
            Debug.Log("Error at AvatarSelect");
            Debug.Log(URL);
            Debug.Log("message = " + APIinfoMessage);
            Debug.Log("code = " + APIinfoCode);
        }


    }

    // Start is called before the first frame update
    void Start()
    {
        Character1.onClick.AddListener(() => {
            avatar = "1";
            string avatarAPIURLComplete = avatarAPIURL + "username=" + Register.newusername + "&avatar=" + avatar;
            StartCoroutine(RegisterAvatar(avatarAPIURLComplete));
            });

        Character2.onClick.AddListener(() => {
            avatar = "2";
            string avatarAPIURLComplete = avatarAPIURL + "username=" + Register.newusername + "&avatar=" + avatar;
            StartCoroutine(RegisterAvatar(avatarAPIURLComplete));
            });

        Character3.onClick.AddListener(() => {
            avatar = "3";
            string avatarAPIURLComplete = avatarAPIURL + "username=" + Register.newusername + "&avatar=" + avatar;
            StartCoroutine(RegisterAvatar(avatarAPIURLComplete));
            });

        Character4.onClick.AddListener(() => {
            avatar = "4";
            string avatarAPIURLComplete = avatarAPIURL + "username=" + Register.newusername + "&avatar=" + avatar;
            StartCoroutine(RegisterAvatar(avatarAPIURLComplete));
            });
    }

}
