using System.Collections;
using UnityEngine;

public class BoilingWater : MonoBehaviour
{
    [SerializeField] ParticleSystem smokeEffect;

    void OnEnable()
    {
        if (smokeEffect != null && smokeEffect.isPlaying)
            smokeEffect.Stop();
    }

    void Start()
    {
        // 초기 세팅
        gameObject.GetComponent<IngredientPourer>().enabled = false;
    }

    void OnMouseUpAsButton()
    {
        // 파티클, 애니메이션 재생
        if(!gameObject.GetComponent<IngredientPourer>().isActiveAndEnabled)
            smokeEffect.Play();
        StartCoroutine(PlayBoilingAnim());
    }

    IEnumerator PlayBoilingAnim()
    {
        // 애니메이션 재생 후 파티클 중지
        gameObject.GetComponent<Animator>().SetBool("IsBoil", true);
        yield return new WaitForSeconds(4f);
        gameObject.GetComponent<IngredientPourer>().enabled = true;
        smokeEffect.Stop();
    }
}