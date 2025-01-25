using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    static AudioManager current;

    [Header("開頭音樂")]
    public AudioClip StartMusic;
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

    AudioSource startMusicSource;
    AudioSource introMusicSource;
    AudioSource bgmSource;
    AudioSource esSource;
    AudioSource playerSource;

    private void Awake()
    {
        current = this;
        DontDestroyOnLoad(gameObject);

        startMusicSource = gameObject.AddComponent<AudioSource>();
        introMusicSource = gameObject.AddComponent<AudioSource>();
        bgmSource = gameObject.AddComponent<AudioSource>();
        esSource = gameObject.AddComponent<AudioSource>();
        playerSource = gameObject.AddComponent<AudioSource>();

        
    }
    
    // Music聲音觸發
    public static void PlayStartMusicAudio()
    {
        current.startMusicSource.clip = current. StartMusic;
        current.startMusicSource.loop = true;
        current.startMusicSource.Play();
    }
    public static void PlaybgmAudio()
    {
        current.bgmSource.clip = current. bgm;
        current.bgmSource.loop = true;
        current.bgmSource.Play();
    }
    public static void StopstartMusicAudio()
    {
        current.startMusicSource.Stop();
    }
    public static void Stopbgm()
    {
        current.bgmSource.Stop();
    }
    
    // Bubble movement聲音觸發
    public static void PlayfootstepAudio()
    {
        int index = Random.Range(0, current.WalkStepclips.Length);
        current.playerSource.clip = current. WalkStepclips[index];
    }
     public static void PlayEatAudio()
    {
        current.playerSource.clip = current. Eatclip;
        current.playerSource.Play();
    }
     public static void PlayPushAudio()
    {
        current.playerSource.clip = current. Pushclip;
        current.playerSource.Play();
    }
      public static void PlayDeadAudio()
    {
        current.playerSource.clip = current. Deadclip;
        current.playerSource.Play();
    }
    
    //事件音效觸發
      public static void PlayobjectfallAudio()
    {
        current.esSource.clip = current. ObjectFallclip;
        current.esSource.Play();
    }
      public static void PlaybubblefallAudio()
    {
        current.esSource.clip = current. BubbleFallclip;
        current.esSource.Play();
    }
      public static void PlayfallingAudio()
    {
        current.esSource.clip = current. Fallingclip;
        current.esSource.Play();
    }
    
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
