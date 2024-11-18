using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryScript : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene("MainMenu Scene", LoadSceneMode.Single);
    }
}
