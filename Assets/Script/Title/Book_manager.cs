using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Book_manager : MonoBehaviour
{
    public GameObject WarningScreen;
    [Space]
    public Animator animator;
    [Space(50)]
    public List<string> Scene_name = new List<string>();
    [HideInInspector] public int SelectedScene = 0;

    private void OnEnable()
    {
        WarningScreen.transform.GetChild(0).gameObject.SetActive(false);
        WarningScreen.transform.GetChild(1).gameObject.SetActive(false);
    }


    public void Start_StroyScene()
    {
        SceneManager.LoadScene(Scene_name[SelectedScene]);
    }


    public void OpenAndClose_WarningScreen()
    {
        //ȭ���� ���������� Ű��, ���������� ����

        if (WarningScreen.transform.GetChild(0).gameObject.activeSelf)//ȭ���� �������� ��
        {
            WarningScreen.transform.GetChild(0).gameObject.SetActive(false);
            WarningScreen.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            WarningScreen.transform.GetChild(0).gameObject.SetActive(true);
            WarningScreen.transform.GetChild(1).gameObject.SetActive(true);
            animator.SetTrigger("Open");
        }
    }

    public void mousedown(bool selected)
    {
        animator.SetTrigger("Close");
    }
}
