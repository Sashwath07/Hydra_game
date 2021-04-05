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
    public Dropdown UserType;
    public Button LoginButton;
    public Button ExitButton;
    public Text LoginFeedback;
    public string loginAPIURL = "https://223.25.69.254:10002/verify_login/";

    public static string username;
    public static string usertype;

    IEnumerator LoginCheck(string URL){
        
        UnityWebRequest APIRequest = UnityWebRequest.Get(URL);
        APIRequest.certificateHandler = new WebRequestCert();   //force accept certificate

        yield return APIRequest.SendWebRequest();

        if (APIRequest.result == UnityWebRequest.Result.ConnectionError || APIRequest.result == UnityWebRequest.Result.ProtocolError){
            Debug.LogError(APIRequest.error);
            LoginFeedback.text = "Server is down. Please try again later.";
            LoginFeedback.gameObject.SetActive(true);
            yield break;
        }

        JSONNode APIinfo = JSON.Parse(APIRequest.downloadHandler.text);
        string APIinfoMessage = APIinfo["message"];
        string APIinfoCode = APIinfo["status_code"];

        if (APIinfoMessage == "authorized login") {
            if (UserType.value == 0) {   
                SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
            }
            else if (UserType.value == 1) {
                SceneManager.LoadScene("Teacher Menu", LoadSceneMode.Single);
            }
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
                username = UsernameInput.text;
                usertype = UserType.value.ToString();
                string loginAPIURLComplete = loginAPIURL + "username=" + UsernameInput.text + "&password=" + PasswordInput.text + "&accountType=" + usertype;
                StartCoroutine(LoginCheck(loginAPIURLComplete));
            }

        });

        ExitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}
