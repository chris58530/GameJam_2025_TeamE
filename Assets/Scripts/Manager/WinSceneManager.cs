using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSceneManager : MonoBehaviour
{
    void Start()
    {
        AudioManager.current.PlaybgmAudio();

    }
    public void LoadScene()
    {
        SceneManager.LoadScene("Intro");
    }
}
