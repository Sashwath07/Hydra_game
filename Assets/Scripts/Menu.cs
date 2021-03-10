using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Button StoryModeButton;
    public Button PVPButton;
    public Button LeaderboardButton;
    public Button LogoutButton;
 
    // Start is called before the first frame update
    void Start()
    {
        StoryModeButton.onClick.AddListener(() => {
            //add purpose of storymode button
        });

        PVPButton.onClick.AddListener(() => {
            //add purpose of storymode button
        });

        LeaderboardButton.onClick.AddListener(() => {
            //add purpose of storymode button
        });

        LogoutButton.onClick.AddListener(() => {
            SceneManager.LoadScene("Login Scene", LoadSceneMode.Single);
        });
    }
}
