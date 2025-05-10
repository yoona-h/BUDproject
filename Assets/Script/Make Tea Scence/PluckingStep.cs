using UnityEngine;

/// <summary>
/// 찻잎 영역을 클릭하면 잎을 수확
/// </summary>
public class PluckingStep : MonoBehaviour
{
    [Tooltip("클릭해야 하는 수")]
    [SerializeField] int maxClick = 10;
    
    [Header("Scripts")]
    [SerializeField] MakeTeaManager makeTeaManager;
    [SerializeField] WitheringStep witheringStep;

    int currentClick;

    void OnEnable()
    {
        currentClick = maxClick;
        witheringStep.enabled = false;
    }

    void OnMouseDown() {
        if(currentClick > 0 && !makeTeaManager.isPluckingAndWitheringFin)
        {
            currentClick--;
            Debug.Log("수확");
            // 수확 애니메이션, 사운드 실행
            if(currentClick == 0)
            {
                witheringStep.enabled = true;
            }
        }
    }
}
