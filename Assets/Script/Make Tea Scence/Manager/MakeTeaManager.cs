using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public enum TeaStep { PluckingAndWithering, Oxidation, Firing, Brewing }
public enum Stats { Hope, Courage, Peace, Wisdom, Vitality, Insight }

public class MakeTeaManager : MonoBehaviour
{
    public TeaStep currentStep;
    
    [Header("공통 UI")]
    [SerializeField] GameObject ChangeStep_Screen;
    [SerializeField] GameObject Mask_Screen;
    [SerializeField] GameObject ProcessingCompletedButton;

    [Header("Step 별 오브젝트")]
    [SerializeField] GameObject pluckingAndWitheringObj;
    [SerializeField] GameObject oxidationObj;
    [SerializeField] GameObject firingObjs;
    [SerializeField] GameObject brewingObjs;

    [Header("각 Step 완료 여부")]
    public bool isPluckingAndWitheringFin = false;
    public bool isOxidationFin = false;
    public bool isFiringFin = false;
    public bool isBrewingFin = false;
    public bool isSelectIndredient = false;
    public bool isOilExtraction = false;

    [Header("Tea Info")]
    public Color leafColor;
    public Dictionary<Stats, int> teaEffectDict;

    [Header("차 만들기 정보")]
    public float oxidationTime = 0f;
    public float firingTime = 0f;

    [Header("Inventory")]
    public bool isInLeafInven = false;
    // public bool isInOliInven = false;
    
    void Awake()
    {
        ChangeStep(currentStep);
        Mask_Screen.SetActive(false);
        Reset_ChangeScreen();

        teaEffectDict = new Dictionary<Stats, int>
        {
            { Stats.Hope, 0 },
            { Stats.Courage, 0 },
            { Stats.Peace, 0 },
            { Stats.Wisdom, 0 },
            { Stats.Vitality, 0 },
            { Stats.Insight, 0}
        };

        ProcessingCompletedButton.SetActive(false);
    }

    void Update()
    {
        if(isFiringFin && !ProcessingCompletedButton.activeSelf)
        {
            ProcessingCompletedButton.SetActive(true);
        }
    }

    void ChangeStep(TeaStep teaStep)
    {
        currentStep = teaStep;

        pluckingAndWitheringObj.SetActive(false);
        oxidationObj.SetActive(false);
        firingObjs.SetActive(false);
        brewingObjs.SetActive(false);

        // 차 만들기 단계 따라 오브젝트 켜기
        switch(teaStep)
        {
            case TeaStep.PluckingAndWithering: 
                pluckingAndWitheringObj.SetActive(true); break;
            case TeaStep.Oxidation: 
                oxidationObj.SetActive(true); break;
            case TeaStep.Firing: 
                firingObjs.SetActive(true); break;
            case TeaStep.Brewing: 
                brewingObjs.SetActive(true); break;
        }
    }

    public void SetStepToPlucking() => ChangeStep(TeaStep.PluckingAndWithering);
    public void SetStepToOxidation() => ChangeStep(TeaStep.Oxidation);
    public void SetStepToFiring() => ChangeStep(TeaStep.Firing);
    public void SetStepToBrewing() => ChangeStep(TeaStep.Brewing);

    public void PlayProcessingCompletedAnim()
    {
        
    }

    void Reset_ChangeScreen()
    {
        //ChangeStep_Screen.SetActive(false);
        ChangeStep_Screen.transform.localPosition = new Vector3(2200, 0, 0);
    }

    public IEnumerator ChangeStep()
    {
        

        Vector3 movespeed = new Vector3(4000*Time.deltaTime, 0, 0);

        Mask_Screen.SetActive(true);

        while (ChangeStep_Screen.transform.localPosition.x > 0)
        {
            ChangeStep_Screen.transform.localPosition -= movespeed;
            yield return null;
        }
        
        // GoToNextStep();
        
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

