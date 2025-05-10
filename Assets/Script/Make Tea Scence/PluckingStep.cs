using UnityEngine;

/// <summary>
/// 찻잎 영역을 클릭하면 잎을 수확
/// </summary>
public class PluckingStep : MonoBehaviour
{
    [SerializeField] int maxClick = 10;
    [SerializeField] MakeTeaManager makeTeaManager;

    int currentClick;

    void Start()
    {
        currentClick = maxClick;
        GetComponent<WitheringStep>().enabled = false;
    }

    void OnMouseDown() {
        if(currentClick > 0)
        {
            currentClick--;
            // 수확 애니메이션, 사운드 실행
            if(currentClick == 0)
            {
                GetComponent<WitheringStep>().enabled = true;
                makeTeaManager.isPluckingFin = true;
            }
        }
    }
}
