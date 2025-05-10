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
    [SerializeField]Collider2D targetArea;

    [Header("드래그 가능 여부")]
    [SerializeField]bool isDraggable = true;

    public bool IsInTargetArea { get; private set; }

    public event Action OnEnterArea;
    public event Action OnExitArea;

    [Header("제자리로 돌아가는 시간")]
    [SerializeField] float duration = 0.3f;

    bool isDragging = false;
    Vector3 offset;
    Vector2 basePos;
    
    //bool wasInTargetArea = false;

    bool isInArea;

    void Start()
    {
        basePos = transform.position;
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
                StartCoroutine(MoveToBasePos());
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
            OnExitArea?.Invoke();
            StartCoroutine(MoveToBasePos());
        }
    }

    IEnumerator MoveToBasePos()
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

    /*void CheckAreaState()
    {
        bool isNowInArea = targetArea.bounds.Contains(transform.position);

        if (!wasInTargetArea && isNowInArea)
        {
            OnEnterArea?.Invoke(); // 진입 시 호출
        }
        else if (wasInTargetArea && !isNowInArea)
        {
            OnExitArea?.Invoke(); // 이탈 시 호출
            transform.position = basePos;
        }

        IsInTargetArea = isNowInArea;
        wasInTargetArea = isNowInArea;
    }*/
}
