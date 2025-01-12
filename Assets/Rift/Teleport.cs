using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    public GameObject portal; // The portal object (or prefab) to spawn
    public float spawnDistance = 5f; // Distance in front of the player where the portal will spawn
    public string portalAnimationName; // The name of the animation to play on the portal

    private Animator portalAnimator;

    void Start()
    {
        // Ensure portal has an Animator component
        if (portal != null)
        {
            portalAnimator = portal.GetComponent<Animator>();
        }
    }

    void Update()
    {
        // Check if the player presses the "E" key
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnPortal();
        }
    }

    void SpawnPortal()
    {
        if (portal != null)
        {
            // Calculate the position in front of the player (in world space)
            Vector3 spawnPosition = transform.position + transform.forward * spawnDistance;

            // Instantiate the portal at the calculated position
            GameObject newPortal = Instantiate(portal, spawnPosition, Quaternion.identity);

            // Play the portal's animation
            Animator newPortalAnimator = newPortal.GetComponent<Animator>();
            if (newPortalAnimator != null && newPortalAnimator.HasState(0, Animator.StringToHash(portalAnimationName)))
            {
                newPortalAnimator.Play(portalAnimationName);
            }
            else
            {
                Debug.LogWarning("Portal animation not found.");
            }
        }
        else
        {
            Debug.LogWarning("Portal object is not assigned.");
        }
    }
}
