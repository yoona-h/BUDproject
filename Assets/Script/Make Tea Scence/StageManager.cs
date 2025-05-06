using System;
using UnityEngine;

public enum TeaStep { Harvest, /*Select,*/ Withering, Oxidation, Roasting, Brewing }
//------------------------------------------�޸�(��ǿ�) : �ٵ� ��ἱ�ð����� �� �ʿ�����??? �� �츮�� �����̶� ��ἱ���� ������ ���� �ƴѰ�? '�߰����'�ݾ�...

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    public TeaStep currentStep = TeaStep.Oxidation;
    //�ʱ�ȭ�� 3�ܰ���� ����? �׽�Ʈ�Ϸ��� �׷����Ű���???

    [SerializeField] private GameObject harvestStage;
    //[SerializeField] private GameObject selectStage;
    [SerializeField] private GameObject witheringStage;
    [SerializeField] private GameObject oxidationStage;
    [SerializeField] private GameObject roastingStage;
    [SerializeField] private GameObject brewingStage;
    [SerializeField] private Canvas canvas;

    public Color leafColor {get; private set;}
    public int ingredientCnt{get; private set;} = 0;
    public event Action Cntis3;

    //------------------------------------------�޸�(��ǿ�) : �̰Ŵ� �̱��������ε� ���⼭ �� ������??? DontDestroyed ������Ʈ�� �ƴ��ݾ�.
    //�׸��� instance�� �� ��ũ��Ʈ �־�θ��� ���߿� �մ� �� �� �ް����� ���� �մ� ���� �� ���� �� �츮�� ������ �������� ���� ����(static)... �׳� �� �ʱ�ȭ�� �� ���� �ʱ�ȭ�ϴ°� ������
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

    public void SetIngredientCnt()
    {
        ingredientCnt++;
        if(ingredientCnt == 3) Cntis3?.Invoke();
    }
}

