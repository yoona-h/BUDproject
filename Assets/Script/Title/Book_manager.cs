using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class Book_manager : MonoBehaviour
{
    /*
     *�մ� �ϳ��� �߰��ϸ� �ؾ��� ��
     1. �ش� �մ��� �� ȭ�� ������ �����
     2. ���� ȭ���� ��ư�� EndingSelect_button ������ �߰��ϱ�
     3. visitor_image�� �մ� �̹��� �߰��ϱ�
     4. visitor_title�� �մ� �̸� �߰��ϱ�
     5. visotor_ending�� �մ� ���� ���� �߰��ϱ�
     */

    [Header("�մ����� ����� UI")]
    public Image VisitorEmage_emage;
    public TMP_Text VisitorTitle_text;
    public TMP_Text[] VisitorEnding_text;
    [Space(20)]
    public GameObject WarningScreen;
    public List<Button> EndingSelect_button = new List<Button>();
    [Space]
    public Animator animator;
    [Space(20)]
    [Tooltip("�ٽ��� �� �Ѿ �� �̸� �����ϱ�. (� �մ��� �����ߴ����� GameData���� stage������ ������ ����.")]
    public string Scene_name;
    [HideInInspector] public int SelectedScene = 0;

    [Space(30)]
    [Tooltip("�մ� �̹��� ������ ����. ��� �̹����� ũ��� �����ϰ� ������.")]
    public List<Sprite> visitor_image = new List<Sprite>();
    [HideInInspector]public List<string> visitor_title = new List<string>();
    public List<string[]> visitor_ending = new List<string[]>();
    void visitor_Data()//�մ԰� ���� ������ ����
    {
        if (visitor_title == null || visitor_title.Count == 0)
        {
            visitor_title = new List<string>() {
            "�մ�1", "�մ�2", "�մ�3", "�մ�4", "�մ�5", "�մ�6", "�մ�7", "�մ�8", "�մ�9"
        };
        }
        visitor_ending.Add(new string[] { "����1", "����2", "����3", "����4", "���� ����1", " ���� ����2" });//�մ�1
        visitor_ending.Add(new string[] { "����1", "����2", "����3", "����4", "���� ����1", " ���� ����2" });//�մ�2
        visitor_ending.Add(new string[] { "����1", "����2", "����3", "����4", "���� ����1", " ���� ����2" });//�մ�3
        visitor_ending.Add(new string[] { "����1", "����2", "����3", "����4", "���� ����1", " ���� ����2" });//�մ�4
        visitor_ending.Add(new string[] { "����1", "����2", "����3", "����4", "���� ����1", " ���� ����2" });//�մ�5
        visitor_ending.Add(new string[] { "����1", "����2", "����3", "����4", "���� ����1", " ���� ����2" });//�մ�6
        visitor_ending.Add(new string[] { "����1", "����2", "����3", "����4", "���� ����1", " ���� ����2" });//�մ�7
        visitor_ending.Add(new string[] { "����1", "����2", "����3", "����4", "���� ����1", " ���� ����2" });//�մ�8
        visitor_ending.Add(new string[] { "����1", "����2", "����3", "����4", "���� ����1", " ���� ����2" });//�մ�9
    }



    private void Awake()
    {
        //�� �� �̻� Ŭ�����ߴ� �մԸ� �ٽ��� �� �ְ� ����
        for (int i = 0; i < GameData.Instance.ending.Count; i++)
        {
            if (GameData.Instance.ending[i])
                EndingSelect_button[i].interactable = true;
            else
                EndingSelect_button[i].interactable = false;
        }
        visitor_Data();
    }
    private void OnEnable()
    {
        WarningScreen.transform.GetChild(0).gameObject.SetActive(false);
        WarningScreen.transform.GetChild(1).gameObject.SetActive(false);
    }


    public void Start_StroyScene()
    {
        GameData.Instance.stage = SelectedScene;
        SceneManager.LoadScene(Scene_name);
    }

    public void select_stroy(int selectstory_number)
    {
        SelectedScene = selectstory_number-1;
        //�� �մԿ� ���� ȭ�鿡 ��� '�׸�, �մ� �ؽ�Ʈ, ���� �ؽ�Ʈ' �����ϱ�
        VisitorEmage_emage.sprite = visitor_image[SelectedScene];
        VisitorTitle_text.text = visitor_title[SelectedScene];
        for (int i = 0; i < VisitorEnding_text.Length; i++)
        {
            VisitorEnding_text[i].text = visitor_ending[SelectedScene][i];
        }
    }

    public void OpenAndClose_WarningScreen()
    {
        //ȭ���� ���������� Ű��, ���������� ����

        if (WarningScreen.transform.GetChild(0).gameObject.activeSelf)//ȭ���� �������� ��
        {
            WarningScreen.transform.GetChild(0).gameObject.SetActive(false);
            WarningScreen.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            WarningScreen.transform.GetChild(0).gameObject.SetActive(true);
            WarningScreen.transform.GetChild(1).gameObject.SetActive(true);
            animator.SetTrigger("Open");
        }
    }

    public void mousedown()
    {
        animator.SetTrigger("Close");
    }
}
