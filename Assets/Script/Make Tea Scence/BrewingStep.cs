using UnityEngine;

public class BrewingStep : MonoBehaviour
{
    [Header("오브젝트")]
    [SerializeField] GameObject teaPot;

    MakeTeaManager makeTeaManager;
    int ingredientCnt;

    void Start()
    {
        // 스크립트 가져오기
        makeTeaManager = GameObject.FindWithTag("GameController").GetComponent<MakeTeaManager>();
        
        teaPot.SetActive(false);
    }

    void TeaPotAni()
    {
        teaPot.SetActive(true);
        // TODO: 개완 흔들기 애니메이션
        teaPot.GetComponent<IngredientPourer>().enabled = true;
    }

    public void SetIngredientCnt()
    {
        ingredientCnt++;
    }
}
