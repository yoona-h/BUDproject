using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 시들리기, 산화 단계 컨트롤하는 클래스
/// </summary>
public class OxidationStage : MonoBehaviour
{
    [SerializeField] SpriteRenderer leafSprite;
    [SerializeField] TMP_Text text;
    [SerializeField] Slider progressSlider;

    [Header("색 변화 기준 시간 (초)")]
    [SerializeField] float[] timeThresholds;

    [Header("색깔 단계별 설정")] 
    [SerializeField] Color[] leafColors;

    [SerializeField] GameObject talisman;

    [SerializeField] MakeTeaManager makeTeaManager;
    DragAreaChecker dragChecker;

    float processTime = 0f;
    bool isProcessing = false;
    Vector2 talismanPos;

    void Start()
    {
        dragChecker = talisman.GetComponent<DragAreaChecker>();
        dragChecker.OnEnterArea += StartProcessing;
        dragChecker.OnExitArea += StopProcessing;

        makeTeaManager.leafColor = leafSprite.color;
        talismanPos = talisman.transform.position;
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
        isProcessing = true;
    }

    void StopProcessing()
    {
        isProcessing = false;

        Debug.Log($"산화 종료, 시간: {processTime:F2}s");

        //TeaData tea = GetOxidationResult(processTime);
        //DisplayTeaInfo(tea.name, tea.effect, tea.description);

        //talisman.SetActive(false);
        //talisman.transform.position = talismanPos;
        makeTeaManager.leafColor = leafSprite.color;

        //MakeTeaManager.Instance.SetOxidizedLeafColor(leafSprite.color);
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
