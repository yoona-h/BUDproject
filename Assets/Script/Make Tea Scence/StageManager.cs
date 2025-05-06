using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public enum TeaStep { Harvest, /*Select,*/ Withering, Oxidation, Roasting, Brewing }
//------------------------------------------메모(김건우) : 근데 재료선택과정이 왜 필요하지??? 차 우리는 과정이랑 재료선택은 별개의 과정 아닌가? '추가재료'잖아...

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    public TeaStep currentStep;

    [Header("Stages")]
    [SerializeField] private GameObject harvestStage;
    //[SerializeField] private GameObject selectStage;
    [SerializeField] private GameObject witheringStage;
    [SerializeField] private GameObject oxidationStage;
    [SerializeField] private GameObject roastingStage;
    [SerializeField] private GameObject brewingStage;

    [Header("UI")]
    [SerializeField] private Button nextstage_button;
    [SerializeField] private GameObject ChangeStep_Screen;
    [SerializeField] private GameObject Mask_Screen;//화면전환 도중 클릭을 방지하기 위한 마스크 스크린

    public Color leafColor {get; private set;}

    //------------------------------------------메모(김건우) : 이거는 싱글톤패턴인데 여기서 쓴 이유는??? DontDestroyed 오브젝트도 아니잖아.
    //그리고 instance에 이 스크립트 넣어두면은 나중에 손님 한 명 받고나서 다음 손님 받을 때 이전 차 우리는 과정이 남아있을 수도 있음(static)... 그냥 씬 초기화할 대 같이 초기화하는게 좋을듯
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        Mask_Screen.SetActive(false);
        Reset_ChangeScreen();
        GoToStep(currentStep);
    }

    public void GoToStep(TeaStep step)
    {
        currentStep = step;

        harvestStage.SetActive(false);
        witheringStage.SetActive(false);
        oxidationStage.SetActive(false);
        roastingStage.SetActive(false);
        brewingStage.SetActive(false);

        switch (step)
        {
            case TeaStep.Harvest: harvestStage.SetActive(true); break;
            case TeaStep.Withering: witheringStage.SetActive(true); break;
            case TeaStep.Oxidation: oxidationStage.SetActive(true); break;
            case TeaStep.Roasting: roastingStage.SetActive(true); break;
            case TeaStep.Brewing: brewingStage.SetActive(true);
                nextstage_button.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                nextstage_button.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                break;
        }

    }

    public void GotoNextStep_Button()
    {
        if (currentStep != TeaStep.Brewing)
            StartCoroutine(ChangeStep());
        else
        {
            //차 우리기 완료되었을 때 버튼 클릭시

        }
    }

    public void GoToNextStep()
    {
        int next = (int)currentStep + 1;
        if (next < Enum.GetValues(typeof(TeaStep)).Length)
            GoToStep((TeaStep)next);
    }

    public void SetOxidizedLeafColor(Color leafColor)
    {
        this.leafColor = leafColor;
    }


    private void Reset_ChangeScreen()
    {
        //ChangeStep_Screen.SetActive(false);
        ChangeStep_Screen.transform.localPosition = new Vector3(2200, 0, 0);
    }
    private IEnumerator ChangeStep()
    {
        Vector3 movespeed = new Vector3(4000*Time.deltaTime, 0, 0);

        Mask_Screen.SetActive(true);

        while (ChangeStep_Screen.transform.localPosition.x > 0)
        {
            ChangeStep_Screen.transform.localPosition -= movespeed;
            yield return null;
        }
        
        GoToNextStep();
        
        while (ChangeStep_Screen.transform.localPosition.x > -2200)
        {
            ChangeStep_Screen.transform.localPosition -= movespeed;
            yield return null;
        }

        Mask_Screen.SetActive(false);
        Reset_ChangeScreen();
        
        yield break;
    }
}

