using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField UsernameInput;
    public InputField PasswordInput;
    public Button LoginButton;
    public Button ExitButton;
    public Text LoginFeedback;

    // Start is called before the first frame update
    void Start()
    {
        LoginButton.onClick.AddListener(() => {
            StartCoroutine(Main.Instance.Web.UserLogin(UsernameInput.text, PasswordInput.text));
        });

        ExitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}
