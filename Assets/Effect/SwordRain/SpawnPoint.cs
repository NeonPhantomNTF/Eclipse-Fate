using UnityEngine;

public class SwordRainSpawner : MonoBehaviour
{
    public GameObject magicCirclePrefab; // Assign the magic circle prefab
    public Transform[] spawnPoints; // Assign 10 spawn points in the inspector
    public float spawnInterval = 0.2f; // Interval between spawns
    public int totalSpawns = 10; // Total number of magic circles to spawn
    private bool isSkillActive = false; // Ensure the skill activates only once per press

    private void Update()
    {
        // Check if the player presses the 'R' key and the skill is not already active
        if (Input.GetKeyDown(KeyCode.R) && !isSkillActive)
        {
            isSkillActive = true;
            StartCoroutine(SpawnMagicCircles());
        }
    }

    private System.Collections.IEnumerator SpawnMagicCircles()
    {
        for (int i = 0; i < totalSpawns; i++) // Spawn the specified number of magic circles
        {
            // Pick a random spawn point
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Instantiate the magic circle at the random spawn point
            GameObject circle = Instantiate(magicCirclePrefab, randomSpawnPoint.position, Quaternion.identity);

            // Set it under the parent (optional for better organization)
            circle.transform.parent = transform;

            // Wait for the spawn interval before spawning the next circle
            yield return new WaitForSeconds(spawnInterval);
        }

        // Reset skill state to allow reactivation if needed
        isSkillActive = false;
    }
}
