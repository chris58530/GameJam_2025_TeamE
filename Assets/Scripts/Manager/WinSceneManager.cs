using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSceneManager : MonoBehaviour
{
   public void LoadScene()
   {
       SceneManager.LoadScene("Intro");
   }
}
