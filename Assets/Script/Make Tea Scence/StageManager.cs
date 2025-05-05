using UnityEngine;

public enum TeaStep { Harvest, /*Select,*/ Withering, Oxidation, Roasting, Brewing }
//------------------------------------------메모(김건우) : 근데 재료선택과정이 왜 필요하지??? 차 우리는 과정이랑 재료선택은 별개의 과정 아닌가? '추가재료'잖아...

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    public TeaStep currentStep = TeaStep.Oxidation;
    //초기화를 3단계부터 시작? 테스트하려고 그랬던거겠지???

    [SerializeField] private GameObject harvestStage;
    //[SerializeField] private GameObject selectStage;
    [SerializeField] private GameObject witheringStage;
    [SerializeField] private GameObject oxidationStage;
    [SerializeField] private GameObject roastingStage;
    [SerializeField] private GameObject brewingStage;
    [SerializeField] private Canvas canvas;

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
            case TeaStep.Oxidation: oxidationStage.SetActive(true); break;
            case TeaStep.Roasting: roastingStage.SetActive(true); break;
            case TeaStep.Brewing: brewingStage.SetActive(true); canvas.enabled = false; break;
        }

    }

    public void GoToNextStep()
    {
        int next = (int)currentStep + 1;
        if (next < System.Enum.GetValues(typeof(TeaStep)).Length)
            GoToStep((TeaStep)next);
    }

    public void SetOxidizedLeafColor(Color leafColor)
    {
        this.leafColor = leafColor;
    }
}

