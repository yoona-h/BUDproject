using UnityEngine;
using UnityEngine.UI;

public class setting_manager : MonoBehaviour
{
    public Slider effectsound_scrollbar;
    public Slider backgroundmusic_scrollbar;
    public Slider speakspeed_scrollbar;
    public Toggle speakauto_toggle;
    public Toggle easymode_toggle;



    //------------------------------------------설정값 관련-------------------------------------
    public void apply_from_GameData()
    {
        effectsound_scrollbar.value = GameData.Instance.EffectSound_Volume;
        backgroundmusic_scrollbar.value = GameData.Instance.BackGroundMusic_Volume;
        speakspeed_scrollbar.value = GameData.Instance.SpeakSpeed;
        speakauto_toggle.isOn = GameData.Instance.SpeakingAuto;
        easymode_toggle.isOn = GameData.Instance.EasyMode;
    }

    public void apply_effectsound()
    {
        GameData.Instance.EffectSound_Volume = effectsound_scrollbar.value;
    }
    public void apply_backgroundmusic()
    {
        GameData.Instance.BackGroundMusic_Volume = backgroundmusic_scrollbar.value;
    }
    public void apply_speakspeed()
    {
        GameData.Instance.SpeakSpeed = (int)speakspeed_scrollbar.value;
    }
    public void apply_speakauto()
    {
        GameData.Instance.SpeakingAuto = speakauto_toggle.isOn;
    }
    public void apply_easymode()
    {
        GameData.Instance.EasyMode = easymode_toggle.isOn;
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