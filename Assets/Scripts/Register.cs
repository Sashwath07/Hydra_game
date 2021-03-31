﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;

public class Register : MonoBehaviour
{
    public Image RegisterImage;
    public Image CharacterSelection;
    public InputField NewUsernameInput;
    public InputField NewPasswordInput;
    public InputField ConfirmPasswordInput;
    public Button RegisterAccountButton;
    public Text RegisterFeedback;

    public string registerAPIURL = "https://223.25.69.254:10002/create_account/";
    public string accountType = "0";
    public static string newusername;


    IEnumerator RegisterAccount(string URL){
        
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

        if (APIinfoMessage == "account created") {
            RegisterImage.gameObject.SetActive(false);
            CharacterSelection.gameObject.SetActive(true);
        }
        else {
            Debug.Log("Error at Register");
            Debug.Log(URL);
            Debug.Log("message = " + APIinfoMessage);
            Debug.Log("code = " + APIinfoCode);            
            RegisterFeedback.text = "Error! Code: " + APIinfoCode;
            RegisterFeedback.gameObject.SetActive(true);
        }


    }

    // Start is called before the first frame update
    void Start()
    {
        RegisterAccountButton.onClick.AddListener(() => {
            if (NewUsernameInput.text == "" || NewPasswordInput.text == "" || ConfirmPasswordInput.text == "") {       
                RegisterFeedback.text = "Username or Password field is empty!";
                RegisterFeedback.gameObject.SetActive(true);
            }
            else if (NewPasswordInput.text != ConfirmPasswordInput.text) {
                RegisterFeedback.text = "Password and Confirm Password are different!";
                RegisterFeedback.gameObject.SetActive(true);
            }
            else {
                newusername = NewUsernameInput.text;
                string registerAPIURLComplete = registerAPIURL + "username=" + NewUsernameInput.text + "&password=" + NewPasswordInput.text + "&accountType=" + accountType;
                StartCoroutine(RegisterAccount(registerAPIURLComplete));

            }
        });
    }
}
