using Unity.VisualScripting;
using UnityEngine;

public class Help : MonoBehaviour
{
    [SerializeField] GameObject HelpWindow;

    bool isOpen = false;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnButtonClick()
    {
        isOpen = !isOpen;
        HelpWindow.SetActive(isOpen);
        if(isOpen) Time.timeScale = 0;
        else Time.timeScale = 1;
    }
        
}
