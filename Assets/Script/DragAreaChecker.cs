using UnityEngine;
using System;

public class DragAreaChecker : MonoBehaviour
{
    [Header("대상 영역 (Collider2D)")]
    [SerializeField] private Collider2D targetArea;

    [Header("드래그 가능 여부")]
    [SerializeField] private bool isDraggable = true;

    public bool IsInTargetArea { get; private set; }

    public event Action OnEnterArea;
    public event Action OnExitArea;

    private bool isDragging = false;
    private Vector3 offset;
    
    private bool wasInTargetArea = false;

    void Update()
    {
        if (!isDraggable) return;

        if (Input.GetMouseButtonDown(0))
        {
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

            CheckAreaState();
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            CheckAreaState();
        }
    }

    void CheckAreaState()
    {
        bool isNowInArea = targetArea.bounds.Contains(transform.position);

        if (!wasInTargetArea && isNowInArea)
        {
            OnEnterArea?.Invoke(); // 진입 시 호출
        }
        else if (wasInTargetArea && !isNowInArea)
        {
            OnExitArea?.Invoke(); // 이탈 시 호출
        }

        IsInTargetArea = isNowInArea;
        wasInTargetArea = isNowInArea;
    }
}
