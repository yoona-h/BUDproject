using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OxidationStage : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer leafSprite; // 찻잎
    [SerializeField] private TMP_Text teaInfoText; // 차 정보 텍스트
    [SerializeField] private Slider oxidationSlider; // 산화 바
    [SerializeField] private GameObject talisman; // 부적

    [Header("산화 시간 기준 (seconds)")]
    [SerializeField] private float yellowThreshold = 2f;
    [SerializeField] private float tawnyThreshold = 4f;
    [SerializeField] private float redThreshold = 6f;
    [SerializeField] private float darkThreshold = 8f;
    [SerializeField] private float purpleThreshold = 10f;

    [Header("잎 색깔")]
    [SerializeField] private Color green = new(0.3f, 0.8f, 0.3f);
    [SerializeField] private Color yellow = new(1f, 0.85f, 0.3f);
    [SerializeField] private Color tawny = new(0.8f, 0.6f, 0.2f);
    [SerializeField] private Color red = new(0.6f, 0.2f, 0.2f);
    [SerializeField] private Color dark = new(0.3f, 0.15f, 0.1f);
    [SerializeField] private Color purple = new(0.5f, 0.3f, 0.5f);

    // 산화 관련
    private float oxidationTime = 0f; // 산화 진행 시간
    private bool isOxidizable = true; // 산화 가능 여부

    DragAreaChecker dragAreaChecker;

    void Start()
    {
        // StageManager.Instance.GoToStep(TeaStep.Oxidation);

        dragAreaChecker = talisman.GetComponent<DragAreaChecker>();
        dragAreaChecker.OnExitArea += HandleExitArea;
    }

    void Update()
    {
        if (dragAreaChecker.IsInTargetArea && isOxidizable)
        {
            oxidationTime += Time.deltaTime;
            UpdateLeafColor();
            UpdateUIBar();
        }
    }


    /// <summary>
    /// 산화 시간에 따라 차잎의 색깔 변함
    /// </summary>
    private void UpdateLeafColor()
    {
        if (oxidationTime < yellowThreshold)
            leafSprite.color = Color.Lerp(green, yellow, oxidationTime / yellowThreshold);
        else if (oxidationTime < tawnyThreshold)
            leafSprite.color = Color.Lerp(yellow, tawny, (oxidationTime - yellowThreshold) / (tawnyThreshold - yellowThreshold));
        else if (oxidationTime < redThreshold)
            leafSprite.color = Color.Lerp(tawny, red, (oxidationTime - tawnyThreshold) / (redThreshold - tawnyThreshold));
        else if (oxidationTime < darkThreshold)
            leafSprite.color = Color.Lerp(red, dark, (oxidationTime - redThreshold) / (darkThreshold - redThreshold));
        else if (oxidationTime < purpleThreshold)
            leafSprite.color = Color.Lerp(dark, purple, (oxidationTime - darkThreshold) / (purpleThreshold - darkThreshold));
        else
            leafSprite.color = purple;
    }


    /// <summary>
    /// 산화에 따라 산화 게이지 업데이트
    /// </summary>
    private void UpdateUIBar()
    {
        if (oxidationSlider != null)
        {
            oxidationSlider.value = Mathf.Clamp01(oxidationTime / purpleThreshold);
        }
    }


    /// <summary>
    /// 부적을 치워서 산화가 중단되면 호출됨
    /// </summary>
    private void HandleExitArea()
    {
        isOxidizable = false;
        StopOxidation();
        talisman.SetActive(false);
    }
    private void StopOxidation()
    {
        TeaData teaData = DetermineTeaByTime(oxidationTime);
        // GameManager.Instance.SetOxidation(oxidationTime);
        UpdateTeaInfoUI(teaData);
    }


    /// <summary>
    /// 산화 시간에 따라 차 정보 반환
    /// </summary>
    private TeaData DetermineTeaByTime(float time)
    {
        if (time < yellowThreshold)
            return new TeaData("황차", "희망 +5, 용기 +2", "노란 빛과 은은한 향. 기분이 좋아지는 차.");
        else if (time < tawnyThreshold)
            return new TeaData("우롱차", "용기 +5, 지혜 +2", "노란빛 갈색. 아저씨가 좋아할 맛.");
        else if (time < redThreshold)
            return new TeaData("홍차", "지혜 +5, 활력 +2", "붉은 색의 고급스러운 향. 신사의 차.");
        else if (time < darkThreshold)
            return new TeaData("흑차", "활력 +5, 통찰 +2", "묵은 향과 진한 풍미. 흙냄새까지 품은 차.");
        else if (time < purpleThreshold)
            return new TeaData("자차", "통찰 +5, 평온 +2", "보라빛과 과일 향. 품종에 따라 천차만별.");
        else
            return new TeaData("자차", "통찰 +5, 평온 +2", "보라빛과 과일 향. 품종에 따라 천차만별.");
    }


    /// <summary>
    /// 화면 UI 업데이트 (차 추출 결과 표시)
    /// </summary>
    private void UpdateTeaInfoUI(TeaData tea)
    {
        if (teaInfoText != null)
            teaInfoText.text = $"{tea.name}\n{tea.effect}\n{tea.description}";
    }
    
    /// <summary>
    /// 차 정보 Struct
    /// </summary>
    private struct TeaData
    {
        public string name;
        public string effect;
        public string description;

        public TeaData(string name, string effect, string description)
        {
            this.name = name;
            this.effect = effect;
            this.description = description;
        }
    }
}
