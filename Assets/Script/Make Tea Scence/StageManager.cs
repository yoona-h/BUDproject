using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public enum TeaStep { Harvest, /*Select,*/ Withering, Oxidation, Roasting, Brewing }
//------------------------------------------�޸�(��ǿ�) : �ٵ� ��ἱ�ð����� �� �ʿ�����??? �� �츮�� �����̶� ��ἱ���� ������ ���� �ƴѰ�? '�߰����'�ݾ�...

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
    [SerializeField] private GameObject Mask_Screen;//ȭ����ȯ ���� Ŭ���� �����ϱ� ���� ����ũ ��ũ��

    public Color leafColor {get; private set;}

    //------------------------------------------�޸�(��ǿ�) : �̰Ŵ� �̱��������ε� ���⼭ �� ������??? DontDestroyed ������Ʈ�� �ƴ��ݾ�.
    //�׸��� instance�� �� ��ũ��Ʈ �־�θ��� ���߿� �մ� �� �� �ް����� ���� �մ� ���� �� ���� �� �츮�� ������ �������� ���� ����(static)... �׳� �� �ʱ�ȭ�� �� ���� �ʱ�ȭ�ϴ°� ������
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
            //�� �츮�� �Ϸ�Ǿ��� �� ��ư Ŭ����

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

