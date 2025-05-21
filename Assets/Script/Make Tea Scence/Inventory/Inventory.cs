using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Leaf")]
    [SerializeField] GameObject leaf;
    [SerializeField] GameObject oxidationTeaDry;
    [SerializeField] GameObject firingTeaDry;

    [Header("Oil")]
    [SerializeField] GameObject oil;
    [SerializeField] GameObject ingredient; // 바구니에 담김 재료 오브젝트
    [SerializeField] Collider2D ingredientWindow; // 재료선택창 영역
    
    MakeTeaManager makeTeaManager;

    void Start()
    {
        // 스크립트 가져오기
        makeTeaManager = GameObject.FindWithTag("GameController").GetComponent<MakeTeaManager>();

        // 초기 세팅
        leaf.SetActive(false);
        oil.SetActive(false);
    }

    public void ExtractOil()
    {
        // 오일 추출 버튼 클릭 시 동작
        oil.SetActive(true);
        ingredient.SetActive(false);
        ingredientWindow.enabled = false;
        makeTeaManager.isOilExtraction = true;
    }

    public void FinishProcessing()
    {
        // 찻잎 가공완료 버튼 클릭 시 동작
        if(makeTeaManager.isInOliInven)
        {
            makeTeaManager.SetStepToBrewing(); // 오일 추출 후면 Plucking 씬으로,
        }
        else
        {
            makeTeaManager.SetStepToPlucking(); // 추출 전이면 Brewing 씬으로 이동
        }

        // 찻잎 인벤토리에 추가
        leaf.GetComponent<SpriteRenderer>().color = makeTeaManager.leafColor; // 잎 색 설정
        leaf.SetActive(true);

        // 가공 단계 잎 오브젝트 비활성화
        oxidationTeaDry.SetActive(false);
        firingTeaDry.SetActive(false);

        makeTeaManager.isInLeafInven = true; // 인벤토리에 잎 추가 true
    }
}