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

    [Header("Scence")]
    public string StartScene;
    public string TutorialScene;

    private void Start()
    {
        Book_screen.SetActive(false);
        Setting_screen.SetActive(false);
        Quit_screen.SetActive(false);
    }

    public void StartButton_function()
    {
        SceneManager.LoadScene(StartScene);
    }

    public void OpenAndClose_BookScreen()
    {
        Book_screen.SetActive(Book_screen.activeSelf);
    }
    public void OpenAndClose_SettingScreen()
    {
        Setting_screen.SetActive(Setting_screen.activeSelf);
    }
    public void OpenAndClose_QuitScreen()
    {
        Quit_screen.SetActive(Quit_screen.activeSelf);
    }
}
