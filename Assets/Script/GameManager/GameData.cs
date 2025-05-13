using UnityEngine;
using System;
using System.Collections.Generic;

public class GameData : MonoBehaviour
{
    //씬 사이에서 전달되는 변수들
    public Dictionary<string, string> investigate_contents = new Dictionary<string, string>();//조사 내용 저장 : <조사한 위치, 조사한 내용>
    public int[] visitor_status = new int[6];//손님 능력치
    public int talk_branch;//대화 분기


    //게임 종료 후에도 저장되는 변수들
    public List<bool> ending = new List<bool>();//엔딩 수집 여부
    public bool FirstGame = true;//게임을 처음 시작했는지 여부
    public int stage = 0;//현재 진행된 스테이지. 스테이지 하나 엔딩으로 보면 1씩 증가시키기

    //각종 설정들
    public float EffectSound_Volume = 1f;//효과음 음량
    public float BackGroundMusic_Volume = 1f;//배경음학 음량
    public int SpeakSpeed = 3;//대화 속도 설정
    public bool SpeakingAuto = false;//대화 자동 넘기기 설정 여부
    public bool EasyMode = false;//이지모드 사용 여부


    public static GameData Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        LoadData();
    }

    public void SaveData()
    {

    }

    public void LoadData()
    {

    }
}
