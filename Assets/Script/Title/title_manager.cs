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


    private setting_manager setting_manager;
    private quit_manager quit_manager;

    private void Start()
    {
        setting_manager = gameObject.GetComponent<setting_manager>();
        quit_manager = gameObject.GetComponent<quit_manager>();
        setting_manager.apply_from_GameData();
        setting_manager.apply_Slider_to_Text();
        //모든 화면 미리 꺼놓기
        Book_screen.SetActive(false);
        Setting_screen.SetActive(false);
        Quit_screen.transform.GetChild(0).gameObject.SetActive(false);
        Quit_screen.transform.GetChild(1).gameObject.SetActive(false);
    }


    public void OpenAndClose_BookScreen()
    {
        //화면이 꺼져있으면 키고, 켜져있으면 끄기
        Book_screen.SetActive(!Book_screen.activeSelf);
    }
    public void OpenAndClose_SettingScreen()
    {
        //화면이 꺼져있으면 키고, 켜져있으면 끄기
        Setting_screen.SetActive(!Setting_screen.activeSelf);
        setting_manager.apply_from_GameData();
    }
    public void OpenAndClose_QuitScreen()
    {
        //화면이 꺼져있으면 키고, 켜져있으면 끄기
       
        if (Quit_screen.transform.GetChild(0).gameObject.activeSelf)//화면이 켜져있을 때
        {
            Quit_screen.transform.GetChild(0).gameObject.SetActive(false);
            Quit_screen.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            Quit_screen.transform.GetChild(0).gameObject.SetActive(true);
            Quit_screen.transform.GetChild(1).gameObject.SetActive(true);
            quit_manager.animator.SetTrigger("Open");
        }
    }
    public void QuitGame()//게임종료
    {
        GameData.Instance.SaveData();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
