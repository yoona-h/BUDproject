using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

/// <summary>
/// 덖기 단계 컨트롤하는 클래스
/// </summary>
public class FiringStep : MonoBehaviour
{   
    [Header("UI")]
    [SerializeField] TMP_Text text;
    [SerializeField] TMP_Text TimerText;
    [SerializeField] GameObject timer;
    [SerializeField] Button button;

    [Header("찻잎")]
    [SerializeField] SpriteRenderer leafSprite;

    [Header("덖기 기준 시간 (초)")]
    [SerializeField] float[] timeThresholds;

    [Header("구름 파티클")]
    [SerializeField] ParticleSystem smokeEffect;

    [Header("Tea Dry")]
    [SerializeField] GameObject teaDry;

    MakeTeaManager makeTeaManager;
    DragAreaChecker dragAreaChecker;

    float processTime = 0f;
    bool isProcessing = false;

    void OnEnable()
    {
        // 스크립트 가져오기
        if(makeTeaManager == null)
            makeTeaManager = GameObject.FindWithTag("GameController").GetComponent<MakeTeaManager>();

        // 잎 색깔 설정
        bool isReady = makeTeaManager.isPluckingAndWitheringFin;
        leafSprite.gameObject.SetActive(isReady);
        leafSprite.color = makeTeaManager.leafColor;
        if(isReady)
        {
            teaDry.GetComponent<Animator>().SetBool("isReady", true);
        }

        if (smokeEffect != null && smokeEffect.isPlaying)
            smokeEffect.Stop();
    }

    void Start()
    {
        // 스크립트 가져오기
        dragAreaChecker = leafSprite.GetComponent<DragAreaChecker>();
        // dragChecker 액션 따라 실행할 함수
        dragAreaChecker.OnEnterArea += StartProcessing;
        dragAreaChecker.OnExitArea += StopProcessing;

        timer.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isProcessing)
        {
            processTime += Time.deltaTime;
            UpdateProgressBar();
        }
    }

    void StartProcessing()
    {
        isProcessing = true;
        timer.gameObject.SetActive(true);
        if (smokeEffect != null && !smokeEffect.isPlaying)
            smokeEffect.Play();

    }

    void StopProcessing()
    {
        isProcessing = false;
        timer.gameObject.SetActive(false);
        text.gameObject.SetActive(true);
        text.text = GetFiringResult(processTime);

        if (smokeEffect != null && smokeEffect.isPlaying)
            smokeEffect.Stop();

        if(!processTime.Equals(0))
        {
            makeTeaManager.isFiringFin = true;
            makeTeaManager.firingTime = processTime;
            button.gameObject.SetActive(true);
        }
    }

    String GetFiringResult(float t)
    {
        if(t.Equals(0)) return "녹차: 평온, 희망 증가";
        else if (t < timeThresholds[0]) return "황차: 희망, 용기 증가";
        else if (t < timeThresholds[1]) return "우롱차: 용기, 지혜 증가";
        else if (t < timeThresholds[2]) return "홍차: 지혜, 활력 증가";
        else if (t < timeThresholds[3]) return "흑차: 활력, 통찰 증가";
        else return "자차: 통찰, 평온 증가";
    }

    void UpdateProgressBar()
    {
        /*if (timer != null && timeThresholds.Length > 0)
        {
            float max = timeThresholds[timeThresholds.Length - 1];
            timer.value = Mathf.Clamp01(processTime / max);
        }*/
        int max = (int)timeThresholds[timeThresholds.Length - 1];
        int time = (int)processTime;
        if(time <= max)
            TimerText.text = $"{time}";
    }

    void OnDisable()
    {
        teaDry.GetComponent<Animator>().SetBool("isReady", false);
    }
}
