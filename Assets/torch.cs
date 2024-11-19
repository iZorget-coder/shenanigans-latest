using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class torch : MonoBehaviour
{
    public GameObject torchSlotOFF, torchSlotON;
    GameObject torchObject;
    FloatingText torchScrpt;
    GameObject player;
    GameObject playerCamera;
    public GameObject torchLight;
    bool hasPickedUpTorch = false;
    private bool isTorchOn = false;

    public float torchDuration = 10f;
    public float cooldownDuration = 30f;
    private float torchTimer = 0f;
    private float cooldownTimer = 0f;

    private bool isInCooldown = false;

    public Slider torchSlider;
    public TMP_Text torchHintText1, torchHintText2;

    void Start()
    {
        torchScrpt = GameObject.FindGameObjectWithTag("drawerUI").GetComponent<FloatingText>();
        torchObject = GameObject.Find("torch");
        player = GameObject.FindGameObjectWithTag("Player");
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
        torchSlotON.SetActive(false);
        torchLight.SetActive(false);  // Ensure the torch light is off at the start

        torchSlider.maxValue = torchDuration;
        torchSlider.value = torchDuration;
        torchSlider.gameObject.SetActive(false);

        torchHintText1.gameObject.SetActive(false);
        torchHintText2.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isTorchOn)
        {
            HandleTorchUsage();
        }

        if (isInCooldown)
        {
            HandleCooldown();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (torchSlotOFF != null && !hasPickedUpTorch)
            {
                PickUpTorch();
            }

            if (hasPickedUpTorch && !isTorchOn && !isInCooldown)
            {
                TurnOnTorch();
            }
            else if (hasPickedUpTorch && isTorchOn)
            {
                TurnOffTorch();
            }
        }

        if (hasPickedUpTorch && !isTorchOn && !isInCooldown)
        {
            torchHintText1.gameObject.SetActive(true);
            torchHintText2.gameObject.SetActive(true);
        }
        else
        {
            torchHintText1.gameObject.SetActive(false);
            torchHintText2.gameObject.SetActive(false);
        }
    }

    void PickUpTorch()
    {
        torchSlotOFF.SetActive(true);
        torchObject.transform.SetParent(playerCamera.transform);
        torchObject.transform.localPosition = new Vector3(-1.042f, -0.673f, -0.385f);
        torchObject.transform.localRotation = Quaternion.Euler(-64.679f, 119.838f, 605.537f);
        hasPickedUpTorch = true;
        torchLight.SetActive(false);  // Ensure the torch light is off when picked up
        torchSlotOFF.SetActive(false);
        torchSlotON.SetActive(true);

        torchSlider.gameObject.SetActive(true);
        torchHintText1.gameObject.SetActive(true);
    }

    void TurnOnTorch()
    {
        torchLight.SetActive(true);
        isTorchOn = true;
        torchSlotON.SetActive(true);
        torchSlotOFF.SetActive(false);

        torchTimer = torchDuration;
        torchSlider.maxValue = torchDuration;
        torchSlider.value = torchDuration;
    }

    void TurnOffTorch()
    {
        torchLight.SetActive(false);
        isTorchOn = false;
        torchSlotON.SetActive(false);
        torchSlotOFF.SetActive(true);

        StartCooldown();
    }

    void HandleTorchUsage()
    {
        torchTimer -= Time.deltaTime;
        torchSlider.value = torchTimer;

        if (torchTimer <= 0)
        {
            TurnOffTorch();
        }
    }

    void StartCooldown()
    {
        isInCooldown = true;
        cooldownTimer = 0f;
        torchSlider.maxValue = cooldownDuration;
        torchSlider.value = cooldownTimer;
    }

    void HandleCooldown()
    {
        cooldownTimer += Time.deltaTime;
        torchSlider.value = cooldownTimer;

        if (cooldownTimer >= cooldownDuration)
        {
            isInCooldown = false;
            torchSlider.value = torchDuration;
        }
    }
}
