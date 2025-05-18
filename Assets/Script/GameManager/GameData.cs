using UnityEngine;
using System;
using System.Collections.Generic;

public class GameData : MonoBehaviour
{
    //씬 사이에서 전달되는 변수들
    [HideInInspector] public Dictionary<string, string> investigate_contents = new Dictionary<string, string>();//조사 내용 저장 : <조사한 위치, 조사한 내용>
    [HideInInspector] public int[] visitor_status = new int[6];//손님 능력치
    [HideInInspector] public int talk_branch;//대화 분기


    //게임 종료 후에도 저장되는 변수들
    [HideInInspector] public List<bool[]> ending = new List<bool[]>();//엔딩 수집 여부 (어떤 엔딩을 봤는지도 저장하기), 이야기가 어디까지 진행되었는지도 해당 변수로 판단할 예정.
    [HideInInspector] public bool FirstGame = true;//게임을 처음 시작했는지 여부
    [HideInInspector] public int stage = 0;//현재 진행된 스테이지. 스테이지 하나 엔딩으로 보면 1씩 증가시키기

    //각종 설정들
    [HideInInspector] public float EffectSound_Volume = 1f;//효과음 음량 
    [HideInInspector] public float BackGroundMusic_Volume = 1f;//배경음학 음량
    [HideInInspector] public int SpeakSpeed = 3;//대화 속도 설정
    [HideInInspector] public bool SpeakingAuto = false;//대화 자동 넘기기 설정 여부
    [HideInInspector] public bool EasyMode = false;//이지모드 사용 여부


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


    public void Reset_Data()//게임전체를 아예 다 초기화시키기 (첫 시작시 변수 초기화 용도)
    {
        FirstGame = true;
    }

    //이 아래는 gpt가 해줌...
    public void SaveData()
    {
        // Dictionary<string, string> -> InvestigateData(JSON)
        InvestigateData invData = new InvestigateData();
        foreach (var pair in investigate_contents)
        {
            invData.keys.Add(pair.Key);
            invData.values.Add(pair.Value);
        }
        PlayerPrefs.SetString("investigate_contents", JsonUtility.ToJson(invData));

        // visitor_status
        for (int i = 0; i < visitor_status.Length; i++)
            PlayerPrefs.SetInt($"visitor_status_{i}", visitor_status[i]);

        PlayerPrefs.SetInt("talk_branch", talk_branch);

        // ending (List<bool[]>를 List<List<bool>>으로 변환 후 직렬화)
        EndingData endData = new EndingData();
        foreach (var arr in ending)
            endData.endings.Add(new List<bool>(arr));
        PlayerPrefs.SetString("ending", JsonUtility.ToJson(endData));

        PlayerPrefs.SetInt("FirstGame", FirstGame ? 1 : 0);
        PlayerPrefs.SetInt("stage", stage);

        PlayerPrefs.SetFloat("EffectSound_Volume", EffectSound_Volume);
        PlayerPrefs.SetFloat("BackGroundMusic_Volume", BackGroundMusic_Volume);
        PlayerPrefs.SetInt("SpeakSpeed", SpeakSpeed);
        PlayerPrefs.SetInt("SpeakingAuto", SpeakingAuto ? 1 : 0);
        PlayerPrefs.SetInt("EasyMode", EasyMode ? 1 : 0);

        PlayerPrefs.Save(); // 반드시 저장
    }
    public void LoadData()
    {
        // Dictionary
        investigate_contents.Clear();
        string invJson = PlayerPrefs.GetString("investigate_contents", "");
        if (!string.IsNullOrEmpty(invJson))
        {
            InvestigateData invData = JsonUtility.FromJson<InvestigateData>(invJson);
            for (int i = 0; i < invData.keys.Count; i++)
            {
                investigate_contents[invData.keys[i]] = invData.values[i];
            }
        }

        // visitor_status
        for (int i = 0; i < visitor_status.Length; i++)
            visitor_status[i] = PlayerPrefs.GetInt($"visitor_status_{i}", 0);

        talk_branch = PlayerPrefs.GetInt("talk_branch", 0);

        // ending
        ending.Clear();
        string endJson = PlayerPrefs.GetString("ending", "");
        if (!string.IsNullOrEmpty(endJson))
        {
            EndingData endData = JsonUtility.FromJson<EndingData>(endJson);
            foreach (var list in endData.endings)
                ending.Add(list.ToArray());
        }

        FirstGame = PlayerPrefs.GetInt("FirstGame", 1) == 1;
        stage = PlayerPrefs.GetInt("stage", 0);

        EffectSound_Volume = PlayerPrefs.GetFloat("EffectSound_Volume", 1f);
        BackGroundMusic_Volume = PlayerPrefs.GetFloat("BackGroundMusic_Volume", 1f);
        SpeakSpeed = PlayerPrefs.GetInt("SpeakSpeed", 3);
        SpeakingAuto = PlayerPrefs.GetInt("SpeakingAuto", 0) == 1;
        EasyMode = PlayerPrefs.GetInt("EasyMode", 0) == 1;
    }
}
[Serializable]
public class InvestigateData
{
    public List<string> keys = new List<string>();
    public List<string> values = new List<string>();
}

[Serializable]
public class EndingData
{
    public List<List<bool>> endings = new List<List<bool>>();
}
