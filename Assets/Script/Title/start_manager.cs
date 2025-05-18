using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class start_manager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text button_text;

    [Header("Scence")]
    public string StartScene;
    public string TutorialScene;

    [HideInInspector] public int final_stage = 0;

    void Start()
    {
        if (GameData.Instance.FirstGame)
            button_text.text = "시작하기";
        else
            button_text.text = "이어하기";
        Find_Finalending();
    }

    public void StartButton_function()
    {
        if (GameData.Instance.FirstGame)//처음시작했을 때
            SceneManager.LoadScene(TutorialScene);
        else
        {
            GameData.Instance.stage = final_stage;
            SceneManager.LoadScene(StartScene);
        }
    }


    public void Find_Finalending()//가장 마지막으로 진행한 엔딩이 무엇인지 탐색하는 함수. GameData에 있는 현재 진행중인 스테이지를 나타내는 stage와는 다른 기능을 가지기 위함!
    {
        for (int i = 0; i < GameData.Instance.ending.Count; i++)
        {
            int ending_count = 0;//엔딩 수집 여부 개수를 세기 위한 변수
            for (int j = 0; j < GameData.Instance.ending[i].Length; j++)//엔딩 개수만큼 반복하기
            {
                //엔딩수집을 못한 경우만 ending_count 세기
                if (GameData.Instance.ending[i][j] == false)
                {
                    ending_count++;
                }
            }
            if (ending_count == GameData.Instance.ending[i].Length)//만약 엔딩을 하나도 보지 못한 스테이지가 있다면 그 스테이지에서 시작해야함
            {
                final_stage = i + 1;
                break;
            }
        }
    }
}
