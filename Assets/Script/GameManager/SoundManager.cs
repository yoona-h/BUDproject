using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [Header("Sound Players")]
    public AudioSource bgmPlayer;//배경음 재생하기 위한 오디오소스
    AudioSource[] sfxPlayers = new AudioSource[10];//효과음을 재생하기 위한 오디오소스 (여러개 동시재생하기 위해 배열로 구현)
    public GameObject sfxPlayer_obj;

    [Header("Audio Clips")]
    public AudioClip bgmSound;//재생할 BGM 저장소
    public AudioClip[] sfxSounds;//재생할 SFX 저장소

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxPlayer_obj.AddComponent<AudioSource>();
        }

        Init();
    }


    public void PlayBGM(AudioClip bgmSound = null)//BGM 바꾸고싶을 때만 매개변수 전달하기. 그냥 재생은 매개변수 없어도 됨
    {
        if (bgmSound == null)
            bgmPlayer.Play();
        else
        {
            bgmPlayer.clip = bgmSound;
            bgmPlayer.Play();
        }
    }
    public void StopBGM()
    {
        bgmPlayer.Stop();
    }


    public void PlaySFX(AudioClip sfxSound)
    {
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            if (sfxPlayers[index].isPlaying)
                continue;

            sfxPlayers[index].clip = sfxSound;
            sfxPlayers[index].Play();
            return;
        }
    }
    public void PlaySFX(int sfxSound)
    {
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            if (sfxPlayers[index].isPlaying)
                continue;

            sfxPlayers[index].clip = sfxSounds[sfxSound];
            sfxPlayers[index].Play();
            return;
        }
    }


    private void Init()//오디오 소스 설정 초기화
    {
        bgmPlayer.playOnAwake = true;
        bgmPlayer.loop = true;
        bgmPlayer.clip = bgmSound;

        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].loop = false;
        }

        apply_volume();
    }
    public void apply_volume()
    {
        bgmPlayer.volume = GameData.Instance.BackGroundMusic_Volume;
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i].volume = GameData.Instance.EffectSound_Volume;
        }
    }
}
