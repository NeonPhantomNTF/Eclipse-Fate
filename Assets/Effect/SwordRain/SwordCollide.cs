using UnityEngine;

public class BladeBehavior : MonoBehaviour
{
    public float emergeSpeed = 2f; // Speed at which the blade emerges
    public float shootSpeed = 10f; // Speed at which the blade moves
    public Material bladeMaterial; // Assign the blade material with the erode slider
    private bool isShot = false;
    private Vector3 targetPosition;

    private void Start()
    {
        // Get the cursor position in world space
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            targetPosition = hit.point;
        }
        else
        {
            // Default to some forward position if no valid target
            targetPosition = transform.position + transform.forward * 10f;
        }

        // Calculate the direction to the target
        Vector3 directionToTarget = (targetPosition - transform.position).normalized;

        // Adjust the blade's orientation to face the target
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

        // Rotate the blade to face forward (Z-axis) while adjusting for its original "upward" (Y-axis) orientation
        transform.rotation = targetRotation * Quaternion.Euler(90, 0, 0); // Adjust angles as needed
    }

    private void Update()
    {
        if (!isShot)
        {
            // Slowly emerge the blade
            transform.localPosition += Vector3.up * emergeSpeed * Time.deltaTime;

            if (transform.localPosition.y >= 1f) // Assuming the blade is fully out at y = 1
            {
                isShot = true;
            }
        }
        else
        {
            // Move the blade toward the target
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, shootSpeed * Time.deltaTime);

            // Destroy if it reaches the target
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                StartCoroutine(HandleErodeEffect());
            }
        }
    }

    private System.Collections.IEnumerator HandleErodeEffect()
    {
        float erodeValue = 0f;
        float duration = 0.8f;

        while (erodeValue < 1f)
        {
            erodeValue += Time.deltaTime / duration;
            bladeMaterial.SetFloat("_Erode", erodeValue); // Update the erode slider
            yield return null;
        }

        Destroy(gameObject); // Destroy the blade after the erode effect completes
    }
}
