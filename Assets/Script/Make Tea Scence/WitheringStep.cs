using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 찻잎 시들리기 단계
/// </summary>
public class WitheringStep : MonoBehaviour
{
    [Tooltip("클릭해야 하는 수")]
    [SerializeField] int maxClick;

    [Header("Tea Dry 애니메이션 관련")]
    [Tooltip("시작 위치")]
    [SerializeField] Vector2 startPos;
    [Tooltip("움직일 y축 offset")]
    [SerializeField] float offset = 7f;
    [Tooltip("움직일 시간 (초)")]
    [SerializeField] float teaDryMoveTime = 1f;
    [Tooltip("시들리기 완료 후 딜레이 시간 (초)")]
    [SerializeField] float waitTime = 1f;

    [Header("찻잎 오브젝트")]
    [SerializeField] GameObject[] leafs;
    
    MakeTeaManager makeTeaManager;
    GameObject arrows;
    Collider2D ingredientCollider;
    int currentClick;

    void OnEnable()
    {
        // 스크립트 가져오기
        if(makeTeaManager == null)
            makeTeaManager = GameObject.FindWithTag("GameController").GetComponent<MakeTeaManager>();
        // 화살표 버튼 가져오기
        if(arrows == null)
            arrows = GameObject.Find("MoveArrow");
        // 재료 영역 콜라이더 가져오기
        if(ingredientCollider == null)
            ingredientCollider = transform.parent.GetComponentInChildren<IngredientControl>().gameObject.GetComponent<Collider2D>();

        if(!makeTeaManager.isPluckingAndWitheringFin)
        {
            currentClick = maxClick;
            leafs[0].SetActive(true);
            leafs[1].SetActive(false);
            arrows.SetActive(false);
            ingredientCollider.enabled = false;
            StartCoroutine(MoveTeaDry(startPos + new Vector2(0,offset), 0));
        }
    }

    void OnDisable()
    {
        transform.position = startPos;
        leafs[0].SetActive(true);
        leafs[1].SetActive(false);
    }

    /// <summary>
    /// 찻잎 영역 클릭 시 실행, 모두 클릭하면 장소 이동 가능
    /// </summary>
    void OnMouseDown()
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
                
                StartCoroutine(MoveTeaDry(startPos, waitTime));
            }
        }
    }

    /// <summary>
    /// 아래쪽에서 Tea Dry 올라오는 애니메이션
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

        ingredientCollider.enabled = true;
        arrows.SetActive(makeTeaManager.isPluckingAndWitheringFin);
    }
}
