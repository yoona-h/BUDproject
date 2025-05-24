using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IngredientControl : MonoBehaviour
{
    [SerializeField] GameObject ingredientWindow;
    [SerializeField] GameObject arrows;
    [SerializeField] GameObject[] ingredients = new GameObject[4]; // 선택된 재료 저장 배열
    [SerializeField] GameObject[] basket = new GameObject[4]; // 바구니에 담긴 재료 배열

    MakeTeaManager makeTeaManager;

    int selectNum = 0;

    void Start()
    {
        // 스크립트 가져오기
        makeTeaManager = GameObject.FindWithTag("GameController").GetComponent<MakeTeaManager>();
    }

    void Update()
    {
        makeTeaManager.isSelectIndredient = selectNum > 0;
    }

    void OnMouseUpAsButton()
    {
        if(EventSystem.current.IsPointerOverGameObject()) return;
      
        ingredientWindow.SetActive(true);
        arrows.SetActive(false);
        GetComponent<Collider2D>().enabled = false;
        transform.parent.GetComponent<Collider2D>().enabled = false;
    }

    public void CloseWindow()
    {
        ingredientWindow.SetActive(false);
        arrows.SetActive(true);
        GetComponent<Collider2D>().enabled = true;
        transform.parent.GetComponent<Collider2D>().enabled = true;
        for (int i=0; i<4; i++)
        {
            if(ingredients[i] != null)
                basket[i].GetComponent<SpriteRenderer>().sprite = ingredients[i].GetComponent<Image>().sprite;
        }
    }

    public bool AddIngredient(GameObject obj)
    {
        for(int i=0; i<ingredients.Length; i++)
        {
            if(ingredients[i] == null)
            {
                ingredients[i] = obj;
                selectNum++;
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
                basket[i].GetComponent<SpriteRenderer>().sprite = null;
                selectNum--;
            }
        }
    }
}
