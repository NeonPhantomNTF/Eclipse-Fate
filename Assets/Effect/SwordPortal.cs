using UnityEngine;

public class SwordPortal : MonoBehaviour
{
    public Transform portalExit; // Position where the sword exits
    public float moveSpeed = 2f; // Speed of the sword movement
    private Renderer swordRenderer;
    private Material swordMaterial;

    void Start()
    {
        // Get the sword's renderer and material
        swordRenderer = GetComponent<Renderer>();
        swordMaterial = swordRenderer.material;
    }

    void Update()
    {
        // Move the sword towards the portal exit
        transform.position = Vector3.MoveTowards(transform.position, portalExit.position, moveSpeed * Time.deltaTime);

        // Update the clipping plane for visibility
        Vector4 clipPlane = new Vector4(
            portalExit.forward.x,
            portalExit.forward.y,
            portalExit.forward.z,
            -Vector3.Dot(portalExit.forward, portalExit.position)
        );
        swordMaterial.SetVector("_ClipPlane", clipPlane);
    }
}
