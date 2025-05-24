using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class ConsultManager : MonoBehaviour
{
    public DialogueLoader loader;

    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI dialogueText;
    public Button[] optionButtons;
    public GameObject optionScreen;

    private int currentId;
    private Coroutine typingCoroutine;

    private bool isTyping = false;
    private string fullTextBuffer = "";
    private int nextIdBuffer = -1;

    private void Start()
    {
        StartDialogue();
    }
    void Update()
    {
        // 클릭 또는 키 입력
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                dialogueText.text = fullTextBuffer;
                isTyping = false;
            }
            else if (!IsChoiceActive() && nextIdBuffer != -1 && !GameData.Instance.SpeakingAuto)
            {
                ShowDialogue(nextIdBuffer);
            }
        }
    }

    void StartDialogue()
    {
        ShowDialogue(1); // 시작 대사 ID
    }
    public void ShowDialogue(int id)
    {
        if (!loader.dialogueDict.TryGetValue(id, out TalkingData data))
        {
            print($"error : ID {id} 대사를 찾을 수 없습니다.");
            return;
        }

        currentId = id;

        if (data.type == "dialogue")
        {
            speakerText.text = data.speaker;
            HideOptions();
            //ExecuteCommand(data.commands?.Length > 0 ? data.commands[0] : null);//이벤트 실행부분. 아직 구현 안함...

            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);

            typingCoroutine = StartCoroutine(TypeText(data.texts[0], data.nextIds.Length > 0 ? data.nextIds[0] : -1));
        }
        else if (data.type == "choice")
        {
            //dialogueText.text = "";
            optionScreen.SetActive(true);
            HideOptions();

            for (int i = 0; i < optionButtons.Length; i++)
            {
                if (i >= data.texts.Length)
                {
                    optionButtons[i].gameObject.SetActive(false);
                    continue;
                }

                int idx = i;
                optionButtons[i].gameObject.SetActive(true);
                optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = data.texts[idx];

                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => {
                    if (data.statChanges.Length > idx && data.statChanges[idx] != null)
                        GameData.Instance.visitor_status[data.statChanges[idx].index] += data.statChanges[idx].value;

                    //if (data.commands.Length > idx)
                    //  ExecuteCommand(data.commands[idx]);//이벤트 실행부분. 아직 구현 안함... (그리고 선택지에서의 이벤트는 해당 선택지 이후 연결되는 대사에 넣는걸로 대체 가능)

                    optionScreen.SetActive(false);
                    ShowDialogue(data.nextIds[idx]);
                });
            }
        }
    }

    IEnumerator TypeText(string fullText, int nextId)
    {
        dialogueText.text = "";
        fullTextBuffer = fullText;
        nextIdBuffer = nextId;
        isTyping = true;

        float delay = (6 - GameData.Instance.SpeakSpeed) * 0.03f;

        foreach (char c in fullText)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(delay);
        }

        isTyping = false;

        if (GameData.Instance.SpeakingAuto && nextId != -1)
        {
            yield return new WaitForSeconds(0.6f);
            ShowDialogue(nextId);
        }
    }

    void HideOptions()
    {
        foreach (var btn in optionButtons)
            btn.gameObject.SetActive(false);
    }

    bool IsChoiceActive()
    {
        foreach (var btn in optionButtons)
            if (btn.gameObject.activeSelf) return true;
        return false;
    }

    /*
    void ExecuteCommand(string command)
    {
        if (string.IsNullOrWhiteSpace(command)) return;

        if (command.StartsWith("face_"))
        {
            var parts = command.Split('_');
            if (parts.Length >= 3)
                CharacterManager.Instance.SetFace(parts[1], parts[2]);
        }
        else if (command.StartsWith("bgm_"))
        {
            AudioManager.Instance.PlayBGM(command.Substring(4));
        }
        else if (command == "shake")
        {
            CameraController.Instance.Shake();
        }
    }
    */
}
