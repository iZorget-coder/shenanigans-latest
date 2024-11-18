using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [Header("Player Movement")]
    private Rigidbody rb;
    public Transform orientation;
    public float moveSpeed = 5f;

    public GameObject cam;
    private float horizontalInput;
    private float verticalInput;
    public Vector2 playerRotation;

    private Vector3 moveDirection;

    public Animator animator;
    public BoxCollider playerBox;

    public float breathingVolume, footStepsVolume;

    [Header("Audio")]
    public AudioClip walkSound;
    public AudioClip breathingSound;
    private AudioSource walkAudioSource;
    private AudioSource breathingAudioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        playerBox = GetComponent<BoxCollider>();

        // Initialize walk sound
        walkAudioSource = gameObject.AddComponent<AudioSource>();
        walkAudioSource.clip = walkSound;
        walkAudioSource.loop = true;
        walkAudioSource.volume = footStepsVolume;

        // Initialize breathing sound
        breathingAudioSource = gameObject.AddComponent<AudioSource>();
        breathingAudioSource.clip = breathingSound;
        breathingAudioSource.loop = true;
        breathingAudioSource.volume = breathingVolume; // Set a constant breathing volume
        breathingAudioSource.Play(); // Start breat hing sound immediately
    }

    void Update()
    {
        PlayerInput();
        RotatePlayer();

        // Play or stop walk audio based on movement
        if (verticalInput != 0 || horizontalInput != 0)
        {
            if (!walkAudioSource.isPlaying)
                walkAudioSource.Play();
        }
        else
        {
            if (walkAudioSource.isPlaying)
                walkAudioSource.Stop();
        }
    }

    void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void RotatePlayer()
    {
        // Rotating the player and the camera
        playerRotation.x -= Input.GetAxis("Mouse Y");
        playerRotation.y += Input.GetAxis("Mouse X");

        // Clamp the vertical rotation (X-axis)
        playerRotation.x = Mathf.Clamp(playerRotation.x, -80f, 70f);

        // Rotate the player (Y-axis)
        transform.localRotation = Quaternion.Euler(0f, playerRotation.y, 0f);

        // Rotate the camera (X-axis only)
        cam.transform.localRotation = Quaternion.Euler(playerRotation.x, 0f, 0f);
    }

    void MovePlayer()
    {
        animator.SetFloat("speed", verticalInput);
        moveSpeed = 5f;
        if (verticalInput == -1)
        {
            animator.SetBool("walking back", true);
            moveSpeed = 5f;
        }
        else
        {
            animator.SetBool("walking back", false);
        }
        if (horizontalInput == 1)
        {
            animator.SetBool("strafing right", true);
            moveSpeed = 5f;
        }
        else
        {
            animator.SetBool("strafing right", false);
        }
        if (horizontalInput == -1)
        {
            animator.SetBool("strafing left", true);
            moveSpeed = 5f;
        }
        else
        {
            animator.SetBool("strafing left", false);
        }

        // Calculate movement direction based on input
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        moveDirection.Normalize(); // Normalize to ensure consistent speed in all directions

        // Maintain the player's current vertical velocity to avoid upward/downward movement issues
        Vector3 velocity = rb.velocity;
        velocity = moveDirection * moveSpeed * Time.fixedDeltaTime * 100f;
        velocity.y = rb.velocity.y; // Preserve the vertical velocity (gravity)

        // Apply the velocity to the rigidbody
        rb.velocity = velocity;
    }
}
