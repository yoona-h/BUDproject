using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 시들리기, 산화 단계 컨트롤하는 클래스
/// </summary>
public class OxidationStep : MonoBehaviour
{
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

    TMP_Text text;
    Slider progressSlider;

    float processTime = 0f;
    bool isProcessing = false;
    
    void OnEnable()
    {
        if(makeTeaManager == null)
            makeTeaManager = GameObject.FindWithTag("GameController").GetComponent<MakeTeaManager>();
        
        // 수확과 시들리기 완료 여부 따라 오브젝트 세팅
        leafSprite.gameObject.SetActive(makeTeaManager.isPluckingAndWitheringFin);
        talisman.SetActive(makeTeaManager.isPluckingAndWitheringFin);
    }

    void Start()
    {
        // UI 가져오기
        text = transform.GetComponentInChildren<TMP_Text>();
        progressSlider = transform.GetComponentInChildren<Slider>();
        progressSlider.gameObject.SetActive(false);

        // 시작 잎 색깔 가져오기
        makeTeaManager.leafColor = leafSprite.color;

        // 스크립트 가져오기
        dragChecker = talisman.GetComponent<DragAreaChecker>();
        // dragChecker 액션 따라 실행할 함수
        dragChecker.OnEnterArea += StartProcessing;
        dragChecker.OnExitArea += StopProcessing;
    }

    void Update()
    {
        if (isProcessing)
        {
            processTime += Time.deltaTime;
            UpdateLeafColor();
            UpdateProgressBar();
        }
    }

    void StartProcessing()
    {
        if(!makeTeaManager.isFiringFin)
        {
            isProcessing = true;
            progressSlider.gameObject.SetActive(true);
        }
    }

    void StopProcessing()
    {
        isProcessing = false;
        progressSlider.gameObject.SetActive(false);

        makeTeaManager.leafColor = leafSprite.color;
        if(!processTime.Equals(0))
        {
            makeTeaManager.isOxidationFin = true;
            makeTeaManager.oxidationTime = processTime;
        }
    }

    /*TeaData GetOxidationResult(float t)
    {
        if (t < timeThresholds[0]) return new TeaData("황차", "희망 +5, 용기 +2", "노란 빛과 은은한 향. 기분이 좋아지는 차.");
        else if (t < timeThresholds[1]) return new TeaData("우롱차", "용기 +5, 지혜 +2", "노란빛 갈색. 아저씨가 좋아할 맛.");
        else if (t < timeThresholds[2]) return new TeaData("홍차", "지혜 +5, 활력 +2", "붉은 색의 고급스러운 향. 신사의 차.");
        else if (t < timeThresholds[3]) return new TeaData("흑차", "활력 +5, 통찰 +2", "묵은 향과 진한 풍미. 흙냄새까지 품은 차.");
        else return new TeaData("자차", "통찰 +5, 평온 +2", "보라빛과 과일 향. 품종에 따라 천차만별.");
    }*/

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
        if (progressSlider != null && timeThresholds.Length > 0)
        {
            float max = timeThresholds[timeThresholds.Length - 1];
            progressSlider.value = Mathf.Clamp01(processTime / max);
        }
    }
}
