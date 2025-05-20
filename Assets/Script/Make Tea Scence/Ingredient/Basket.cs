using UnityEngine;

public class Basket : MonoBehaviour
{
    [SerializeField] GameObject button;

    MakeTeaManager makeTeaManager;

    void Start()
    {
        // 스크립트 가져오기
        makeTeaManager = GameObject.FindWithTag("GameController").GetComponent<MakeTeaManager>();
    }

    void OnMouseEnter() 
    {
        if(makeTeaManager.isSelectIndredient && !makeTeaManager.isOilExtraction)
            button.SetActive(true);
    }

    void OnMouseExit() 
    {
        button.SetActive(false);
    }
}
