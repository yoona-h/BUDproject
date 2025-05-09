using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 덖가 단계 컨트롤하는 클래스
/// </summary>
public class FiringStage : MonoBehaviour
{   
    [SerializeField] SpriteRenderer leafSprite;
    [SerializeField] TMP_Text text;
    [SerializeField] Slider progressSlider;

    [Header("덖기 기준 시간 (초)")]
    [SerializeField] protected float[] timeThresholds;

    [Header("연기 파티클")]
    [SerializeField] private ParticleSystem smokeEffect;
    
    [SerializeField] MakeTeaManager makeTeaManager;
    DragAreaChecker dragAreaChecker;

    float processTime = 0f;
    bool isProcessing = false;
    Vector2 leafPos;

    void OnEnable()
    {
        // 잎 색깔 설정
        leafPos = leafSprite.transform.position;
        leafSprite.color = makeTeaManager.leafColor;

        if (smokeEffect != null && smokeEffect.isPlaying)
            smokeEffect.Stop();
    }

    void Start()
    {
        dragAreaChecker = leafSprite.GetComponent<DragAreaChecker>();
        dragAreaChecker.OnEnterArea += StartProcessing;
        dragAreaChecker.OnExitArea += StopProcessing;
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

        if (smokeEffect != null && !smokeEffect.isPlaying)
            smokeEffect.Play();

        makeTeaManager.firingTime = processTime;
    }

    void StopProcessing()
    {
        isProcessing = false;

        Debug.Log($"덖기 종료, 시간: {processTime:F2}s");

        // DisplayTeaInfo("이름", "추가된 효과", "설명");

        if (smokeEffect != null && smokeEffect.isPlaying)
            smokeEffect.Stop();

        //dragAreaChecker.enabled = false;
        //leafSprite.transform.position = leafPos;
        Color finalRoastingColor = leafSprite.color;
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
