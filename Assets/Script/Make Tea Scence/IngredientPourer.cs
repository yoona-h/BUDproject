using UnityEngine;
using System;
using System.Collections;

public class IngredientPourer : DragAreaChecker
{
    [SerializeField] Transform pouringPos;

    Animator animator;

    void Start()
    {
        OnEnterArea += PlayAnim;
        animator = GetComponent<Animator>();
        animator.SetBool("OnClick", false);
    }

    void PlayAnim()
    {
        StartCoroutine(AnimateAndDestroy());    
    }

    IEnumerator AnimateAndDestroy()
    {
        isDraggable = false;
        if(pouringPos != null)
        {
            transform.position = pouringPos.position;
        }
        
        animator.SetBool("OnClick", true);
        yield return new WaitForSeconds(3.333f);
        Destroy(gameObject);
    }
}
