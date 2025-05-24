using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

/// <summary>
/// 찻잎 영역을 클릭하면 잎을 수확
/// </summary>
public class PluckingStep : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject text;
    [SerializeField] Slider progressSlider;

    [Header("클릭 수")]
    [SerializeField] int maxClick = 10;

    public event Action pluckingFin;
    
    MakeTeaManager makeTeaManager;

    int currentClick;

    void Start()
    {
        // 스크립트 가져오기
        if(makeTeaManager == null)
            makeTeaManager = GameObject.FindWithTag("GameController").GetComponent<MakeTeaManager>();
        
        // 초기 세팅
        currentClick = 0;
        progressSlider.gameObject.SetActive(false);
    }

    void OnMouseUpAsButton() {
        if(EventSystem.current.IsPointerOverGameObject()) return;

        text.SetActive(false);

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
                GetComponent<Collider2D>().enabled = false;
                pluckingFin?.Invoke();
            }
        }
    }

    
}
