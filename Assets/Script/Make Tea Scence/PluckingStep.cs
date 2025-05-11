using UnityEngine;

/// <summary>
/// 찻잎 영역을 클릭하면 잎을 수확
/// </summary>
public class PluckingStep : MonoBehaviour
{
    [Tooltip("클릭해야 하는 수")]
    [SerializeField] int maxClick = 10;

    [SerializeField] GameObject withering;
    
    MakeTeaManager makeTeaManager;

    int currentClick;

    void OnEnable()
    {
        // 스크립트 가져오기
        if(makeTeaManager == null)
            makeTeaManager = GameObject.FindWithTag("GameController").GetComponent<MakeTeaManager>();
        
        currentClick = maxClick;
        withering.SetActive(false);
    }

    void OnMouseDown() {
        if(currentClick > 0 && !makeTeaManager.isPluckingAndWitheringFin)
        {
            currentClick--;
            Debug.Log("수확");
            // TODO: 수확 애니메이션, 사운드 실행
            if(currentClick == 0)
            {
                withering.SetActive(true);
            }
        }
    }
}
