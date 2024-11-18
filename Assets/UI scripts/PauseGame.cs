using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public GameObject menu;
    public GameObject resume;
    public GameObject quit;

    public bool on;
    public bool off;





    void Start()
    {
        menu.SetActive(false);
        off = true;
        on = false;
    }




    void Update()
    {
        if (off && Input.GetButtonDown("pause"))
        {
            Time.timeScale = 0;
            menu.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            off = false;
            on = true;
        }

        else if (on && Input.GetButtonDown("pause"))
        {
            Time.timeScale = 1;
            menu.SetActive(false);
            off = true;
            on = false;
        }
        
    }

    public void Resume()
    {
            Time.timeScale = 1;
            menu.SetActive(false);
            off = true;
            on = false;
        Cursor.lockState = CursorLockMode.Locked;


    }
    public void backToMainMenu()
    {
        Time.timeScale = 1;
        menu.SetActive(false);
        off = false;    
        on = true;
        SceneManager.LoadScene("MainMenu Scene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
