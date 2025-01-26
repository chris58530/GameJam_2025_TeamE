using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] private GameObject playCamera;
    [SerializeField] private GameObject UICamera;

    public void FindPlayer(GameObject player)
    {
        playCamera.GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
    }
    public void ShakeCamera()
    {
        CinemachineBasicMultiChannelPerlin perlin = playCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = 10;
        StartCoroutine(StopShake(.5f));
    }
    IEnumerator StopShake(float time)
    {
        yield return new WaitForSeconds(time);
        CinemachineBasicMultiChannelPerlin perlin = playCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = 0;
    }

    public void SwitchCamera(GameManager.GameState gameState)
    {
        switch (gameState)
        {
            case GameManager.GameState.Menu:
                playCamera.SetActive(false);
                UICamera.SetActive(true);
                break;
            case GameManager.GameState.Start:
                playCamera.SetActive(true);
                UICamera.SetActive(false);
                break;
        }
    }
}
