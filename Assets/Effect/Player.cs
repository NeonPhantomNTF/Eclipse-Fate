using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public Transform cameraTransform;
    public GameObject skillEffectPrefab; // Skill effect prefab
    public Transform skillSpawnPoint; // Spawn point for the skill effect

    private CharacterController characterController;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovement();
        HandleJumping();
        HandleSkill();
    }

    private void HandleMovement()
    {
        // Get input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement direction relative to the camera
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            // Convert move direction to world space relative to the camera
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // Move the character
            characterController.Move(moveDir * moveSpeed * Time.deltaTime);
        }
    }

    private void HandleJumping()
    {
        // Check if the character is grounded
        isGrounded = characterController.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Ensure the character sticks to the ground
        }

        // Handle jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Apply vertical movement
        characterController.Move(velocity * Time.deltaTime);
    }

    private void HandleSkill()
    {
        // Check if the F key is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (skillEffectPrefab != null && skillSpawnPoint != null)
            {
                // Instantiate the skill effect at the spawn point
                Instantiate(skillEffectPrefab, skillSpawnPoint.position, skillSpawnPoint.rotation);
            }
            else
            {
                Debug.LogWarning("Skill effect prefab or spawn point not assigned!");
            }
        }
    }
}
