using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System;
using Unity.VisualScripting;

/// <summary>
/// 시들리기, 산화 단계 컨트롤하는 클래스
/// </summary>
public class OxidationStep : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] TMP_Text text;
    [SerializeField] TMP_Text timerText;
    [SerializeField] GameObject timer;

    [Header("Tea Dry")]
    [SerializeField] GameObject teaDry;

    [Header("찻잎")]
    [SerializeField] SpriteRenderer leafSprite;

    [Header("색 변화 기준 시간 (초)")]
    [SerializeField] float[] timeThresholds;

    [Header("색깔 단계별 설정")] 
    [SerializeField] Color[] leafColors;
    [Header("부적")]
    [SerializeField] GameObject talisman;

    MakeTeaManager makeTeaManager;
    DragAreaChecker dragChecker;

    

    float processTime = 0f;
    bool isProcessing = false;
    
    void OnEnable()
    {
        if(makeTeaManager == null)
            makeTeaManager = GameObject.FindWithTag("GameController").GetComponent<MakeTeaManager>();
        
        // 수확과 시들리기 완료 여부 따라 오브젝트 세팅
        bool isReady = makeTeaManager.isPluckingAndWitheringFin;
        leafSprite.gameObject.SetActive(isReady);
        talisman.SetActive(isReady);
        if(isReady)
        {
            teaDry.GetComponent<Animator>().SetBool("isReady", true);
        }
    }

    void Start()
    {
        // 시작 잎 색깔 가져오기
        makeTeaManager.leafColor = leafSprite.color;

        // 스크립트 가져오기
        dragChecker = talisman.GetComponent<DragAreaChecker>();
        // dragChecker 액션 따라 실행할 함수
        dragChecker.OnEnterArea += StartProcessing;
        dragChecker.OnExitArea += StopProcessing;

        timer.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isProcessing)
        {
            processTime += Time.deltaTime;
            UpdateLeafColor();
            UpdateProgressBar();
        }

        if(makeTeaManager.isFiringFin)
            talisman.GetComponent<Collider2D>().enabled = false;
    }

    void StartProcessing()
    {
        if(!makeTeaManager.isFiringFin)
        {
            isProcessing = true;
            text.gameObject.SetActive(false);
            timer.gameObject.SetActive(true);
        }
    }

    void StopProcessing()
    {
        isProcessing = false;
        // timer.gameObject.SetActive(false);
        text.gameObject.SetActive(true);
        text.text = GetOxidationResult(processTime);

        makeTeaManager.leafColor = leafSprite.color;
        if(!processTime.Equals(0))
        {
            makeTeaManager.isOxidationFin = true;
        }
        SaveTeaInfo(processTime);
    }

    String GetOxidationResult(float t)
    {
        if(t.Equals(0)) return "녹차: 평온, 희망 증가";
        else if (t < timeThresholds[0]) return "황차: 희망, 용기 증가";
        else if (t < timeThresholds[1]) return "우롱차: 용기, 지혜 증가";
        else if (t < timeThresholds[2]) return "홍차: 지혜, 활력 증가";
        else if (t < timeThresholds[3]) return "흑차: 활력, 통찰 증가";
        else return "자차: 통찰, 평온 증가";
    }

    void SaveTeaInfo(float t)
    {
        if(t.Equals(0))
        {
            makeTeaManager.teaName = TeaName.녹차;
            makeTeaManager.teaEffectDict[Stats.Peace] += 5;
            makeTeaManager.teaEffectDict[Stats.Hope] += 2;
        }
        else if (t < timeThresholds[0])
        {
            makeTeaManager.teaName = TeaName.황차;
            makeTeaManager.teaEffectDict[Stats.Hope] += 5;
            makeTeaManager.teaEffectDict[Stats.Courage] += 2;
        }
        else if (t < timeThresholds[1])
        {
            makeTeaManager.teaName = TeaName.우롱차;
            makeTeaManager.teaEffectDict[Stats.Courage] += 5;
            makeTeaManager.teaEffectDict[Stats.Wisdom] += 2;
        }
        else if (t < timeThresholds[2])
        {
            makeTeaManager.teaName = TeaName.홍차;
            makeTeaManager.teaEffectDict[Stats.Wisdom] += 5;
            makeTeaManager.teaEffectDict[Stats.Vitality] += 2;
        }
        else if (t < timeThresholds[3])
        {
            makeTeaManager.teaName = TeaName.흑차;
            makeTeaManager.teaEffectDict[Stats.Vitality] += 5;
            makeTeaManager.teaEffectDict[Stats.Insight] += 2;
        }
        else
        {
            makeTeaManager.teaName = TeaName.자차;
            makeTeaManager.teaEffectDict[Stats.Insight] += 5;
            makeTeaManager.teaEffectDict[Stats.Peace] += 2;
        }
    }

    void UpdateLeafColor()
    {
        if (leafColors.Length < 2 || timeThresholds.Length < 1) return;

        for (int i = 0; i < timeThresholds.Length; i++)
        {
            if (processTime < timeThresholds[i])
            {
                Color from = leafColors[i];
                Color to = leafColors[Mathf.Min(i + 1, leafColors.Length - 1)];
                float t = i == 0 ? processTime / timeThresholds[0] : (processTime - timeThresholds[i - 1]) / (timeThresholds[i] - timeThresholds[i - 1]);
                leafSprite.color = Color.Lerp(from, to, t);
                return;
            }
        }

        // 마지막 색상으로 고정
        leafSprite.color = leafColors[leafColors.Length - 1];
    }

    void UpdateProgressBar()
    {
        /*if (progressSlider != null && timeThresholds.Length > 0)
        {
            float max = timeThresholds[timeThresholds.Length - 1];
            progressSlider.value = Mathf.Clamp01(processTime / max);
        }*/
        int max = (int)timeThresholds[timeThresholds.Length - 1];
        int time = (int)processTime;
        if(time <= max)
            timerText.text = $"{time}";
    }

    void OnDisable()
    {
        teaDry.GetComponent<Animator>().SetBool("isReady", false);
    }
}
