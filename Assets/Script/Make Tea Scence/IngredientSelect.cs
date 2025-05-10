using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using System.Data.Common;

public class IngredientSelect : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    [SerializeField] Image ingredientImage;
    [SerializeField] Color selectedColor;
    [SerializeField] TMP_Text titleText;
    [SerializeField] TMP_Text descriptionText;
    [SerializeField] string title;
    [SerializeField] string descriptions;
    
    [SerializeField] IngredientControl ingredientControl;

    bool isSelected = false;
    Color baseColor;

    void Start()
    {
        baseColor = GetComponent<Image>().color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isSelected = !isSelected;
        if(isSelected)
        {
            if(ingredientControl.addIngredient(gameObject))
            {
                GetComponent<Image>().color = selectedColor;
            }
        }
        else
        {
            ingredientControl.removeIngredient(gameObject);
            GetComponent<Image>().color = baseColor;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ingredientImage.sprite = GetComponent<Image>().sprite;
        descriptionText.text = descriptions;
        titleText.text = title;
    }
}
