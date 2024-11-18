using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpscareTrigger : MonoBehaviour
{
    public AudioSource Scream;
    public GameObject playerCam;
    public GameObject Jumpcam;
    public GameObject flashTimer;

    void OnTriggerEnter()
    {
        Scream.Play();
        Jumpcam.SetActive(true);
        playerCam.SetActive(false);
        flashTimer.SetActive(true);
        StartCoroutine(EndJumpscare());
    }

    IEnumerator EndJumpscare()
    {
        yield return new WaitForSeconds(2.03f);
    playerCam.SetActive(true );
        Jumpcam.SetActive(false);
        flashTimer.SetActive(false);
    }
}
