using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 화살표 클릭시 배경 전환을 담당하는 클래스
/// </summary>
public class BackgroundSwitcher : MonoBehaviour
{
    [Header("BackGround")]
    public GameObject workbenchBG; // 작업대
    public GameObject storageBG; // 재료 창고
    public GameObject furnaceBG; // 아궁이
    public GameObject barBG; // 손님에게 차 제공할 바

    [Header("Arrows")]
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject upArrow;
    public GameObject downArrow;
    
    MakeTeaManager makeTeaManager;

    void Start()
    {
        makeTeaManager = GetComponent<MakeTeaManager>();

        // 현재 차 만들기 단계에 맞게 세팅
        ChangeBackground(makeTeaManager.currentStep);
    }

    /// <summary>
    /// 클릭한 화살표 화면 따라 배경 전환
    /// </summary>
    public void OnArrowClick(string direction)
    {
        TeaStep teaStep = makeTeaManager.currentStep;

        if(direction.Equals("left"))
        {
            if(teaStep == TeaStep.Firing)
            {
                ChangeBackground(TeaStep.WitheringAndOxidation);
            }
            else
            {
                ChangeBackground(TeaStep.Plucking);
            }
        }
        else if(direction.Equals("right"))
        {
            if(teaStep == TeaStep.Plucking)
            {
                ChangeBackground(TeaStep.WitheringAndOxidation);
            }
            else
            {
                ChangeBackground(TeaStep.Firing);
            }
        }
        else if(direction.Equals("up"))
        {
            ChangeBackground(TeaStep.Firing);
        }
        else
        {
            ChangeBackground(TeaStep.Brewing);
        }
    }

    /// <summary>
    /// teaStep으로 배경 전환
    /// </summary>
    void ChangeBackground(TeaStep teaStep)
    {
        // 모든 항목 끄기
        workbenchBG.SetActive(false);
        storageBG.SetActive(false);
        furnaceBG.SetActive(false);
        barBG.SetActive(false);

        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
        upArrow.SetActive(false);
        downArrow.SetActive(false);

        // 차 만들기 단계 따라 배경과 화살표 켜기
        switch(teaStep)
        {
            case TeaStep.Plucking: 
                storageBG.SetActive(true);
                rightArrow.SetActive(true);
                break;
            case TeaStep.WitheringAndOxidation: 
                workbenchBG.SetActive(true);
                leftArrow.SetActive(true); rightArrow.SetActive(true); 
                break;
            case TeaStep.Firing: 
                furnaceBG.SetActive(true);
                leftArrow.SetActive(true); downArrow.SetActive(true);
                break;
            case TeaStep.Brewing: 
                barBG.SetActive(true);
                upArrow.SetActive(true);
                break;
        }

        makeTeaManager.ChangeStep(teaStep);
    }
}
