using System.Data.Common;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static AudioManager current;

    [Header("開頭音樂")]
    public AudioClip StartMusic;
    [Header("Battle音樂")]
    public AudioClip BattleMusic;
    [Header("Intro")]
    public AudioClip IntroMusic;
    [Header("BGM")]
    public AudioClip bgm;
    [Header("Bubble Movement")]
    public AudioClip[] WalkStepclips;
    public AudioClip Eatclip;
    public AudioClip Pushclip;
    public AudioClip Deadclip;
    [Header("事件音效")]
    public AudioClip ObjectFallclip;
    public AudioClip Fallingclip;
    public AudioClip BubbleFallclip;

  [Header("AudioMixer 設定")]
    public AudioMixer audioMixer; // 引用 AudioMixer 資源
    public string bgmMixerGroupName = "BGM";        // BGM 的 Mixer Group 名稱
    public string esMixerGroupName = "SFX";        // 事件音效的 Mixer Group 名稱
    public string playerMixerGroupName = "Player"; // 玩家動作音效的 Mixer Group 名稱


    public AudioSource bgmSource;
    public AudioSource esSource;
    public AudioSource playerSource;

    private void Awake()
    {
        current = this;


        bgmSource = gameObject.AddComponent<AudioSource>();
        esSource = gameObject.AddComponent<AudioSource>();
        playerSource = gameObject.AddComponent<AudioSource>();

        AssignMixerGroup(bgmSource, bgmMixerGroupName);
        AssignMixerGroup(esSource, esMixerGroupName);
        AssignMixerGroup(playerSource, playerMixerGroupName);
    }

    private void AssignMixerGroup(AudioSource source, string mixerGroupName)
    {
        if (audioMixer == null)
        {
            Debug.LogError("AudioMixer 尚未指定！");
            return;
        }

        AudioMixerGroup[] mixerGroups = audioMixer.FindMatchingGroups(mixerGroupName);
        if (mixerGroups.Length > 0)
        {
            source.outputAudioMixerGroup = mixerGroups[0];
            Debug.Log($"成功將 {source.gameObject.name} 的輸出設置為 {mixerGroupName} Group");
        }
        else
        {
            Debug.LogError($"未找到名為 {mixerGroupName} 的 Mixer Group！");
        }
    }


    // Music聲音觸發
    public void PlayStartMusicAudio()
    {
        current.bgmSource.clip = current.StartMusic;
        current.bgmSource.loop = true;
        current.bgmSource.Play();
    }
    public void PlaybgmAudio()
    {
        current.bgmSource.clip = current.bgm;
        current.bgmSource.loop = true;
        current.bgmSource.Play();
    }
     public void PlayBattleBGM()
    {
        current.bgmSource.clip = current.BattleMusic;
        current.bgmSource.loop = true;
        current.bgmSource.Play();
    }
    public void StopstartMusicAudio()
    {
        current.bgmSource.Stop();
    }
    public void Stopbgm()
    {
        current.bgmSource.Stop();
    }

    // Bubble movement聲音觸發
    public void PlayfootstepAudio()
    {
        if (current.playerSource.isPlaying)
        {
            return;
        }
        int index = Random.Range(0, current.WalkStepclips.Length);
        current.playerSource.clip = current.WalkStepclips[index];
        playerSource.Play();
    }
    public void PlayEatAudio()
    {
        current.playerSource.clip = current.Eatclip;
        current.playerSource.Play();
    }
    public void PlayPushAudio()
    {
        current.playerSource.clip = current.Pushclip;
        current.playerSource.Play();
    }
    public void PlayDeadAudio()
    {
        current.playerSource.clip = current.Deadclip;
        current.playerSource.Play();
    }

    //事件音效觸發
    public void PlayobjectfallAudio()
    {
        current.esSource.clip = current.ObjectFallclip;
        current.esSource.Play();
    }
    public void PlaybubblefallAudio()
    {
        current.esSource.clip = current.BubbleFallclip;
        current.esSource.Play();
    }
    public void PlayfallingAudio()
    {
        current.esSource.clip = current.Fallingclip;
        current.esSource.PlayOneShot(BubbleFallclip);
    }


}
