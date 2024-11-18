using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    public float playerReach = 3f;
    Interactable currentInteract;
    // Update is called once per frame
    void Update()
    {
        CheckInteract();
        if (Input.GetKeyDown(KeyCode.E) && currentInteract)
        {

        }
    }
    void CheckInteract()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out hit, playerReach))
        {
            if (hit.collider.tag == "Interactable")
            {
                Interactable newInteract = hit.collider.GetComponent<Interactable>();

                if (currentInteract && newInteract != currentInteract)
                {

                    currentInteract.DisableOutline();
                }
                if (newInteract.enabled)
                {
                    SetNewInteract(newInteract);
                }
                else
                {
                    DisableInteract();
                }

            }
            else
            {
                DisableInteract();
            }
        }
        else
        {
            DisableInteract();
        }
    }
    void SetNewInteract(Interactable newInteract)
    {
        currentInteract = newInteract;
        currentInteract.EnableOutline();
    }
    void DisableInteract()
    {
        if (currentInteract)
        {
            currentInteract.DisableOutline();
            currentInteract = null;
        }
    }

}