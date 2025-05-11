using UnityEngine;

public class BrewingStep : MonoBehaviour
{
    [Header("오브젝트")]
    [SerializeField] GameObject gaiwan;
    [SerializeField] GameObject teaPot;

    [SerializeField] float xPos;

    MakeTeaManager makeTeaManager;
    int ingredientCnt;

    void Start()
    {
        // 스크립트 가져오기
        makeTeaManager = GameObject.FindWithTag("GameController").GetComponent<MakeTeaManager>();
        
        teaPot.SetActive(false);
        //StageManager.Instance.Cntis3 += TeaPotAni;
    }

    void TeaPotAni()
    {
        gaiwan.SetActive(true);
        teaPot.SetActive(true);
        gaiwan.transform.position += new Vector3(xPos, 0, 0);
        // TODO: 개완 흔들기 애니메이션
        gaiwan.GetComponent<IngredientPourer>().enabled = true;
    }

    public void SetIngredientCnt()
    {
        ingredientCnt++;
    }
}
