using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IntroScene : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(gameSceneName);
        }
    }
    public UnityEngine.Video.VideoPlayer videoPlayer;
    public string gameSceneName;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(UnityEngine.Video.VideoPlayer vp)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(gameSceneName);
    }
    void OnDisable()
    {
        videoPlayer.loopPointReached -= OnVideoFinished;
    }
}
