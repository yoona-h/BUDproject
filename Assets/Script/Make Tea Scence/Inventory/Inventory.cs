using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] GameObject oil;
    
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
        makeTeaManager.isOilExtraction = true;
    }

}
