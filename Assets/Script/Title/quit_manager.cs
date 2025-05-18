using UnityEngine;
using TMPro;
using System.Collections;

public class quit_manager : MonoBehaviour
{
    title_manager title_manager;
    public Animator animator;

    private void Start()
    {
        title_manager = gameObject.GetComponent<title_manager>();
    }
    public void mouseenter(TMP_Text text_obj)
    {
        text_obj.color = Color.yellow;
    }
    public void mouseexit(TMP_Text text_obj)
    {
        text_obj.color = Color.white;
    }
    public void mousedown(bool selected)
    {
        if (selected)//게임 종료
        {
            title_manager.QuitGame();
        }
        else
        {
            animator.SetTrigger("Close");
            //StartCoroutine(end_of_closescreen());
        }
    }

    IEnumerator end_of_closescreen()
    {
        yield return new WaitForSeconds(0.25f);
        title_manager.OpenAndClose_QuitScreen();
        yield break;
    }
}
