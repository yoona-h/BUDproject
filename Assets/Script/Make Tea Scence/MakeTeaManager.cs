using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public enum TeaStep { Plucking, WitheringAndOxidation, Firing, Brewing}

public class MakeTeaManager : MonoBehaviour
{
    public TeaStep currentStep;
    
    [Header("UI")]
    public Button brewingButton;
    public GameObject ChangeStep_Screen;
    public GameObject Mask_Screen;

    [Header("Step Objects")]
    public GameObject pluckingObjs;
    public GameObject witheringAndOxidation;
    public GameObject firingObjs;
    public GameObject brewingObjs;

    public Color leafColor;

    void Start()
    {
        ChangeStep(currentStep);
        Mask_Screen.SetActive(false);
        Reset_ChangeScreen();
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

    private void Reset_ChangeScreen()
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

