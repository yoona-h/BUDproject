using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 산화/덖기의 공통적인 단계를 처리하는 베이스 클래스
/// </summary>
public class LeafProcessingStage : MonoBehaviour
{
    [Header("공통 컴포넌트")]
    [SerializeField] protected SpriteRenderer leafSprite;
    [SerializeField] protected TMP_Text teaInfoText;
    [SerializeField] protected Slider progressSlider;

    [Header("색 변화 기준 시간 (초)")]
    [SerializeField] protected float[] timeThresholds;

    [Header("색깔 단계별 설정")] 
    [SerializeField] protected Color[] leafColors;

    protected float processTime = 0f;
    protected bool isProcessing = false;

    protected virtual void Update()
    {
        if (isProcessing)
        {
            processTime += Time.deltaTime;
            UpdateLeafColor();
            UpdateProgressBar();
        }
    }

    public virtual void StartProcessing()
    {
        processTime = 0f;
        isProcessing = true;
    }

    public virtual void StopProcessing()
    {
        isProcessing = false;
    }

    protected virtual void UpdateLeafColor()
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

    protected virtual void UpdateProgressBar()
    {
        if (progressSlider != null && timeThresholds.Length > 0)
        {
            float max = timeThresholds[timeThresholds.Length - 1];
            progressSlider.value = Mathf.Clamp01(processTime / max);
        }
    }

    protected virtual void DisplayTeaInfo(string name, string effect, string description)
    {
        if (teaInfoText != null)
            teaInfoText.text = $"{name}\n{effect}\n{description}";
    }

    public float GetProcessTime() => processTime;
    public bool IsProcessing() => isProcessing;
}