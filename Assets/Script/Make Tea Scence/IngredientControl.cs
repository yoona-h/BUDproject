using UnityEngine;
using UnityEngine.UI;

public class IngredientControl : MonoBehaviour
{
    [SerializeField] GameObject ingredientWindow;
    [SerializeField] GameObject arrows;
    [SerializeField] MakeTeaManager makeTeaManager;

    [SerializeField] GameObject[] ingredients = new GameObject[4];
    [SerializeField] GameObject[] basket = new GameObject[4];
    
    void OnMouseDown()
    {
        ingredientWindow.SetActive(true);
        arrows.SetActive(false);
        transform.parent.GetComponent<Collider2D>().enabled = false;
    }

    public void CloseWindow()
    {
        ingredientWindow.SetActive(false);
        arrows.SetActive(true);
        transform.parent.GetComponent<Collider2D>().enabled = true;
        /*r(int i=0; i<4; i++)
        {
            if(ingredients[i] != null)
                basket[i].GetComponent<SpriteRenderer>().sprite = ingredients[i].GetComponent<Image>().sprite;
        }*/
    }

    public bool AddIngredient(GameObject obj)
    {
        for(int i=0; i<ingredients.Length; i++)
        {
            if(ingredients[i] == null)
            {
                ingredients[i] = obj;
                return true;
            }
        }
        return false;
    }

    public void RemoveIngredient(GameObject obj)
    {
        for(int i=0; i<ingredients.Length; i++)
        {
            if(ingredients[i] == obj)
            {
                ingredients[i] = null;
                // baket[i].GetComponent<SpriteRenderer>().sprite = null;
            }
        }
    }
}
