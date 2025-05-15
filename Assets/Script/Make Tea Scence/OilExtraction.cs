using UnityEngine;

public class OilExtraction : MonoBehaviour
{
    [SerializeField] GameObject oilSprite;

    void Start()
    {
        oilSprite.SetActive(false);
        gameObject.SetActive(false);
    }

    public void ExtractOil()
    {
        oilSprite.SetActive(true);
    }
}
