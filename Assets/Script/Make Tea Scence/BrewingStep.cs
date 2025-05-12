using UnityEngine;

public class BrewingStep : MonoBehaviour
{
    [Header("오브젝트")]
    [SerializeField] GameObject teaPot;
    [SerializeField] GameObject boilingWater;
    [SerializeField] GameObject teaCup;
    [SerializeField] GameObject leaf;
    [SerializeField] GameObject oil;

    MakeTeaManager makeTeaManager;
    int ingredientCnt = 0;

    void Start()
    {
        // 스크립트 가져오기
        makeTeaManager = GameObject.FindWithTag("GameController").GetComponent<MakeTeaManager>();
    }

    


}
