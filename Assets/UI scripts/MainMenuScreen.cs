using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuScreen : MonoBehaviour
{
    public GameObject gameintroTitleCanvas;  // Reference to the Game Title Canvas
    public GameObject storylineCanvas;  // Reference to the Storyline Canvas
    
    public float delayBeforeMenu = 5f;
    public float storylineDuration = 5f;

    private void Start()
    {
        //gameintroTitleCanvas.SetActive(true);
        //storylineCanvas.SetActive(false);

        StartCoroutine(AutoLoadMainMenu() );
    }

    

    public void Play()
    {
        // Load the game scene directly if "Play" is selected
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    private IEnumerator AutoLoadMainMenu()
    {
        // Wait for 3 seconds before loading the main menu
        yield return new WaitForSeconds(delayBeforeMenu);

        // Hide the Game Title Canvas and show the Storyline Canvas before loading the Main Menu
        gameintroTitleCanvas.SetActive(false);
        storylineCanvas.SetActive(true);

        // Load the Main Menu Scene after the storyline is shown
        yield return new WaitForSeconds(storylineDuration);  // Additional delay for storyline display
        SceneManager.LoadScene("MainMenu Scene");
    }
}
