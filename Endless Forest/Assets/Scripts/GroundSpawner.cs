using UnityEngine;
public class GroundSpawner : MonoBehaviour
{
    private Vector3 nextSpawnPoint;
    public static GroundSpawner groundSpawner;

    [SerializeField] private GameObject groundPrefab;

    private void Start()
    {
        // Singleton instance, providing global access to this script functionality.
        groundSpawner = this;

        // Spawn the initial ground tiles with some containing items
        for (int i = 0; i < 15; i++)
        {
            bool spawnItems = i >= 3; // Only spawn items in the 4th and subsequent tiles
            SpawnGround(spawnItems);
        }
    }

    public void SpawnGround(bool spawnItems)
    {
        // Instantiate a new ground tile at the next spawn point
        GameObject newGround = Instantiate(groundPrefab, nextSpawnPoint, Quaternion.identity);

        // Update the next spawn point to the end position of the new ground tile
        nextSpawnPoint = newGround.transform.Find("NewSpawnPoint - Ground").position;

        // Optionally, spawn obstacles and coins on the new ground tile
        if (spawnItems)
        {
            GroundAdOns groundTile = newGround.GetComponent<GroundAdOns>();
            groundTile.SpawnObstacle();
            groundTile.SpawnCoins();
        }
    }
}