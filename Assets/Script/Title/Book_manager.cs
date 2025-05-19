using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class Book_manager : MonoBehaviour
{
    /*
     *손님 하나를 추가하면 해야할 일
     1. 해당 손님이 들어갈 화면 씬에서 만들기
     2. 만든 화면의 버튼을 EndingSelect_button 변수에 추가하기
     3. visitor_image에 손님 이미지 추가하기
     4. visitor_title에 손님 이름 추가하기
     5. visotor_ending에 손님 엔딩 설명 추가하기
     */

    [Header("손님정보 출력할 UI")]
    public Image VisitorEmage_emage;
    public TMP_Text VisitorTitle_text;
    public TMP_Text[] VisitorEnding_text;
    [Space(20)]
    public GameObject WarningScreen;
    public List<Button> EndingSelect_button = new List<Button>();
    [Space(20)]
    [Tooltip("다시할 때 넘어갈 씬 이름 지정하기. (어떤 손님을 선택했는지는 GameData에서 stage변수에 지정할 예정.")]
    public string Scene_name;
    [HideInInspector] public int SelectedScene = 0;

    [Space(30)]
    [Tooltip("손님 이미지 저장할 변수. 모든 이미지의 크기는 일정하게 맞추자.")]
    public List<Sprite> visitor_image = new List<Sprite>();
    [HideInInspector]public List<string> visitor_title = new List<string>();
    public List<string[]> visitor_ending = new List<string[]>();

    [Space]
    [Header("etc.")]
    public Scrollbar scrollbar;
    public Animator animator;


    void visitor_Data()//손님과 엔딩 데이터 지정
    {
        if (visitor_title == null || visitor_title.Count == 0)
        {
            visitor_title = new List<string>() {
            "손님1", "손님2", "손님3", "손님4", "손님5", "손님6", "손님7", "손님8", "손님9"
        };
        }
        visitor_ending.Add(new string[] { "엔딩1 name1", "엔딩2", "엔딩3", "엔딩4", "히든 엔딩1", " 히든 엔딩2" });//손님1
        visitor_ending.Add(new string[] { "엔딩1 name2", "엔딩2", "엔딩3", "엔딩4", "히든 엔딩1", " 히든 엔딩2" });//손님2
        visitor_ending.Add(new string[] { "엔딩1 name3", "엔딩2", "엔딩3", "엔딩4", "히든 엔딩1", " 히든 엔딩2" });//손님3
        visitor_ending.Add(new string[] { "엔딩1 name4", "엔딩2", "엔딩3", "엔딩4", "히든 엔딩1", " 히든 엔딩2" });//손님4
        visitor_ending.Add(new string[] { "엔딩1 name5", "엔딩2", "엔딩3", "엔딩4", "히든 엔딩1", " 히든 엔딩2" });//손님5
        visitor_ending.Add(new string[] { "엔딩1 name6", "엔딩2", "엔딩3", "엔딩4", "히든 엔딩1", " 히든 엔딩2" });//손님6
        visitor_ending.Add(new string[] { "엔딩1 name7", "엔딩2", "엔딩3", "엔딩4", "히든 엔딩1", " 히든 엔딩2" });//손님7
        visitor_ending.Add(new string[] { "엔딩1 name8", "엔딩2", "엔딩3", "엔딩4", "히든 엔딩1", " 히든 엔딩2" });//손님8
        visitor_ending.Add(new string[] { "엔딩1 name9", "엔딩2", "엔딩3", "엔딩4", "히든 엔딩1", " 히든 엔딩2" });//손님9
    }



    private void Awake()
    {
        //한 번 이상 클리어했던 손님만 다시할 수 있게 설정
        for (int i = 0; i < GameData.Instance.ending.Count; i++)
        {
            if (GameData.Instance.ending[i][0] || GameData.Instance.ending[i][1] || GameData.Instance.ending[i][2] || GameData.Instance.ending[i][3] || GameData.Instance.ending[i][4] || GameData.Instance.ending[i][5])
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
        scrollbar.value = 1;
    }


    public void Start_StroyScene()
    {
        GameData.Instance.stage = SelectedScene;
        SceneManager.LoadScene(Scene_name);
    }

    public void select_stroy(int selectstory_number)
    {
        SelectedScene = selectstory_number-1;
        //고른 손님에 따라서 화면에 띄울 '그림, 손님 텍스트, 엔딩 텍스트' 설정하기
        VisitorEmage_emage.sprite = visitor_image[SelectedScene];
        VisitorTitle_text.text = visitor_title[SelectedScene];
        for (int i = 0; i < VisitorEnding_text.Length; i++)
        {
            VisitorEnding_text[i].text = visitor_ending[SelectedScene][i];
        }
    }

    public void OpenAndClose_WarningScreen()
    {
        //화면이 꺼져있으면 키고, 켜져있으면 끄기

        if (WarningScreen.transform.GetChild(0).gameObject.activeSelf)//화면이 켜져있을 때
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
