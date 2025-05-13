using UnityEngine;
using UnityEngine.SceneManagement;

public class Book_manager : MonoBehaviour
{
    public GameObject SelectScreen;
    [Space]
    public Animator animator;


    public void OpenAndClose_SelectScreen()
    {
        //ȭ���� ���������� Ű��, ���������� ����

        if (SelectScreen.transform.GetChild(0).gameObject.activeSelf)//ȭ���� �������� ��
        {
            SelectScreen.transform.GetChild(0).gameObject.SetActive(false);
            SelectScreen.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            SelectScreen.transform.GetChild(0).gameObject.SetActive(true);
            SelectScreen.transform.GetChild(1).gameObject.SetActive(true);
            animator.SetTrigger("Open");
        }
    }

    public void mousedown(bool selected)
    {
        animator.SetTrigger("Close");
    }
}
