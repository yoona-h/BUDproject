using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 찻잎 시들리기 단계
/// </summary>
public class WitheringStep : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject text;
    [SerializeField] GameObject back;

    [Header("클릭 수")]
    [SerializeField] int maxClick;

    [Header("Tea Dry 애니메이션 관련")]
    [Tooltip("시작 위치")]
    [SerializeField] Transform startPos;
    [Tooltip("움직일 위치")]
    [SerializeField] Transform endPos;
    [Tooltip("움직일 시간 (초)")]
    [SerializeField] float teaDryMoveTime = 1f;

    [Tooltip("시들리기 완료 후 딜레이 시간 (초)")]
    [SerializeField] float waitTime = 1f;

    [Header("찻잎 오브젝트")]
    [SerializeField] GameObject[] leafs;

    [Header("재료 영역 콜라이더")]
    [SerializeField] Collider2D ingredientCollider;
    [SerializeField] Collider2D basket;
    [SerializeField] GameObject arrows;
    [SerializeField] GameObject jokja;
    
    
    [SerializeField] PluckingStep pluckingStep;
    MakeTeaManager makeTeaManager;
    
    
    int currentClick;

    void Start()
    {
        // 스크립트 가져오기
        if(makeTeaManager == null)
            makeTeaManager = GameObject.FindWithTag("GameController").GetComponent<MakeTeaManager>();
        
        // 초기 세팅
        text.SetActive(false);
        pluckingStep.pluckingFin += startWithering;
    }

    void startWithering()
    {
        currentClick = maxClick;

        leafs[0].SetActive(true);
        leafs[1].SetActive(false);
        arrows.SetActive(false);
        jokja.SetActive(false);
        basket.enabled = false;
        ingredientCollider.enabled = false;
        
        back.SetActive(true);
        StartCoroutine(MoveTeaDry(endPos.position, 0));
    }

    /// <summary>
    /// 찻잎 영역 클릭 시 실행, 모두 클릭하면 장소 이동 가능
    /// </summary>
    void OnMouseUpAsButton()
    {
        if(currentClick > 0) 
        {
            currentClick--;
            Debug.Log("건조");
            // 건조 애니메이션, 사운드 실행
            if(currentClick == 0)
            {
                leafs[0].SetActive(false);
                leafs[1].SetActive(true); // 마르고 부서진 찻잎 스프라이트
                makeTeaManager.isPluckingAndWitheringFin = true;
                text.SetActive(false);
                StartCoroutine(MoveTeaDry(startPos.position, waitTime));
            }
        }
    }

    /// <summary>
    /// Tea Dry 움직이는 애니메이션
    /// </summary>
    IEnumerator MoveTeaDry(Vector2 endPos, float waitTime)
    {
        Vector2 currentPos = transform.position;
        float currentTime = 0f;
        yield return new WaitForSeconds(waitTime);

        while(currentTime < teaDryMoveTime)
        {
            transform.position = Vector2.Lerp(currentPos, endPos, currentTime/teaDryMoveTime);
            currentTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;

        bool fin = makeTeaManager.isPluckingAndWitheringFin;
        ingredientCollider.enabled = fin && !makeTeaManager.isOilExtraction;
        arrows.SetActive(fin);
        jokja.SetActive(fin);
        basket.enabled = fin;

        text.SetActive(!fin);
        back.SetActive(!fin);
    }

    void OnDisable()
    {
        transform.position = startPos.position;
    }
}
