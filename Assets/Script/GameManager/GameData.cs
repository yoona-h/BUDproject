using UnityEngine;
using System;
using System.Collections.Generic;

public class GameData : MonoBehaviour
{
    /*
    - 조사한 내용
    - 손님 정보 (능력치, 특징)
    - 대화 분기
    - 설정 정보
    - 도감 (엔딩, 차 종류, 재료) 
    */

    public static Dictionary<string, string> investigate_contents = new Dictionary<string, string>();//조사 내용 저장 : <조사한 위치, 조사한 내용>
    public static int[] visitor_status = new int[6];//손님 능력치
    public static int talk_branch;//대화 분기
    public static List<bool> ending = new List<bool>();//엔딩 수집 여부

    public static bool FirstGame;



    public static GameData Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
