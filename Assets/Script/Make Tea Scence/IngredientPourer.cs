using UnityEngine;
using System;
using System.Collections;

public class IngredientPourer : MonoBehaviour
{
    [SerializeField] Transform pouringPos;

    Animator animator;
    DragAreaChecker dragAreaChecker;

    void Start()
    {
        dragAreaChecker = GetComponent<DragAreaChecker>();
        dragAreaChecker.OnEnterArea += PlayAnim;
        animator = GetComponent<Animator>();
        animator.SetBool("OnClick", false);
    }

    void PlayAnim()
    {
        StartCoroutine(AnimateAndDestroy());    
    }

    IEnumerator AnimateAndDestroy()
    {
        dragAreaChecker.enabled = false;
        if(pouringPos != null)
        {
            transform.position = pouringPos.position;
        }
        
        animator.SetBool("OnClick", true);
        yield return new WaitForSeconds(3.333f);
        Destroy(gameObject);
    }
}
