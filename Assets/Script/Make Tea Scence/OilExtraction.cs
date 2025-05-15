using UnityEngine;

public class OilExtraction : MonoBehaviour
{
    [SerializeField] GameObject oilSprite;
    [SerializeField] GameObject arrows;
    [SerializeField] Collider2D ingredientColi;
    [SerializeField] Collider2D pluckingColi;

    void Start()
    {
        oilSprite.SetActive(false);
        gameObject.SetActive(false);
    }

    public void ExtractOil()
    {
        oilSprite.SetActive(true);
    }

    public void OpenWindow()
    {
        ingredientColi.enabled = false;
        pluckingColi.enabled = false;
        arrows.SetActive(false);
        gameObject.SetActive(true);
    }

    public void CloseWindow()
    {
        ingredientColi.enabled = true;
        pluckingColi.enabled = true;
        arrows.SetActive(true);
        gameObject.SetActive(false);
    }
}
