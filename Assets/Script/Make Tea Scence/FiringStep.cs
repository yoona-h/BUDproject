using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 덖기 단계 컨트롤하는 클래스
/// </summary>
public class FiringStep : MonoBehaviour
{   
    [Header("UI")]
    [SerializeField] TMP_Text text;
    [SerializeField] Slider progressSlider;

    [Header("찻잎")]
    [SerializeField] SpriteRenderer leafSprite;

    [Header("덖기 기준 시간 (초)")]
    [SerializeField] float[] timeThresholds;

    [Header("연기 파티클")]
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

        progressSlider.gameObject.SetActive(false);
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

    void OnDisable()
    {
        teaDry.GetComponent<Animator>().SetBool("isReady", false);
    }
}
