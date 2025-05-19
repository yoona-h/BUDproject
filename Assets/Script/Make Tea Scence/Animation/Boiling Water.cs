using System.Collections;
using UnityEngine;

public class BoilingWater : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<IngredientPourer>().enabled = false;
    }

    void OnMouseDown()
    {
        StartCoroutine(PlayBoilingAnim());
    }

    IEnumerator PlayBoilingAnim()
    {
        gameObject.GetComponent<Animator>().SetBool("IsBoil", true);
        yield return new WaitForSeconds(4f);
        gameObject.GetComponent<IngredientPourer>().enabled = true;
    }
}
