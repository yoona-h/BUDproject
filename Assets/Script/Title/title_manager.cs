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

    private setting_manager setting_manager;
    private quit_manager quit_manager;

    private void Start()
    {
        setting_manager = gameObject.GetComponent<setting_manager>();
        quit_manager = gameObject.GetComponent<quit_manager>();
        setting_manager.apply_from_GameData();
        //��� ȭ�� �̸� ������
        Book_screen.SetActive(false);
        Setting_screen.SetActive(false);
        Quit_screen.transform.GetChild(0).gameObject.SetActive(false);
        Quit_screen.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void StartButton_function()
    {
        if (GameData.Instance.FirstGame)//ó���������� ��
            SceneManager.LoadScene(TutorialScene);
        else
            SceneManager.LoadScene(StartScene);
    }

    public void OpenAndClose_BookScreen()
    {
        //ȭ���� ���������� Ű��, ���������� ����
        Book_screen.SetActive(!Book_screen.activeSelf);
    }
    public void OpenAndClose_SettingScreen()
    {
        //ȭ���� ���������� Ű��, ���������� ����
        Setting_screen.SetActive(!Setting_screen.activeSelf);
        setting_manager.apply_from_GameData();
    }
    public void OpenAndClose_QuitScreen()
    {
        //ȭ���� ���������� Ű��, ���������� ����
       
        if (Quit_screen.transform.GetChild(0).gameObject.activeSelf)//ȭ���� �������� ��
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
    public void QuitGame()//��������
    {
        //���ӵ����� �����ϴ� �Լ� �߰��ϱ�
        Application.Quit();
    }
}
