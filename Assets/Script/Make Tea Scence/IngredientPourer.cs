using UnityEngine;
using System.Collections;
using System;

public class IngredientPourer : MonoBehaviour
{
    [Header("대상 오브젝트")]
    [SerializeField] private Transform obj; // 기준 위치 오브젝트
    [SerializeField] private float moveDuration = 0.5f; // 애니메이션 시간

    private bool isPouring = false;

    void OnMouseDown()
    {
        if (!isPouring && gameObject.activeInHierarchy)
        {
            StartCoroutine(MoveToGaiwan());
        }
    }

    IEnumerator MoveToGaiwan()
    {
        isPouring = true;

        Vector3 startPos = transform.position;
        Vector3 endPos = obj.position;

        float elapsed = 0f;
        while (elapsed < moveDuration)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsed / moveDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;

        // 애니메이션 완료 후 사라지거나 상태 변경
        OnPoured();
    }

    void OnPoured()
    {
        this.gameObject.SetActive(false);
        StageManager.Instance.SetIngredientCnt();
    }
}
