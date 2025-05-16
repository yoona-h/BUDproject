using UnityEngine;
using System;
using UnityEngine.Timeline;
using System.Collections;

/// <summary>
/// 드래그 가능한 물체가 대상 영역 안에 있는지 체크
/// </summary>
public class DragAreaChecker : MonoBehaviour
{
    [Header("대상 영역 (Collider2D)")]
    [SerializeField] Collider2D targetArea;

    [Header("드래그 가능 여부")]
    public bool isDraggable = true;

    [Header("사용 후 제자리로")]
    public bool isReturn = true;
    [SerializeField] bool PlayAnim = false;
    [SerializeField] Transform baseTransform; // null이면 현재 위치로 설정됨
    [SerializeField] float duration = 0.3f;


    public event Action OnEnterArea;
    public event Action OnExitArea;
    public bool isInArea;

    bool isDragging = false;
    
    Vector3 offset;
    Vector2 basePos;

    void Awake()
    {
        if(baseTransform == null)
        {
            basePos = transform.position;
        }
        else
        {
            basePos = baseTransform.position;
        }
        
    }

    void Update()
    {
        if (!isDraggable) return;

        isInArea = targetArea.bounds.Contains(transform.position);

        if (Input.GetMouseButtonDown(0))
        {
            if(isInArea)
            {
                OnExitArea?.Invoke();
                if(isReturn)
                {
                    if(!PlayAnim)
                        transform.position = basePos;
                    else
                        StartCoroutine(MoveToBasePos());
                }
                return;
            }

            Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (GetComponent<Collider2D>().OverlapPoint(mouseWorld))
            {
                isDragging = true;
                offset = transform.position - (Vector3)mouseWorld;
            }
        }

        if (isDragging && Input.GetMouseButton(0))
        {
            Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mouseWorld + (Vector2)offset;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            CheckAreaState();
        }
    }

    void CheckAreaState()
    {
        if(isInArea)
        {
            OnEnterArea?.Invoke();
        }
        else
        {
            StartCoroutine(MoveToBasePos());
        }
    }

    /// <summary>
    /// 물체가 제자리로 돌아가는 애니메이션
    /// </summary>
    public IEnumerator MoveToBasePos()
    {
        Vector2 startPosition = transform.position;
        float elapsedTime = 0f;

        isDraggable = false;

        while (elapsedTime < duration)
        {
            transform.position = Vector2.Lerp(startPosition, basePos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        transform.position = basePos;
        isDraggable = true;
    }
}
