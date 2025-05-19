using System.Collections;
using UnityEngine;

public class Oil : MonoBehaviour
{
    [SerializeField] Transform oilInven;
    [SerializeField] float duration;

    MakeTeaManager makeTeaManager;

    void Start()
    {
        // 스크립트 가져오기
        makeTeaManager = GameObject.FindWithTag("GameController").GetComponent<MakeTeaManager>();
    }

    void OnMouseUpAsButton() 
    {
        StartCoroutine(MoveToUIPos());
    }

    IEnumerator MoveToUIPos()
    {
        Vector2 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position = Vector2.Lerp(startPosition, oilInven.transform.position, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        // transform.position = oilInven.transform.position;
        oilInven.gameObject.SetActive(true);
        makeTeaManager.isInOliInven = true;
        Destroy(gameObject);
    }
}
