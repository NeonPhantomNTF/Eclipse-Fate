using UnityEngine;

public class CubeMaskController : MonoBehaviour
{
    public Material swordMaterial;
    public Transform cubeTransform;

    void Update()
    {
        if (swordMaterial != null && cubeTransform != null)
        {
            swordMaterial.SetVector("_CubePosition", cubeTransform.position);
            swordMaterial.SetVector("_CubeScale", cubeTransform.localScale);
        }
    }
}
