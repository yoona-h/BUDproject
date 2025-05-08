using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public enum TeaStep { Plucking, WitheringAndOxidation, Firing, Brewing}

public class MakeTeaManager : MonoBehaviour
{
    public TeaStep currentStep;
    
    [Header("UI")]
    [SerializeField] private Button nextStep_button;
    [SerializeField] private GameObject ChangeStep_Screen;
    [SerializeField] private GameObject Mask_Screen;
    public Color leafColor {get; private set;}

    private void Start()
    {
        Mask_Screen.SetActive(false);
        Reset_ChangeScreen();
        // ChangeLocation(currentStep);
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
    public IEnumerator ChangeStep(TeaStep teaStep)
    {
        currentStep = teaStep;

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

