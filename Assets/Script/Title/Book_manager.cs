using UnityEngine;
using UnityEngine.SceneManagement;

public class Book_manager : MonoBehaviour
{
    public GameObject SelectScreen;
    [Space]
    public Animator animator;


    public void OpenAndClose_SelectScreen()
    {
        //화면이 꺼져있으면 키고, 켜져있으면 끄기

        if (SelectScreen.transform.GetChild(0).gameObject.activeSelf)//화면이 켜져있을 때
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
