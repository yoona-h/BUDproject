using UnityEngine;
using System;
using System.Collections.Generic;

public class GameData : MonoBehaviour
{
    /*
    - ������ ����
    - �մ� ���� (�ɷ�ġ, Ư¡)
    - ��ȭ �б�
    - ���� ����
    - ���� (����, �� ����, ���) 
    */

    public static Dictionary<string, string> investigate_contents = new Dictionary<string, string>();//���� ���� ���� : <������ ��ġ, ������ ����>
    public static int[] visitor_status = new int[6];//�մ� �ɷ�ġ
    public static int talk_branch;//��ȭ �б�
    public static List<bool> ending = new List<bool>();//���� ���� ����

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
