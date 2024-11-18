using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameOver : MonoBehaviour
{
    public GameObject gameoverpanel;
    public bool on;
    public bool off;


    private void Start()
    {
        gameoverpanel.SetActive(false);
        off = true;
        on = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        if(off && Input.GetKeyDown(KeyCode.Tab))
        {
            off = false;
            on = true;
            gameoverpanel.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Debug.Log("end key is pressed");

        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
        Debug.Log("Restarting");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu Scene");
        Debug.Log("Going to main menu");
    }
}
