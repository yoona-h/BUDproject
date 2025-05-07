using UnityEngine;

public class BrewingStage : MonoBehaviour
{
    [Header("오브젝트")]
    [SerializeField] private GameObject gaiwan;
    [SerializeField] private GameObject teaPot;

    [SerializeField] private float xPos;

    private int ingredientCnt;

    void Start()
    {
        teaPot.SetActive(false);
        //StageManager.Instance.Cntis3 += TeaPotAni;
    }

    void Update()
    {
        
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
