using UnityEngine;
using UnityEngine.UI;

public class talk_sys : MonoBehaviour
{
    public Text talktext;
    public Image talk_box;
    public string[] lines;
    private int index = 0;

    private void Start()
    {
        talk_box.enabled = false;
        talktext.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            talk_box.enabled = true;
            talktext.enabled = true;
            ShowNext();
        }
    }

    void ShowNext()
    {
        if (index < lines.Length)
        {
            talktext.text = lines[index];
            index++;
        }
        else
        {
            talk_box.enabled = false;
            talktext.enabled = false;
        }
    }
}

