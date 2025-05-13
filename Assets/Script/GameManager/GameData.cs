using UnityEngine;
using System;
using System.Collections.Generic;

public class GameData : MonoBehaviour
{
    //�� ���̿��� ���޵Ǵ� ������
    public Dictionary<string, string> investigate_contents = new Dictionary<string, string>();//���� ���� ���� : <������ ��ġ, ������ ����>
    public int[] visitor_status = new int[6];//�մ� �ɷ�ġ
    public int talk_branch;//��ȭ �б�


    //���� ���� �Ŀ��� ����Ǵ� ������
    public List<bool> ending = new List<bool>();//���� ���� ����
    public bool FirstGame = true;//������ ó�� �����ߴ��� ����
    public int stage = 0;//���� ����� ��������. �������� �ϳ� �������� ���� 1�� ������Ű��

    //���� ������
    public float EffectSound_Volume = 1f;//ȿ���� ����
    public float BackGroundMusic_Volume = 1f;//������� ����
    public int SpeakSpeed = 3;//��ȭ �ӵ� ����
    public bool SpeakingAuto = false;//��ȭ �ڵ� �ѱ�� ���� ����
    public bool EasyMode = false;//������� ��� ����


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
