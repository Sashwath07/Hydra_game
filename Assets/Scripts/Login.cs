using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SimpleJSON;

public class Login : MonoBehaviour
{
    public InputField UsernameInput;
    public InputField PasswordInput;
    public Button LoginButton;
    public Button ExitButton;
    public Text LoginFeedback;
    public string loginAPIURL = "https://223.25.69.254:10002/verify_login/";

    IEnumerator CallAPI(string URL){
        
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

        if (APIinfoMessage == "authorized login") {
            SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
        }
        else if (APIinfoMessage == "unauthorized login") {
            LoginFeedback.text = "Wrong Username or Password!";
            LoginFeedback.gameObject.SetActive(true);
        }
        else {
            Debug.Log("Error");
            Debug.Log("message = " + APIinfoMessage);
            Debug.Log("code = " + APIinfoCode);            
            LoginFeedback.text = "Error! Code: " + APIinfoCode;
            LoginFeedback.gameObject.SetActive(true);
        }


    }

    // Start is called before the first frame update
    void Start()
    {
        LoginButton.onClick.AddListener(() => {

            if (UsernameInput.text == "" || PasswordInput.text == "") {       
                LoginFeedback.text = "Username or Password field is empty!";
                LoginFeedback.gameObject.SetActive(true);
            }
            else {
                string loginAPIURLComplete = loginAPIURL + "username=" + UsernameInput.text + "&password=" + PasswordInput.text;
                Debug.Log(loginAPIURLComplete);
                StartCoroutine(CallAPI(loginAPIURLComplete));
            }

        });

        ExitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}
