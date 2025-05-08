using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public enum TeaStep { Plucking, WitheringAndOxidation, Firing, Brewing }
public enum Stats { Hope, Courage, Peace, Wisdom, Vitality, Insight }

public class MakeTeaManager : MonoBehaviour
{
    public TeaStep currentStep;
    
    [Header("공통 UI")]
    public GameObject ChangeStep_Screen;
    public GameObject Mask_Screen;

    [Header("Step 별 오브젝트")]
    public GameObject pluckingObjs;
    public GameObject witheringAndOxidation;
    public GameObject firingObjs;
    public GameObject brewingObjs;

    [Header("각 Step 완료 여부")]
    public bool isPluckingFin = false;
    public bool isWitheringFin = false;
    public bool isOxidationFin = false;
    public bool isFiringFin = false;
    public bool isBrewingFin = false;

    [Header("Tea Info")]
    public Color leafColor;
    public Dictionary<Stats, int> teaEffectDict;
    
    void Start()
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
    }

    public void ChangeStep(TeaStep teaStep)
    {
        currentStep = teaStep;

        pluckingObjs.SetActive(false);
        witheringAndOxidation.SetActive(false);
        firingObjs.SetActive(false);
        brewingObjs.SetActive(false);

        // 차 만들기 단계 따라 오브젝트 켜기
        switch(teaStep)
        {
            case TeaStep.Plucking: 
                pluckingObjs.SetActive(true); break;
            case TeaStep.WitheringAndOxidation: 
                witheringAndOxidation.SetActive(true); break;
            case TeaStep.Firing: 
                firingObjs.SetActive(true); break;
            case TeaStep.Brewing: 
                brewingObjs.SetActive(true); break;
        }
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

