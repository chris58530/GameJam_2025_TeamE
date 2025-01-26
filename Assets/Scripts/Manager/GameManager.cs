using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] GameObject menuUI;

    [SerializeField] GameEvent gameEvent;
    [SerializeField] private Animator startAnimation;
    public enum GameState
    {
        Menu,
        Start,
        GameOver
    }
    void Start()
    {
        SetGameState(GameState.Menu);
        AudioManager.current.PlayStartMusicAudio();
    }
    public void SetSceneButton(string stateName)
    {
        SetGameState((GameState)System.Enum.Parse(typeof(GameState), stateName));
    }
    IEnumerator WaitAndSetGameState(GameState state)
    {
        startAnimation.Play("MagicVideo");
        yield return new WaitForSeconds(1f);
        AudioManager.current.StopstartMusicAudio();
        AudioManager.current.PlaybgmAudio();

        sceneLoader.LoadSceneAdditive("BaseSettingScene");
        sceneLoader.LoadSceneAdditive("UI");
        sceneLoader.LoadSceneAdditive("BathScene");
        menuUI.SetActive(false);
        gameEvent.StartGameEvent();
        PlayerData.Instance.Init();

        FindObjectOfType<DuckSummon>().Summon();
    }
    public void SetGameState(GameState state)
    {
        CameraManager.Instance.SwitchCamera(state);

        switch (state)
        {
            case GameState.Menu:

                sceneLoader.UnloadScene("BaseSettingScene");
                sceneLoader.UnloadScene("UI");
                menuUI.SetActive(true);
                CameraManager.Instance.SwitchCamera(state);

                break;
            case GameState.Start:
                StartCoroutine(WaitAndSetGameState(state));


                break;

            case GameState.GameOver:
                gameEvent.EndGameEvent();
                break;
        }
    }


}
