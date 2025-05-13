using Unity.VisualScripting;
using UnityEngine;

public class TalismanAnim : MonoBehaviour
{
    Animator animator;
    DragAreaChecker dragAreaChecker;
    
    void Start()
    {
        dragAreaChecker = GetComponent<DragAreaChecker>();
        animator = GetComponent<Animator>();
        animator.SetBool("OnClick", false);
    }

    void OnMouseDown()
    {
        if(!dragAreaChecker.isInArea)
        {
            animator.SetBool("OnClick", true);
        }        
    }
    void OnMouseUp()
    {
        animator.SetBool("OnClick", false);
    }   
}
