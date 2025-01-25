using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] GameObject menuUI;

    [SerializeField] GameEvent gameEvent;
    public enum GameState
    {
        Menu,
        Start,
        Playing,
        GameOver
    }
    public void SetSceneButton(string stateName)
    {
        SetGameState((GameState)System.Enum.Parse(typeof(GameState), stateName));
    }
    public void SetGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Menu:
                sceneLoader.UnloadScene("BaseSettingScene");
                sceneLoader.UnloadScene("UI");
                menuUI.SetActive(true);

                break;
            case GameState.Start:
                sceneLoader.LoadSceneAdditive("BaseSettingScene");
                sceneLoader.LoadSceneAdditive("UI");
                sceneLoader.LoadSceneAdditive("BathScene");
                menuUI.SetActive(false);
                gameEvent.StartGameEvent();
                PlayerData.Instance.Init();
                break;
            case GameState.Playing:
                break;
            case GameState.GameOver:
                break;
        }
    }


}
