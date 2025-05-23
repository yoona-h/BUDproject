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
        StartCoroutine(MoveToUIPos());
    }

    IEnumerator MoveToUIPos()
    {
        Vector2 startPosition = transform.position;
        float elapsedTime = 0f;

        yield return new WaitForSeconds(0.5f);

        while (elapsedTime < duration)
        {
            transform.position = Vector2.Lerp(startPosition, oilInven.transform.position, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        oilInven.gameObject.SetActive(true);
        Destroy(gameObject);
    }
}
