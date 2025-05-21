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

    MakeTeaManager makeTeaManager;
    
    void Start()
    {
        // 스크립트 가져오기
        makeTeaManager = GameObject.FindWithTag("GameController").GetComponent<MakeTeaManager>();
        
        // 이벤트 연결결
        boilingWater.GetComponent<IngredientPourer>().FinPouring += PlusWater;
        leaf.GetComponent<IngredientPourer>().FinPouring += PlusLeaf;
        oil.GetComponent<IngredientPourer>().FinPouring += PlusOil;
        teaPot.GetComponent<IngredientPourer>().FinPouring += FinBrewwing;
        teaCup.GetComponent<DragAreaChecker>().OnEnterArea += SwitchScene;

        // 초기 세팅
        teaPot.GetComponent<IngredientPourer>().enabled = false;
        teaCup.GetComponent<DragAreaChecker>().enabled = false;
    }

    void PlusWater()
    {
        teaPot.GetComponent<IngredientPourer>().enabled = true; // teapot 드래그 가능해짐
    }

    void PlusLeaf()
    {
        // TODO: 게임 데이터에 잎 정보 보내기
    }

    void PlusOil()
    {
        // TODO: 게임 데이터에 오일 정보 보내기
    }

    void FinBrewwing()
    {
        makeTeaManager.isBrewingFin = true;
        teaPot.GetComponent<Collider2D>().enabled = false;
        teaCup.GetComponent<DragAreaChecker>().enabled = true;
        Debug.Log("차를 완성했습니다~");
    }

    void SwitchScene()
    {
        // TODO: 다음씬으로 전환
    }
}
