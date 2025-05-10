using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 덖기 단계 컨트롤하는 클래스
/// </summary>
public class FiringStep : MonoBehaviour
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

    void Start()
    {
        dragAreaChecker = leafSprite.GetComponent<DragAreaChecker>();
        dragAreaChecker.OnEnterArea += StartProcessing;
        dragAreaChecker.OnExitArea += StopProcessing;
    }

    void OnEnable()
    {
        leafSprite.gameObject.SetActive(makeTeaManager.isPluckingAndWitheringFin);

        // 잎 색깔 설정
        leafSprite.color = makeTeaManager.leafColor;

        if (smokeEffect != null && smokeEffect.isPlaying)
            smokeEffect.Stop();
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
        progressSlider.gameObject.SetActive(true);
        if (smokeEffect != null && !smokeEffect.isPlaying)
            smokeEffect.Play();

    }

    void StopProcessing()
    {
        isProcessing = false;
        progressSlider.gameObject.SetActive(false);

        if (smokeEffect != null && smokeEffect.isPlaying)
            smokeEffect.Stop();

        if(!processTime.Equals(0))
        {
            makeTeaManager.isFiringFin = true;
            makeTeaManager.firingTime = processTime;
        }
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
