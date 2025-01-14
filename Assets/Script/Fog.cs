using UnityEngine;

public class FogZone : MonoBehaviour
{
    // Backup original fog settings
    private float originalFogStart;
    private float originalFogEnd;

    void Start()
    {
        // Save the original fog settings
        originalFogStart = RenderSettings.fogStartDistance;
        originalFogEnd = RenderSettings.fogEndDistance;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Change fog settings
            RenderSettings.fogStartDistance = 1f;
            RenderSettings.fogEndDistance = 20f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Revert to original fog settings
            RenderSettings.fogStartDistance = originalFogStart;
            RenderSettings.fogEndDistance = originalFogEnd;
        }
    }
}
