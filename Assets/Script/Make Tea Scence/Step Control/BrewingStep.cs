using TMPro;
using UnityEngine;

public class BrewingStep : MonoBehaviour
{
    [Header("오브젝트")]
    [SerializeField] GameObject teaPot;
    [SerializeField] GameObject boilingWater;
    [SerializeField] GameObject teaCup;
    [SerializeField] GameObject leaf;
    [SerializeField] GameObject oil;

    public int ingredientCnt = 0;

    MakeTeaManager makeTeaManager;
    
    void Start()
    {
        // 스크립트 가져오기
        makeTeaManager = GameObject.FindWithTag("GameController").GetComponent<MakeTeaManager>();
        
        boilingWater.GetComponent<IngredientPourer>().FinPouring += CntIngredient;
        leaf.GetComponent<IngredientPourer>().FinPouring += CntIngredient;
        oil.GetComponent<IngredientPourer>().FinPouring += CntIngredient;
        teaPot.GetComponent<IngredientPourer>().FinPouring += FinBrewwing;

        teaPot.GetComponent<IngredientPourer>().enabled = false;
    }

    void CntIngredient()
    {
        ingredientCnt++;
        if(ingredientCnt == 3)
        {
            teaPot.GetComponent<IngredientPourer>().enabled = true;
        }
    }

    void FinBrewwing()
    {
        makeTeaManager.isBrewingFin = true;
        Debug.Log("차를 완성했습니다~");
    }
}
