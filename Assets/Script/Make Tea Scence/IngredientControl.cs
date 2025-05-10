using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class IngredientControl : MonoBehaviour
{
    [SerializeField] GameObject ingredientWindow;
    [SerializeField] GameObject arrows;
    [SerializeField] MakeTeaManager makeTeaManager;

    [SerializeField] GameObject[] ingredients = new GameObject[4];
    
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
    }

    public bool addIngredient(GameObject obj)
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

    public void removeIngredient(GameObject obj)
    {
        for(int i=0; i<ingredients.Length; i++)
        {
            if(ingredients[i] == obj)
            {
                ingredients[i] = null;
            }
        }
    }
}
