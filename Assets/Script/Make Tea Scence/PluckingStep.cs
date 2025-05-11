using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 찻잎 영역을 클릭하면 잎을 수확
/// </summary>
public class PluckingStep : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] TMP_Text text;
    [SerializeField] Slider progressSlider;

    [Header("클릭 수")]
    [SerializeField] int maxClick = 10;

    [Header("withering 오브젝트")]
    [SerializeField] GameObject withering;
    
    MakeTeaManager makeTeaManager;

    int currentClick;

    void OnEnable()
    {
        // 스크립트 가져오기
        if(makeTeaManager == null)
            makeTeaManager = GameObject.FindWithTag("GameController").GetComponent<MakeTeaManager>();
        
        currentClick = 0;

        withering.SetActive(false);
        progressSlider.gameObject.SetActive(false);
    }

    void OnMouseDown() {
        if(currentClick < maxClick && !makeTeaManager.isPluckingAndWitheringFin)
        {
            Debug.Log("수확");
            currentClick++;
            // TODO: 수확 애니메이션, 사운드 실행

            progressSlider.gameObject.SetActive(true);
            progressSlider.value = (float)currentClick / maxClick;

            if(currentClick == maxClick)
            {
                progressSlider.gameObject.SetActive(false);
                text.enabled = false;
                withering.SetActive(true);
            }
        }
    }
}
