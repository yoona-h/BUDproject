using TMPro;
using UnityEngine;

public class Basket : MonoBehaviour
{
    [SerializeField] GameObject button;

    MakeTeaManager makeTeaManager;

    void Start()
    {
        // 스크립트 가져오기
        makeTeaManager = GameObject.FindWithTag("GameController").GetComponent<MakeTeaManager>();
        GetComponent<Collider2D>().enabled = false;
    }

    void Update()
    {
        GetComponent<Collider2D>().enabled = makeTeaManager.isSelectIndredient && !makeTeaManager.isOilExtraction;
    }

    void OnMouseEnter() 
    {
        button.SetActive(true);
    }

    void OnMouseExit() 
    {
        button.SetActive(false);
    }
}
