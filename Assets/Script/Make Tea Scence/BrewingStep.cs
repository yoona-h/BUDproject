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

    [Header("Teapot content number")]
    [SerializeField] int contentNum = 3;

    public int ingredientCnt = 0;

    MakeTeaManager makeTeaManager;
    
    void Start()
    {
        // 스크립트 가져오기
        makeTeaManager = GameObject.FindWithTag("GameController").GetComponent<MakeTeaManager>();
    }

    


}
