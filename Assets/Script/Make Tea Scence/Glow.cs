using UnityEngine;

public class Glow : MonoBehaviour
{
    [SerializeField] GameObject glow;

    void OnMouseEnter() 
    {
        glow.SetActive(true);   
    }

    void OnMouseExit() 
    {
        glow.SetActive(false);   
    }

    void OnDisable()
    {
        glow.SetActive(false);
    }
}
