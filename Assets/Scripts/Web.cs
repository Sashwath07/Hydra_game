using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Web : MonoBehaviour
{

    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/LoginUI/GetLogin.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.downloadHandler.text == "Login Success!") {
                    //if username and password is correct, load main menu scene
                    Debug.Log("Correct");
                    SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
                }
                else {
                    //if not correct, show error
                    Debug.Log(www.downloadHandler.text);
                }
            }
        }
    }

    public IEnumerator Register(string username, string password, string password2)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);
        form.AddField("loginPass2", password2);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/LoginUI/Register.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
}
