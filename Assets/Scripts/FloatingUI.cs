using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    Transform mainCam;
    Transform unit;
    Transform worldSpaceCanvas;

    public Vector3 offset;
    public float visibilityDistance = 2.0f;

    CanvasGroup textCanvasGroup;
    public Animator doorAnimator, drawerAnimator;
    public TextMeshProUGUI textDisplay;
    public bool isDoorOpen, isDrawerOpen = false;

    private RectTransform rectDrawer;
    private Vector3 originalScale;
    public AudioSource audioSource;
    public AudioClip doorOpenSound;
    public AudioClip doorCloseSound;
    public AudioClip drawerOpenSound;
    public AudioClip drawerCloseSound;
    string message;


    void Start()
    {
       
       
        rectDrawer = GetComponent<RectTransform>();
        originalScale = rectDrawer.localScale;

        mainCam = Camera.main.transform;
        unit = transform.parent;
        worldSpaceCanvas = GameObject.FindGameObjectWithTag("InteractUI").transform;
        transform.SetParent(worldSpaceCanvas);

        textCanvasGroup = GetComponent<CanvasGroup>();
        if (textCanvasGroup == null)
        {
            textCanvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        if (doorAnimator == null)
        {
            Debug.LogError("No Animator assigned to FloatingText script.");
        }

        if (audioSource == null)
        {
            Debug.LogError("No AudioSource assigned to FloatingText script.");
        }

        UpdateDoorStatusText();

        // Apply offset to the RectTransform position
        rectDrawer.localPosition += offset;
    }

    void Update()
    {
        textDisplay.text = message;
        if (gameObject.tag == "torchUI")
        {
            message = "torch";
        }

        if (gameObject.tag == "drawerUI")
        {
            message = "drawer";
        }

        float distanceToPlayer = Vector3.Distance(unit.position, mainCam.position);

        if (gameObject.tag == "doorUI")
        {
            HandleDoorInteraction(distanceToPlayer);
        }

        if (gameObject.tag == "drawerUI")
        {
            HandleDrawerInteraction(distanceToPlayer);
        }

        if (gameObject.tag == "torchUI")
        {
            HandleTorchInteraction(distanceToPlayer);
        }

        transform.LookAt(mainCam);
        transform.Rotate(180, 0, 0);

        Vector3 desiredPosition = unit.position + offset;
        transform.position = desiredPosition;
    }

    void HandleDoorInteraction(float distanceToPlayer)
    {
        if (distanceToPlayer <= visibilityDistance)
        {
            textCanvasGroup.alpha = 1f;
            textCanvasGroup.interactable = true;
            textCanvasGroup.blocksRaycasts = true;

            UpdateDoorStatusText();
            if (Input.GetKeyDown(KeyCode.E) && doorAnimator != null)
            {
                if (!isDoorOpen)
                {
                    doorAnimator.SetBool("isOpening", true);
                    doorAnimator.SetBool("isClosing", false);
                    isDoorOpen = true;

                    if (doorOpenSound != null && audioSource != null)
                    {
                        audioSource.PlayOneShot(doorOpenSound);
                    }
                }
                else
                {
                    doorAnimator.SetBool("isOpening", false);
                    doorAnimator.SetBool("isClosing", true);
                    isDoorOpen = false;

                    if (doorCloseSound != null && audioSource != null)
                    {
                        audioSource.PlayOneShot(doorCloseSound);
                    }
                }

                UpdateDoorStatusText();
            }
        }
        else
        {
            HideTextCanvasGroup();
        }
    }

    void HandleDrawerInteraction(float distanceToPlayer)
    {
        if (distanceToPlayer <= visibilityDistance)
        {
            textCanvasGroup.alpha = 1f;
            textCanvasGroup.interactable = true;
            textCanvasGroup.blocksRaycasts = true;

            if (Input.GetKeyDown(KeyCode.E) && drawerAnimator != null)
            {
                if (!isDrawerOpen)
                {
                    // Open the drawer
                    drawerAnimator.SetBool("openDrawer", true);
                    drawerAnimator.SetBool("closeDrawer", false);
                    isDrawerOpen = true;

                    StartCoroutine(ScaleRectTransform(rectDrawer, Vector3.zero, 0.1f));

                    if (drawerOpenSound != null && audioSource != null)
                    {
                        audioSource.PlayOneShot(drawerOpenSound);
                    }
                }
                else
                {
                    // Close the drawer
                    drawerAnimator.SetBool("openDrawer", false);
                    drawerAnimator.SetBool("closeDrawer", true);
                    isDrawerOpen = false;

                    StartCoroutine(ScaleRectTransform(rectDrawer, originalScale, 0.1f));

                    if (drawerCloseSound != null && audioSource != null)
                    {
                        audioSource.PlayOneShot(drawerCloseSound);
                    }

                    HideTextCanvasGroup();
                }
            }

            // Disable torchSlot when drawer is open and 'C' is pressed
           
        }
        else
        {
            HideTextCanvasGroup();
        }
    }


    void HandleTorchInteraction(float distanceToPlayer)
    {
        if (distanceToPlayer <= visibilityDistance)
        {
            textCanvasGroup.alpha = 1f;
            textCanvasGroup.interactable = true;
            textCanvasGroup.blocksRaycasts = true;
        }
        else
        {
            HideTextCanvasGroup();
        }
    }

    void HideTextCanvasGroup()
    {
        textCanvasGroup.alpha = 0f;
        textCanvasGroup.interactable = false;
        textCanvasGroup.blocksRaycasts = false;
    }

    IEnumerator ScaleRectTransform(RectTransform rectTransform, Vector3 targetScale, float duration)
    {
        Vector3 initialScale = rectTransform.localScale;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            rectTransform.localScale = Vector3.Lerp(initialScale, targetScale, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        rectTransform.localScale = targetScale;
    }

    void UpdateDoorStatusText()
    {
        if (textDisplay != null)
        {
            if (gameObject.tag == "doorUI")
            {
                textDisplay.text = isDoorOpen ? message = "close" : message = "open";
            }
        }
    }
}
