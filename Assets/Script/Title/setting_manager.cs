using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class setting_manager : MonoBehaviour
{
    [Header("Interators")]
    public Slider effectsound_scrollbar;
    public Slider backgroundmusic_scrollbar;
    public Slider speakspeed_scrollbar;
    public Toggle speakauto_toggle;
    public Toggle easymode_toggle;

    [Header("Texts")]
    public TMP_Text effectsound_text;
    public TMP_Text backgroundmusic_text;
    public TMP_Text speakspeed_text;

    public readonly string[] AudioLevel = {
        "무 음",       // 0
        "속삭임",      // 1
        "고요함",      // 2
        "머 묾",       // 3
        "차오름",      // 4
        "또렷함",      // 5
        "두드러짐",    // 6
        "울 림",       // 7
        "충만함",      // 8
        "넘 침",       // 9
        "벅 참"        // 10
    };
    public readonly string[] SpeedLevel = new string[]
    {
        "느림",   //1
        "차분",   //2
        "보통",   //3
        "빠름",   //4
        "급속"    //5
    };

    //------------------------------------------설정값 관련-------------------------------------
    public void apply_from_GameData()
    {
        effectsound_scrollbar.value = GameData.Instance.EffectSound_Volume;
        backgroundmusic_scrollbar.value = GameData.Instance.BackGroundMusic_Volume;
        speakspeed_scrollbar.value = GameData.Instance.SpeakSpeed;
        speakauto_toggle.isOn = GameData.Instance.SpeakingAuto;
        easymode_toggle.isOn = GameData.Instance.EasyMode;
    }

    public void apply_from_Interactors()
    {
        GameData.Instance.EffectSound_Volume = effectsound_scrollbar.value;
        GameData.Instance.BackGroundMusic_Volume = backgroundmusic_scrollbar.value;
        GameData.Instance.SpeakSpeed = (int)speakspeed_scrollbar.value;
        GameData.Instance.SpeakingAuto = speakauto_toggle.isOn;
        GameData.Instance.EasyMode = easymode_toggle.isOn;
    }

    public void apply_Slider_to_Text()
    {
        effectsound_text.text = AudioLevel[(int)(GameData.Instance.EffectSound_Volume * 10)] + new string('*', (int)(GameData.Instance.EffectSound_Volume * 100) % 10/2);
        backgroundmusic_text.text = AudioLevel[(int)(GameData.Instance.BackGroundMusic_Volume * 10)] + new string('*', (int)(GameData.Instance.BackGroundMusic_Volume * 100) % 10/2);
        speakspeed_text.text = SpeedLevel[GameData.Instance.SpeakSpeed-1];
    }




    //------------------------------------------닫기 버튼 관련----------------------------------
    
    public void close_screen()
    {
        gameObject.GetComponent<title_manager>().OpenAndClose_SettingScreen();
    }
    bool clicked = false;
    public void mousedown_effect()
    {
        clicked = true;
    }
    public void mouseup_effect()
    {
        if (clicked)
            close_screen();
    }
    public void mouseexit_effect()
    {
            clicked = false;
    }
}