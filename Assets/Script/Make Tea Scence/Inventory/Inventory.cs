using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] GameObject oil;
    [SerializeField] GameObject ingredient;
    [SerializeField] Collider2D ingredientWindow;
    
    MakeTeaManager makeTeaManager;

    void Start()
    {
        // 스크립트 가져오기
        makeTeaManager = GameObject.FindWithTag("GameController").GetComponent<MakeTeaManager>();

        oil.SetActive(false);
    }

    public void ExtractOil()
    {
        oil.SetActive(true);
        ingredient.SetActive(false);
        ingredientWindow.enabled = false;
        makeTeaManager.isOilExtraction = true;
    }

}
