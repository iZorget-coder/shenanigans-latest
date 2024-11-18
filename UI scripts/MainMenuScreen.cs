using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuScreen : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("MainMenu Scene");
    }
   public void Play()
    {
        SceneManager.LoadScene("SampleScene");

    }
}
