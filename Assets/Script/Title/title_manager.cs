using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class title_manager : MonoBehaviour
{
    [Header("Screens")]
    public GameObject Book_screen;
    public GameObject Setting_screen;
    public GameObject Quit_screen;

    [Header("Buttons")]
    public Button Start_button;
    public Button Book_button;
    public Button Setting_button;
    public Button Quit_button;

    public Scene StartScene;
    public Scene TutorialScene;

    public void StartButton_function()
    {
        
    }

    public void OpenAndClose_BookScreen()
    {

    }
    public void OpenAndClose_SettingScreen()
    {

    }
}
