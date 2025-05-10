using System.Collections;
using UnityEngine;

public class WitheringStep : MonoBehaviour
{
    [SerializeField] float yPos = -2f;
    [SerializeField] float teaDryMoveTime = 1f;
    [SerializeField] int maxClick;
    int currentClick;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    IEnumerator MoveTeaDry()
    {
        float currentTime = 0f;
        while(currentTime < teaDryMoveTime)
        {
            
            yield return null;
        }
    }
}
