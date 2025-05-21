using UnityEngine;
using System;
using System.Collections;

public class IngredientPourer : DragAreaChecker
{
    [SerializeField] Transform pouringPos;
    [SerializeField] bool isDestroy = true;

    public event Action FinPouring;

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
        targetArea.GetComponent<Collider2D>().enabled = false;

        isDraggable = false;
        if(pouringPos != null)
        {
            transform.position = pouringPos.position;
        }
        
        animator.SetBool("OnClick", true);
        
        yield return new WaitForSeconds(3.333f);
        
        FinPouring?.Invoke();
        targetArea.GetComponent<Collider2D>().enabled = true;

        if(isDestroy)
        {
            Destroy(gameObject);
        }
        if(isReturn)
        {
            StartCoroutine(MoveToBasePos());
        }
    }
}
