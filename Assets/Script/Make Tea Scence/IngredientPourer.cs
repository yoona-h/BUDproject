using UnityEngine;
using System;
using System.Collections;

public class IngredientPourer : DragAreaChecker
{
    [Header("애니메이션 실행")]
    [SerializeField] Animator animator;

    [Header("애니메이션 트리거 이름")]
    [SerializeField] string animationTrigger = "Trigger";

    BrewingStep brewingStep;

    void Start()
    {
        brewingStep = transform.parent.GetComponent<BrewingStep>();
        OnEnterArea += HandleOnEnterArea;
    }

    void HandleOnEnterArea()
    {
        // 애니메이션 실행
        if (animator != null)
        {
            brewingStep.ingredientCnt++;
            StartCoroutine(AnimateAndDestroy());
        }

        // 애니메이션 실행 후 물체 삭제
        Destroy(gameObject);
    }

    IEnumerator AnimateAndDestroy()
    {
        // 애니메이션 실행
        if (animator != null)
        {
            animator.SetTrigger(animationTrigger);
        }

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        Destroy(gameObject);
    }

    void OnDestroy()
    {
        // 이벤트 구독 해제
        OnEnterArea -= HandleOnEnterArea;
    }
}
